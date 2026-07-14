using Vesy13.Application;
using Vesy13.Models;
using Vesy13.Services.Configuration;
using Vesy13.Services.Hardware;
using Vesy13.Services.Hardware.Filters;
using Vesy13.Services.Repositories;

namespace Vesy13.Forms;

/// <summary>
/// Форма статического взвешивания: вагон стоит, оператор последовательно фиксирует
/// две тележки нажатием пробела, результат сохраняется в PostgreSQL.
/// </summary>
public partial class StaticWeighingForm : Form
{
    private SimA04ReaderStatic    _sim = null!;
    private StaticFilterPipeline  _filter = null!;
    private LocalRepository _ldb  = null!;
    private SettingsService _settings = null!;

    private enum WeighState { Idle, Bogie1Captured }
    private WeighState _state = WeighState.Idle;
    private DateTime?  _trainStartTime;
    private int        _wagonNumber;
    private int        _bogie1Code;
    private SimA04Frame   _lastFrame;
    private double     _zeroOffsetTonnes;
    private DateTime   _lastRawDiagAt = DateTime.MinValue;
    private DateTime   _lastCalcDiagAt = DateTime.MinValue;
    private string     _lastRawBytes = "";
    private bool       _weighingStorageAvailable = true;

    public StaticWeighingForm()
    {
        InitializeComponent();
    }

    public StaticWeighingForm(LocalRepository ldb, SettingsService settings)
    {
        _sim = new SimA04ReaderStatic { Channel = settings.Current.ActiveChannel };
        _filter = new StaticFilterPipeline(_sim, settings.Current);
        _ldb  = ldb;
        _settings = settings;
        InitializeComponent();
    }

    private double ReadRawTonnes(int adcCode) =>
        CalibrationCalculator.Convert(_ldb.CalibPoints, adcCode, _sim.Channel) ?? 0;

    private bool HasStaticCalibration() => ActiveCalibPointCount() > 0;

