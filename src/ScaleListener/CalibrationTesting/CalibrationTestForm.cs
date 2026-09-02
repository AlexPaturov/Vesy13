using System.Globalization;
using System.IO.Ports;
using Vesy13.Services.Hardware;

namespace ScaleListener.CalibrationTesting;

public partial class CalibrationTestForm : Form
{
    private readonly SerialPort _port;
    private IReadOnlyList<CalibrationTestResult> _results = Array.Empty<CalibrationTestResult>();
    private volatile bool _isShuttingDown;
    private int _activeAdcCode;
    private ActiveChannel _selectedChannel = ActiveChannel.Main;

    public CalibrationTestForm()
    {
        InitializeComponent();

        _port = new SerialPort("COM4", 4800, Parity.Even, 8, StopBits.One)
        {
            ReadTimeout = SerialPort.InfiniteTimeout,
            WriteTimeout = 500,
        };
        _port.DataReceived += Port_DataReceived;

        _cmbChannel.SelectedIndex = 0;
        LoadDefaultScenario();
        RunScenario();
    }

    private ActiveChannel CurrentChannel => _selectedChannel;

    private void BtnRun_Click(object? sender, EventArgs e) => RunScenario();
    private void BtnReset_Click(object? sender, EventArgs e)
    {
        LoadDefaultScenario();
        RunScenario();
    }

    private void CmbChannel_SelectedIndexChanged(object? sender, EventArgs e)
    {
        _selectedChannel = _cmbChannel.SelectedIndex == 0 ? ActiveChannel.Main : ActiveChannel.Backup;
        if (_gridResults.Rows.Count > 0)
            RunScenario();
        UpdateActiveCodeLabel();
    }

    private void GridResults_SelectionChanged(object? sender, EventArgs e)
    {
        if (_gridResults.CurrentRow?.Tag is not CalibrationTestResult result)
            return;

        _activeAdcCode = result.AdcCode;
        UpdateActiveCodeLabel();
    }

    private void LoadDefaultScenario()
    {
        _gridAnchors.Rows.Clear();
        _gridAnchors.Rows.Add("0", "10000");
        _gridAnchors.Rows.Add("20", "20000");
        _gridAnchors.Rows.Add("80", "40000");
    }

    private IReadOnlyList<CalibrationAnchor> ReadAnchors()
    {
        var anchors = new List<CalibrationAnchor>();
        foreach (DataGridViewRow row in _gridAnchors.Rows)
        {
            if (row.IsNewRow)
                continue;

            string massText = row.Cells[0].Value?.ToString()?.Trim() ?? "";
            string codeText = row.Cells[1].Value?.ToString()?.Trim() ?? "";
            if (massText.Length == 0 && codeText.Length == 0)
                continue;

            if (!TryParseDecimal(massText, out decimal mass))
                throw new InvalidOperationException($"Строка {row.Index + 1}: неверная масса «{massText}».");
            if (!int.TryParse(codeText, NumberStyles.Integer, CultureInfo.InvariantCulture, out int code))
                throw new InvalidOperationException($"Строка {row.Index + 1}: неверный код АЦП «{codeText}».");

            anchors.Add(new CalibrationAnchor
            {
                Id = anchors.Count + 1,
                Mass = mass,
                AdcCode = code,
            });
        }

        return anchors;
    }

    private static bool TryParseDecimal(string text, out decimal value)
    {
        string normalized = text.Replace(',', '.');
        return decimal.TryParse(
            normalized,
            NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
            CultureInfo.InvariantCulture,
            out value);
    }

    private void RunScenario()
    {
        try
        {
            var anchors = ReadAnchors();
            _results = CalibrationTestRunner.Run(anchors, CurrentChannel);
            ShowResults(_results);
            int failed = _results.Count(result => !result.Passed);
            _lblStatus.Text = $"Проверок: {_results.Count}; отклонений от линейного эталона: {failed}. Выберите строку для передачи её ADC-кода через COM4.";
            _lblStatus.ForeColor = failed == 0 ? Color.Green : Color.DarkRed;
            _btnExport.Enabled = _results.Count > 0;
        }
        catch (Exception ex)
        {
            _results = Array.Empty<CalibrationTestResult>();
            _gridResults.Rows.Clear();
            _btnExport.Enabled = false;
            _lblStatus.Text = ex.Message;
            _lblStatus.ForeColor = Color.DarkRed;
        }
    }

