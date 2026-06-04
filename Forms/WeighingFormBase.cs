using Vesy13.Models;
using Vesy13.Services;

namespace Vesy13.Forms;

public partial class WeighingFormBase : Form
{
    protected AdcService         _adc   = null!;
    protected CalibrationService _calib = null!;
    protected DatabaseService    _db    = null!;

    // ── State machine ──────────────────────────────────────────────────────
    private enum WeighState { Idle, Bogie1Captured }
    private WeighState _state = WeighState.Idle;
    private DateTime?  _trainStartTime;
    private int        _wagonNumber;
    private int        _bogie1Code;
    protected AdcFrame _lastFrame;

    // ── Virtual interface ──────────────────────────────────────────────────
    protected virtual string GetDirection()        => "";
    protected virtual bool   ValidateBeforeWeigh() => false;
    protected virtual bool   ShowDirectionColumn   => false;
    protected virtual double ToTonnes(int adcCode) => 0;
    protected virtual string GetMode()             => "";

    public WeighingFormBase() { }  // for designer / derived classes

    protected WeighingFormBase(AdcService adc, CalibrationService calib, DatabaseService db)
    {
        _adc   = adc;
        _calib = calib;
        _db    = db;
    }

    // ── Grid columns (зависят от ShowDirectionColumn — добавляются в OnLoad) ─
    private void SetupGridColumns()
    {
        if (ShowDirectionColumn)
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Напр.", Width = 52,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "№",     Width = 38,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тел.1", Width = 90,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тел.2", Width = 90,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Вагон", Width = 90,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Время", Width = ShowDirectionColumn ? 80 : 100, SortMode = DataGridViewColumnSortMode.NotSortable });
    }

    // ── Designer event handlers ────────────────────────────────────────────
    protected void BtnWeigh_Click(object? sender, EventArgs e)  => HandleWeighPress();
    protected void BtnZero_Click(object? sender, EventArgs e)   => OnZeroClick();
    protected void BtnFinish_Click(object? sender, EventArgs e) => HandleFinish();
    protected void BtnBack_Click(object? sender, EventArgs e)   => Close();

    // ── Lifecycle ──────────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (DesignMode || _adc is null) return;
        SetupGridColumns();
        _adc.FrameReceived     += OnFrame;
        _adc.ConnectionChanged += OnConnectionChanged;
        UpdateConn(_adc.IsConnected);
        UpdateChannelLabel();
        UpdateButtonStates();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (!DesignMode && _adc is not null)
        {
            _adc.FrameReceived     -= OnFrame;
            _adc.ConnectionChanged -= OnConnectionChanged;
        }
        base.OnFormClosed(e);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Space && _btnWeigh.Enabled)
        {
            HandleWeighPress();
            return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    // ── ADC events ─────────────────────────────────────────────────────────

    private void OnFrame(object? sender, AdcFrame frame)
    {
        if (InvokeRequired) { BeginInvoke(() => OnFrame(sender, frame)); return; }
        _lastFrame = frame;
        if (_state == WeighState.Idle)
        {
            _lblValue.Text      = ToTonnes(ActiveCode(frame)).ToString("F2");
            _lblValue.ForeColor = _adc.IsConnected ? Color.LimeGreen : Color.DimGray;
        }
    }

    private void OnConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) { BeginInvoke(() => OnConnectionChanged(sender, connected)); return; }
        UpdateConn(connected);
    }

    private void UpdateConn(bool connected)
    {
        _dotConn.BackColor = connected ? Color.LimeGreen : Color.Gray;
        _lblConn.Text      = connected ? $"АЦП: {_adc.PortName}" : "АЦП: отключён";
        if (_state == WeighState.Idle)
            _lblValue.ForeColor = connected ? Color.LimeGreen : Color.DimGray;
    }

    protected void UpdateChannelLabel() =>
        _lblChannel.Text = _adc.Channel == ActiveChannel.Main
            ? "Канал: Основной (CH0)"
            : "Канал: Резервный (CH1)";

    protected int ActiveCode(AdcFrame f) =>
        _adc.Channel == ActiveChannel.Main ? f.Ch0 : f.Ch1;

    // ── Weighing logic ─────────────────────────────────────────────────────

    private void HandleWeighPress()
    {
        if (!ValidateBeforeWeigh()) return;

        if (_state == WeighState.Idle)
        {
            _wagonNumber++;
            if (_wagonNumber == 1)
                _trainStartTime = DateTime.Now;

            _bogie1Code = ActiveCode(_lastFrame);
            _state      = WeighState.Bogie1Captured;

            _lblValue.Text      = ToTonnes(_bogie1Code).ToString("F2");
            _lblValue.ForeColor = Color.Yellow;
            _lblStatus.Text     = "Тележка 1 зафиксирована  —  Ожидание тележки 2";
            _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 2";
            _btnWeigh.BackColor = Color.FromArgb(180, 100, 0);
        }
        else
        {
            int      bogie2    = ActiveCode(_lastFrame);
            DateTime wagonTime = DateTime.Now;
            var      record    = new WagonRecord(_wagonNumber, _trainStartTime!.Value,
                                                 wagonTime, ToTonnes(_bogie1Code), ToTonnes(bogie2), GetDirection());
            AddToGrid(record);
            SaveAsync(record);

            _state              = WeighState.Idle;
            _lblValue.Text      = record.Total.ToString("F2");
            _lblValue.ForeColor = Color.Cyan;
            _lblStatus.Text     = $"Вагон №{_wagonNumber}: {record.Total:F2} т  —  Готов к следующему";
            _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
            _btnWeigh.BackColor = Color.FromArgb(0, 130, 0);
        }

        UpdateButtonStates();
    }

    private void OnZeroClick()
    {
        MessageBox.Show("Ноль установлен (в разработке)", "Ноль",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void HandleFinish()
    {
        if (_wagonNumber == 0) return;
        var res = MessageBox.Show(
            $"Завершить состав?\nВзвешено вагонов: {_wagonNumber}",
            "Завершить состав", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (res != DialogResult.Yes) return;

        _trainStartTime     = null;
        _wagonNumber        = 0;
        _state              = WeighState.Idle;
        _grid.Rows.Clear();
        _lblValue.Text      = "—";
        _lblValue.ForeColor = _adc.IsConnected ? Color.LimeGreen : Color.DimGray;
        _lblStatus.Text     = "Готов к взвешиванию  —  Тележка 1";
        _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
        _btnWeigh.BackColor = Color.FromArgb(0, 130, 0);
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        bool idle          = _state == WeighState.Idle;
        _btnZero.Enabled   = idle;
        _btnFinish.Enabled = idle && _wagonNumber > 0;
    }

    private void AddToGrid(WagonRecord r)
    {
        var values = new List<object>();
        if (ShowDirectionColumn) values.Add(r.Direction);
        values.Add(r.Number.ToString());
        values.Add(r.Bogie1.ToString("F2"));
        values.Add(r.Bogie2.ToString("F2"));
        values.Add(r.Total.ToString("F2"));
        values.Add(r.WagonTime.ToString("HH:mm:ss"));
        _grid.Rows.Insert(0, values.ToArray());
        while (_grid.Rows.Count > 10)
            _grid.Rows.RemoveAt(_grid.Rows.Count - 1);
    }

    private async void SaveAsync(WagonRecord record)
    {
        try
        {
            await _db.SaveWagonAsync(record, GetMode());
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения в БД:\n{ex.Message}",
                "База данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