    private bool ValidateBeforeWeigh()
    {
        if (!_sim.IsConnected)
        {
            MessageBox.Show("АЦП не подключён.", "Взвешивание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (HasStaticCalibration()) return true;
        MessageBox.Show($"Нет статической калибровки для канала {(_sim.Channel == ActiveChannel.Main ? "CH0" : "CH1")}.",
            "Взвешивание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
    }

    private (CalibPoint? Point, int ActiveCount) SelectStaticCalibPoint(int adcCode)
    {
        int channel = _sim.Channel == ActiveChannel.Main ? 0 : 1;
        var active = _ldb.CalibPoints
            .Where(point => point.Channel == channel && point.IsActive)
            .OrderBy(point => point.AdcCode)
            .ToList();

        if (active.Count == 0)
            return (null, 0);

        var selected = active[0];
        foreach (var point in active)
        {
            if (adcCode >= point.AdcCode)
                selected = point;
            else
                break;
        }

        return (selected, active.Count);
    }

    private string BuildStaticCalcDiagnostic(int adcCode, double rawTonnes, double resultTonnes)
    {
        int channel = _sim.Channel == ActiveChannel.Main ? 0 : 1;
        var (point, activeCount) = SelectStaticCalibPoint(adcCode);
        var common = $"calibChannel={channel} activeCalibPoints={activeCount} totalCalibPoints={_ldb.CalibPoints.Count} " +
                     $"rawTonnes={rawTonnes:F4} zeroOffset={_zeroOffsetTonnes:F4} resultTonnes={resultTonnes:F4} " +
                     $"discretization={_settings.Current.WeightDiscretizationTonnes:F4}";

        if (point is null)
            return $"{common} calibPoint=none";

        double k = point.AdcCode == 0 ? 0 : (double)point.Mass / point.AdcCode;
        return $"{common} calibPointId={point.Id} calibPointChannel={point.Channel} " +
               $"calibPointCode={point.AdcCode} calibPointMass={(double)point.Mass:F4} calibK={k:F8}";
    }

    private double ToTonnes(int adcCode) =>
        WeightFormatter.RoundToDiscretization(
            ReadRawTonnes(adcCode) - _zeroOffsetTonnes,
            _settings.Current.WeightDiscretizationTonnes);

    private void ApplyTheme()
    {
        BackColor = UiColors.AppBackground;
        _layoutMain.BackColor = UiColors.AppBackground;
        _pnlActions.BackColor = UiColors.AppBackground;
        _lblChannel.Font  = UiFonts.Medium;
        _lblChannel.ForeColor = UiColors.TextMuted;
        _pnlDisplay.BackColor = UiColors.DisplayBackground;
        _lblValue.Font    = UiFonts.Display;
        _lblValue.ForeColor = UiColors.TextOnDarkMuted;
        _lblBogie1Caption.Font = UiFonts.Body;
        _lblBogie1Caption.ForeColor = UiColors.TextOnDarkMuted;
        _lblBogie1Value.Font = UiFonts.Medium;
        _lblBogie1Value.ForeColor = UiColors.TextOnDarkMuted;
        _lblBogie2Caption.Font = UiFonts.Body;
        _lblBogie2Caption.ForeColor = UiColors.TextOnDarkMuted;
        _lblBogie2Value.Font = UiFonts.Medium;
        _lblBogie2Value.ForeColor = UiColors.TextOnDarkMuted;
        _btnWeigh.Font    = UiFonts.WeighButton;
        _btnWeigh.BackColor = UiColors.PrimaryAction;
        _btnWeigh.ForeColor = UiColors.TextOnDark;
        _btnZero.Font     = UiFonts.Medium;
        _btnZero.BackColor = UiColors.NeutralAction;
        _btnZero.ForeColor = UiColors.TextPrimary;
        _btnFinish.Font   = UiFonts.Medium;
        _btnFinish.BackColor = UiColors.DangerAction;
        _btnFinish.ForeColor = UiColors.TextOnDark;
        _grid.Font        = UiFonts.GridBody;
        _grid.BackgroundColor = UiColors.Surface;
        _grid.BorderStyle = BorderStyle.FixedSingle;
        _grid.ColumnHeadersDefaultCellStyle.Font = UiFonts.GridHeader;
        _grid.ColumnHeadersDefaultCellStyle.BackColor = UiColors.GridHeaderBack;
        _grid.ColumnHeadersDefaultCellStyle.ForeColor = UiColors.GridHeaderText;
        _grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiColors.GridHeaderBack;
        _grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiColors.GridHeaderText;
        _grid.DefaultCellStyle.BackColor = UiColors.Surface;
        _grid.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
        _grid.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
        _grid.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
        _grid.AlternatingRowsDefaultCellStyle.BackColor = UiColors.GridAlternateRow;
        _grid.AlternatingRowsDefaultCellStyle.ForeColor = UiColors.TextPrimary;
        _grid.GridColor = UiColors.GridLine;
        _pnlStatusBar.BackColor = UiColors.StatusBar;
        _dotConn.BackColor = UiColors.Disconnected;
        _lblConn.Font = UiFonts.Body;
        _lblConn.ForeColor = UiColors.TextMuted;
        _lblStorage.Font = UiFonts.Body;
        _lblStorage.ForeColor = UiColors.TextMuted;
    }

    private void SetupGridColumns()
    {
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        DataGridViewTextBoxColumn Col(string header, float fillWeight) => new()
        {
            HeaderText = header,
            FillWeight = fillWeight,
            SortMode   = DataGridViewColumnSortMode.NotSortable,
        };

        _grid.Columns.Add(Col("№", 45));
        _grid.Columns.Add(Col("Тел.1", 75));
        _grid.Columns.Add(Col("Тел.2", 75));
        _grid.Columns.Add(Col("Вагон", 75));
        _grid.Columns.Add(Col("Время вагона", 140));
        _grid.Columns.Add(Col("Дата состава", 130));
        _grid.Columns.Add(Col("Время состава", 140));
    }
    // ── Lifecycle ──────────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyTheme();
        if (DesignMode || _sim is null) return;
        AuditLogger.Action(AuditLogger.FormOpened, "Form", "StaticWeighingForm");
        _sim.ConnectionTimeoutMs = 2000;
        SetupGridColumns();
        _filter.FilteredFrameReceived += OnFrame;
        _sim.RawFrameReceived  += OnRawFrame;
        _sim.ConnectionChanged += OnConnectionChanged;
        AuditLogger.QueueStatusChanged += OnAuditQueueStatusChanged;
        EnsureAdcConnected(showError: true);
        UpdateConn(_sim.IsConnected);
        UpdateChannelLabel();
        UpdateButtonStates();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (!DesignMode && _sim is not null)
        {
            if (_state == WeighState.Bogie1Captured)
            {
                if (MessageBox.Show(
                    "Взвешивание вагона не завершено — зафиксирована только тележка 1.\nЗамер тележки 1 будет потерян. Выйти?",
                    "Незавершённый вагон", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
            else if (_wagonNumber > 0)
            {
                HandleFinish(silent: true);
            }
        }
        base.OnFormClosing(e);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (!DesignMode && _sim is not null)
        {
            _filter.FilteredFrameReceived -= OnFrame;
            _filter.Dispose();
            _sim.RawFrameReceived  -= OnRawFrame;
            _sim.ConnectionChanged -= OnConnectionChanged;
            AuditLogger.QueueStatusChanged -= OnAuditQueueStatusChanged;
            _sim.Close();
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

    // ── Designer event handlers ────────────────────────────────────────────

    private void BtnWeigh_Click(object? sender, EventArgs e)  => HandleWeighPress();
    private void BtnZero_Click(object? sender, EventArgs e)   => OnZeroClick();
    private void BtnFinish_Click(object? sender, EventArgs e) => HandleFinish();

    private bool EnsureAdcConnected(bool showError)
    {
        if (_sim.IsPortOpen) return true;

        try
        {
            _sim.Open(_settings.Current.AdcPortName);
            return true;
        }
        catch (Exception ex)
        {
            UpdateConn(false);
            if (showError)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }

    // ── ADC events ─────────────────────────────────────────────────────────

    private void OnRawFrame(object? sender, byte[] raw)
    {
        _lastRawBytes = string.Join(" ", raw);
        var now = DateTime.Now;
        if ((now - _lastRawDiagAt).TotalSeconds < 1) return;
        _lastRawDiagAt = now;

        var frame = SimA04Frame.Parse(raw);
        AuditLogger.Action(AuditLogger.AdcStaticRawFrame,
            "StaticRawFrame",
            $"len={raw.Length} valid={frame.Valid} bytes=[{_lastRawBytes}] ch0={frame.Ch0} ch1={frame.Ch1}",
            _sim.PortName,
            _sim.Channel.ToString());
    }

    private void OnFrame(object? sender, SimA04Frame frame)
    {
        if (InvokeRequired)
        {
            BeginInvoke(() => OnFrame(sender, frame));
            return;
        }

        _lastFrame = frame;
        int code = ActiveCode(frame);
        double tonnes = ToTonnes(code);

        if (!HasStaticCalibration())
        {
            _lblValue.Text = "—";
            _lblValue.ForeColor = UiColors.Disconnected;
        }
        else
        {
            _lblValue.Text = tonnes.ToString("F2");
            _lblValue.ForeColor = _sim.IsConnected
                ? (_state == WeighState.Bogie1Captured ? UiColors.PendingAction : UiColors.PrimaryAction)
                : UiColors.Disconnected;

            if (_state == WeighState.Bogie1Captured)
                SetBogieValue(_lblBogie2Value, tonnes);
        }

        WriteCalcDiagnostic(frame, code, tonnes);
    }

    private void OnConnectionChanged(object? sender, bool connected)
    {
        if (InvokeRequired) 
        { 
            BeginInvoke(() => OnConnectionChanged(sender, connected)); 
            return; 
        }
        UpdateConn(connected);
    }

    private void UpdateConn(bool connected)
    {
        _dotConn.BackColor = connected ? Color.LimeGreen : Color.Red;
        _lblConn.ForeColor = connected ? Color.LimeGreen : Color.Red;
        var adcState = connected
            ? $"АЦП: {_sim.PortName}"
            : _sim.IsPortOpen ? $"АЦП: нет валидного потока ({_sim.PortName})" : "АЦП: порт закрыт";
        _lblConn.Text = adcState;
        UpdateStorageStatus();
        UpdateButtonStates();
        if (_state == WeighState.Idle)
            _lblValue.ForeColor = connected ? UiColors.PrimaryAction : UiColors.Disconnected;
    }

    private void OnAuditQueueStatusChanged(object? sender, AuditLogger.AuditQueueStatus status)
    {
        if (IsDisposed || Disposing) return;
        if (InvokeRequired)
        {
            BeginInvoke(() => OnAuditQueueStatusChanged(sender, status));
            return;
        }

        UpdateConn(_sim.IsConnected);
    }

    private void UpdateStorageStatus()
    {
        var audit = AuditLogger.GetQueueStatus();
        var messages = new List<string>();
        if (!HasStaticCalibration())
            messages.Add($"Статическая калибровка: не задана для канала {(_sim.Channel == ActiveChannel.Main ? "CH0" : "CH1")}");
        if (!_weighingStorageAvailable)
            messages.Add("Взвешивание: БД недоступна, запись не сохранена");

        var hasError = !HasStaticCalibration() || !_weighingStorageAvailable || !audit.IsDatabaseAvailable || audit.DroppedCount > 0;
        if (!audit.IsDatabaseAvailable)
            messages.Add($"БД недоступна; очередь {audit.PendingCount}/{AuditLogger.QueueCapacity}; потеряно {audit.DroppedCount}");
        else if (audit.PendingCount > 0 || audit.DroppedCount > 0)
            messages.Add($"синхронизация {audit.PendingCount}/{AuditLogger.QueueCapacity}; потеряно {audit.DroppedCount}");

        _lblStorage.Text = string.Join(" | ", messages);
        _lblStorage.ForeColor = hasError ? Color.Red : UiColors.TextMuted;
    }

    private void UpdateChannelLabel() => _lblChannel.Text = _sim.Channel == ActiveChannel.Main ? "Канал: Основной (CH0)" : "Канал: Резервный (CH1)";

    private int ActiveCode(SimA04Frame f) => _sim.Channel == ActiveChannel.Main ? f.Ch0 : f.Ch1;

    private static void SetBogieValue(Label label, double tonnes) => label.Text = $"{tonnes:F2} т";

    private void ResetBogieValues()
    {
        _lblBogie1Value.Text = "—";
        _lblBogie2Value.Text = "—";
    }

    private int ActiveCalibPointCount()
    {
        int channel = _sim.Channel == ActiveChannel.Main ? 0 : 1;
        return _ldb.CalibPoints.Count(p => p.Channel == channel && p.IsActive);
    }

    private void WriteCalcDiagnostic(SimA04Frame frame, int activeCode, double tonnes)
    {
        var now = DateTime.Now;
        if ((now - _lastCalcDiagAt).TotalSeconds < 1) return;
        _lastCalcDiagAt = now;

        double rawTonnes = ReadRawTonnes(activeCode);
        string calc = BuildStaticCalcDiagnostic(activeCode, rawTonnes, tonnes);
        AuditLogger.Action(AuditLogger.AdcStaticCalc,
            "StaticCalc",
            $"channel={_sim.Channel} activeCode={activeCode} tonnes={tonnes:F2} ch0={frame.Ch0} ch1={frame.Ch1} {calc}",
            _sim.PortName,
            $"raw=[{_lastRawBytes}]");
    }

    // ── Weighing logic ─────────────────────────────────────────────────────

    private void HandleWeighPress()
    {
        if (!_btnWeigh.Enabled) return;
        if (!ValidateBeforeWeigh()) return;
        if (_state == WeighState.Idle)
        {
            _wagonNumber++;
            if (_wagonNumber == 1)
                _trainStartTime = DateTime.Now;
            _bogie1Code         = ActiveCode(_lastFrame);
            double bogie1Tonnes = ToTonnes(_bogie1Code);
            double bogie1RawTonnes = ReadRawTonnes(_bogie1Code);
            string bogie1Calc = BuildStaticCalcDiagnostic(_bogie1Code, bogie1RawTonnes, bogie1Tonnes);
            AuditLogger.Action(AuditLogger.AdcStaticCalc,
                "StaticWeighPress",
                $"step=bogie1 channel={_sim.Channel} code={_bogie1Code} tonnes={bogie1Tonnes:F2} ch0={_lastFrame.Ch0} ch1={_lastFrame.Ch1} {bogie1Calc}",
                _sim.PortName,
                $"raw=[{_lastRawBytes}]");
            _state              = WeighState.Bogie1Captured;
            SetBogieValue(_lblBogie1Value, bogie1Tonnes);
            _lblBogie2Value.Text = "—";
            _lblValue.ForeColor = UiColors.PendingAction;
            _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 2";
            _btnWeigh.BackColor = UiColors.PendingAction;
        }
        else
        {
            int      bogie2    = ActiveCode(_lastFrame);
            double   bogie2Tonnes = ToTonnes(bogie2);
            double   bogie1Tonnes = ToTonnes(_bogie1Code);
            double   bogie2RawTonnes = ReadRawTonnes(bogie2);
            string   bogie2Calc = BuildStaticCalcDiagnostic(bogie2, bogie2RawTonnes, bogie2Tonnes);
            AuditLogger.Action(AuditLogger.AdcStaticCalc,
                "StaticWeighPress",
                $"step=bogie2 channel={_sim.Channel} code={bogie2} tonnes={bogie2Tonnes:F2} ch0={_lastFrame.Ch0} ch1={_lastFrame.Ch1} {bogie2Calc}",
                _sim.PortName,
                $"raw=[{_lastRawBytes}]");
            DateTime wagonTime = DateTime.Now;
            var record = new LocalWagon
            {
                Number    = _wagonNumber,
                TrainTime = _trainStartTime!.Value,
                WagonTime = wagonTime,
                Bogie1    = bogie1Tonnes,
                Bogie2    = bogie2Tonnes,
                Mode      = "СТАТИКА",
            };
            AddToGrid(record);
            SaveAsync(record);
            AuditLogger.Action(AuditLogger.WeighingSaved,
                "LocalWagon", $"вагон №{_wagonNumber} total={record.Total:F2}",
                "PostgreSQL", _wagonNumber.ToString());
            _state              = WeighState.Idle;
            SetBogieValue(_lblBogie1Value, bogie1Tonnes);
            SetBogieValue(_lblBogie2Value, bogie2Tonnes);
            _lblValue.Text      = record.Total.ToString("F2");
            _lblValue.ForeColor = UiColors.Info;
            _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
            _btnWeigh.BackColor = UiColors.PrimaryAction;
        }
        UpdateButtonStates();
    }

    private void OnZeroClick()
    {
        if (!ValidateBeforeWeigh()) return;
        double current = ReadRawTonnes(ActiveCode(_lastFrame));
        double limit = _settings.Current.OperatorZeroLimitTonnes;
        if (Math.Abs(current) > limit)
        {
            MessageBox.Show($"Ноль можно установить только в пределах {limit:F2} т.", "Ноль",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _zeroOffsetTonnes = current;
        _lblValue.Text = ToTonnes(ActiveCode(_lastFrame)).ToString("F2");
        AuditLogger.Action(AuditLogger.ZeroSet, "Zero", $"offset={_zeroOffsetTonnes:F2} limit={limit:F2}");
    }

    private void HandleFinish(bool silent = false)
    {
        if (_wagonNumber == 0) return;
        if (!silent && MessageBox.Show($"Завершить состав?\nВзвешено вагонов: {_wagonNumber}",
                "Завершить состав", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        int finishedCount   = _wagonNumber;
        _trainStartTime     = null;
        _wagonNumber        = 0;
        _state              = WeighState.Idle;
        _grid.Rows.Clear();
        _lblValue.Text      = "—";
        _lblValue.ForeColor = _sim.IsConnected ? UiColors.PrimaryAction : UiColors.Disconnected;
        ResetBogieValues();
        _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
        _btnWeigh.BackColor = UiColors.PrimaryAction;
        AuditLogger.Action(AuditLogger.TrainFinished,
            "Train", $"СТАТИКА вагонов={finishedCount}", "PostgreSQL");
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        bool canWeigh = HasStaticCalibration() && _sim.IsConnected;
        _btnWeigh.Enabled  = canWeigh;
        _btnZero.Enabled   = _state == WeighState.Idle && canWeigh;
        _btnFinish.Enabled = _state == WeighState.Idle && _wagonNumber > 0;
    }

    private void AddToGrid(LocalWagon r)
    {
        _grid.Rows.Insert(0, r.Number.ToString(),
            r.Bogie1.ToString("F2"), r.Bogie2.ToString("F2"),
            r.Total.ToString("F2"), r.WagonTime.ToString("HH:mm:ss"),
            r.TrainTime.ToString("dd.MM.yyyy"), r.TrainTime.ToString("HH:mm:ss"));
    }

    private async void SaveAsync(LocalWagon record)
    {
        try
        {
            await _ldb.SaveWagonAsync(record);
            _weighingStorageAvailable = true;
            UpdateConn(_sim.IsConnected);
            if (_state == WeighState.Idle && _wagonNumber == record.Number)
                ResetBogieValues();
        }
        catch (Exception ex)
        {
            _weighingStorageAvailable = false;
            UpdateConn(_sim.IsConnected);
            AuditLogger.Error(AuditLogger.ErrorDb,
                "LocalWagon", $"вагон №{record.Number}", "PostgreSQL", ex.Message);
        }
    }
}