    private void ShowResults(IEnumerable<CalibrationTestResult> results)
    {
        _gridResults.Rows.Clear();
        foreach (var result in results)
        {
            string checkpoint = result.Checkpoint.Replace("\t", " ").Replace("\r", " ").Replace("\n", " ");
            int rowIndex = _gridResults.Rows.Add(
                checkpoint,
                result.AdcCode.ToString(CultureInfo.InvariantCulture),
                result.ExpectedMass.ToString("F5", CultureInfo.InvariantCulture),
                result.ActualMass?.ToString("F5", CultureInfo.InvariantCulture) ?? "—",
                result.ErrorTonnes?.ToString("F5", CultureInfo.InvariantCulture) ?? "—",
                result.SelectedCalibrationPointId?.ToString(CultureInfo.InvariantCulture) ?? "—",
                result.Passed ? "PASS" : "FAIL");
            var row = _gridResults.Rows[rowIndex];
            row.Tag = result;
            row.DefaultCellStyle.BackColor = result.Passed ? Color.Honeydew : Color.MistyRose;
        }

        if (_gridResults.Rows.Count > 0)
        {
            _gridResults.CurrentCell = _gridResults.Rows[0].Cells[0];
            if (_gridResults.Rows[0].Tag is CalibrationTestResult first)
                _activeAdcCode = first.AdcCode;
        }

        UpdateActiveCodeLabel();
    }

    private void BtnExport_Click(object? sender, EventArgs e)
    {
        if (_results.Count == 0)
            return;
        if (_saveCsvDialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            CalibrationCsvExporter.Save(_saveCsvDialog.FileName, _results);
            _lblStatus.Text = $"CSV сохранён: {_saveCsvDialog.FileName}";
            _lblStatus.ForeColor = Color.Green;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось сохранить CSV.\n{ex.Message}", "Экспорт",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnConnect_Click(object? sender, EventArgs e)
    {
        if (_port.IsOpen)
        {
            _port.Close();
            UpdateConnectionState();
            return;
        }

        try
        {
            _port.Open();
            UpdateConnectionState();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось открыть COM4.\n{ex.Message}", "Подключение",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            if (_isShuttingDown || !_port.IsOpen)
                return;

            int bytesToRead = _port.BytesToRead;
            if (bytesToRead <= 0)
                return;

            var buffer = new byte[bytesToRead];
            _port.Read(buffer, 0, bytesToRead);
            foreach (byte value in buffer)
            {
                if (value == 214)
                    SendStaticResponse();
            }
        }
        catch
        {
            // Порт мог закрыться одновременно с получением запроса.
        }
    }

    private void SendStaticResponse()
    {
        int code = Volatile.Read(ref _activeAdcCode);
        int ch0 = CurrentChannel == ActiveChannel.Main ? code : 0;
        int ch1 = CurrentChannel == ActiveChannel.Backup ? code : 0;
        byte[] frame = SimA04StaticFrameBuilder.Build(ch0, ch1);
        _port.Write(frame, 0, frame.Length);
    }

    private void UpdateConnectionState()
    {
        _btnConnect.Text = _port.IsOpen ? "Disconnect" : "Connect";
        _btnConnect.BackColor = _port.IsOpen ? Color.Honeydew : SystemColors.Control;
        UpdateActiveCodeLabel();
    }

    private void UpdateActiveCodeLabel()
    {
        string channel = CurrentChannel == ActiveChannel.Main ? "CH0" : "CH1";
        string connection = _port?.IsOpen == true ? "ONLINE" : "OFFLINE";
        _lblActiveCode.Text = $"{connection}  COM4  {channel}={_activeAdcCode}";
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _isShuttingDown = true;
        _port.DataReceived -= Port_DataReceived;
        if (_port.IsOpen)
            _port.Close();
        base.OnFormClosing(e);
    }
}
