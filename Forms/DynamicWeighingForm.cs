using Vesy13.Application;
using Vesy13.Models;
using Vesy13.Services.Configuration;
using Vesy13.Services.Hardware;
using Vesy13.Services.Repositories;

namespace Vesy13.Forms;

/// <summary>
/// Форма динамического взвешивания: фиксирует две тележки на ходу и рассчитывает
/// нетто с учётом направления движения состава (→/←).
/// </summary>
public partial class DynamicWeighingForm : Form
{
    private SimA04ReaderDynamic  _sim = null!;
    private LocalRepository _ldb  = null!;
    private SettingsService _settings = null!;
    private enum WeighState { Idle, Bogie1Captured }
    private WeighState _state = WeighState.Idle;
    private DateTime?  _trainStartTime;
    private int        _wagonNumber;
    private int        _bogie1Code;
    private SimA04DynamicSample _lastSample;
    private double     _zeroOffsetTonnes;
    private readonly System.Windows.Forms.Timer _adcDiagTimer = new();
    private readonly object _sampleSync = new();
    private SimA04DynamicSample _latestSample;
    private long _latestSampleVersion;
    private long _displayedSampleVersion;
    private long _uiSamplesReceived;
    private long _uiSamplesApplied;
    private long _lastLoggedRawBytes;
    private long _lastLoggedSamples;
    private long _lastLoggedSkippedBytes;
    private long _lastLoggedUiReceived;
    private long _lastLoggedUiApplied;
    private long _lastDynamicDiagAt;
    private bool _weighingStorageAvailable = true;

    public DynamicWeighingForm()
    {
        InitializeComponent();
    }

    public DynamicWeighingForm(SimA04ReaderDynamic sim, LocalRepository ldb, SettingsService settings)
    {
        _sim = sim;
        _ldb  = ldb;
        _settings = settings;
        InitializeComponent();
        _adcDiagTimer.Interval = 100;
        _adcDiagTimer.Tick += (_, _) =>
        {
            RefreshDisplayFromLatestSample();
            UpdateAdcDiagnostics();
        };
    }

    private string GetDirection() => _rbPlus.Checked ? "→ (+)" : "← (–)";

    private bool HasDynamicCalibration() => _ldb.Dynamic.IsActive && _ldb.Dynamic.DeletedAt is null;

    private double ReadRawTonnes(int adcCode) => CalibrationCalculator.ConvertDynamic(_ldb.Dynamic, adcCode, GetDirection());

    private double ToTonnes(int adcCode) =>
        WeightFormatter.RoundToDiscretization(ReadRawTonnes(adcCode) - _zeroOffsetTonnes, _settings.Current.WeightDiscretizationTonnes);

    private bool ValidateBeforeWeigh()
    {
        if (!_rbPlus.Checked && !_rbMinus.Checked)
        {
            MessageBox.Show("Выберите направление движения состава.", "Взвешивание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (HasDynamicCalibration()) return true;
        MessageBox.Show($"Нет динамической калибровки для направления {GetDirection()}.", "Взвешивание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
    }

    private void ApplyTheme()
    {
        BackColor = UiColors.AppBackground;
        _layoutMain.BackColor = UiColors.AppBackground;
        _pnlTop.BackColor = UiColors.AppBackground;
        _pnlActions.BackColor = UiColors.AppBackground;
        _gbDir.BackColor = UiColors.Surface;
        _gbDir.Font       = UiFonts.Body;
        _gbDir.ForeColor  = UiColors.TextPrimary;
        _rbPlus.Font      = UiFonts.Medium;
        _rbPlus.ForeColor  = UiColors.TextPrimary;
        _rbMinus.Font     = UiFonts.Medium;
        _rbMinus.ForeColor = UiColors.TextPrimary;
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
        _lblConn.Font     = UiFonts.Body;
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

        _grid.Columns.Add(Col("Напр.", 65));
        _grid.Columns.Add(Col("№", 45));
        _grid.Columns.Add(Col("Тел.1", 75));
        _grid.Columns.Add(Col("Тел.2", 75));
        _grid.Columns.Add(Col("Вагон", 75));
        _grid.Columns.Add(Col("Время вагона", 120));
        _grid.Columns.Add(Col("Дата состава", 120));
        _grid.Columns.Add(Col("Время состава", 120));
    }
    // ── Lifecycle ──────────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyTheme();
        if (DesignMode || _sim is null) return;
        AuditLogger.Action(AuditLogger.FormOpened, "Form", "DynamicWeighingForm");
        _sim.ConnectionTimeoutMs = 5000;
        SetupGridColumns();
        _sim.SampleReceived += OnSample;
        _sim.ConnectionChanged += OnConnectionChanged;
        _rbPlus.CheckedChanged += Direction_CheckedChanged;
        _rbMinus.CheckedChanged += Direction_CheckedChanged;
        AuditLogger.QueueStatusChanged += OnAuditQueueStatusChanged;
        _adcDiagTimer.Start();
        EnsureAdcConnected(showError: true);
        UpdateConn(_sim.IsConnected);
        UpdateChannelLabel();
        ResetBogieValues();
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
            _adcDiagTimer.Stop();
            _sim.SampleReceived             -= OnSample;
            _sim.ConnectionChanged          -= OnConnectionChanged;
            _rbPlus.CheckedChanged          -= Direction_CheckedChanged;
            _rbMinus.CheckedChanged         -= Direction_CheckedChanged;
            AuditLogger.QueueStatusChanged  -= OnAuditQueueStatusChanged;
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

    private void Direction_CheckedChanged(object? sender, EventArgs e)
    {
        _displayedSampleVersion = 0;
        RefreshDisplayFromLatestSample();
        UpdateButtonStates();
        UpdateAdcDiagnostics();
    }

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
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }

    // ── ADC events ─────────────────────────────────────────────────────────

    private void OnSample(object? sender, SimA04DynamicSample sample)
    {
        lock (_sampleSync)
        {
            _latestSample = sample;
            _latestSampleVersion++;
        }

        Interlocked.Increment(ref _uiSamplesReceived);
    }

    private void RefreshDisplayFromLatestSample()
    {
        SimA04DynamicSample sample;
        long version;

        lock (_sampleSync)
        {
            if (_latestSampleVersion == _displayedSampleVersion) return;
            sample = _latestSample;
            version = _latestSampleVersion;
        }

        _displayedSampleVersion = version;
        Interlocked.Increment(ref _uiSamplesApplied);
        _lastSample = sample;

        if (!HasDynamicCalibration())
        {
            _lblValue.Text = "—";
            _lblValue.ForeColor = UiColors.Disconnected;
            return;
        }

        _lblValue.Text = ToTonnes(ActiveCode(sample)).ToString("F2");
        _lblValue.ForeColor = _sim.IsConnected ? UiColors.PrimaryAction : UiColors.Disconnected;

        if (_state == WeighState.Bogie1Captured)
            SetBogieValue(_lblBogie2Value, ToTonnes(ActiveCode(sample)));
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

    private void OnAuditQueueStatusChanged(object? sender, AuditLogger.AuditQueueStatus status)
    {
        if (IsDisposed || Disposing) return;
        if (InvokeRequired)
        {
            BeginInvoke(() => OnAuditQueueStatusChanged(sender, status));
            return;
        }

        UpdateAdcDiagnostics();
    }

    private void UpdateConn(bool connected)
    {
        _dotConn.BackColor = connected ? Color.LimeGreen : Color.Red;
        _lblConn.ForeColor = connected ? Color.LimeGreen : Color.Red;
        _lblConn.Text      = connected ? $"АЦП: {_sim.PortName}" : "АЦП: отключён";
        if (_state == WeighState.Idle)
            _lblValue.ForeColor = connected ? UiColors.PrimaryAction : UiColors.Disconnected;
        UpdateAdcDiagnostics();
    }

    private void UpdateAdcDiagnostics()
    {
        if (DesignMode || _sim is null) return;

        WriteDynamicDiagnostics();

        if (!_sim.IsPortOpen)
        {
            _dotConn.BackColor = Color.Red;
            _lblConn.ForeColor = Color.Red;
            _lblConn.Text = "АЦП: порт закрыт";
            UpdateStorageStatus();
            return;
        }

        var connected = _sim.IsConnected;
        _dotConn.BackColor = connected ? Color.LimeGreen : Color.Red;
        _lblConn.ForeColor = connected ? Color.LimeGreen : Color.Red;
        var adcState = connected
            ? $"АЦП: {_sim.PortName}"
            : $"АЦП: нет валидного потока ({_sim.PortName})";
        _lblConn.Text = adcState;
        UpdateStorageStatus();
    }

    private void WriteDynamicDiagnostics()
    {
        const long intervalMs = 5000;
        var now = Environment.TickCount64;
        if (_lastDynamicDiagAt != 0 && now - _lastDynamicDiagAt < intervalMs) return;

        var elapsedMs = _lastDynamicDiagAt == 0 ? intervalMs : now - _lastDynamicDiagAt;
        _lastDynamicDiagAt = now;

        var rawBytes = _sim.RawBytesReceived;
        var samples = _sim.SamplesReceived;
        var skippedBytes = _sim.SkippedBytes;
        var received = Interlocked.Read(ref _uiSamplesReceived);
        var applied = Interlocked.Read(ref _uiSamplesApplied);

        var rawBytesDelta = Delta(rawBytes, ref _lastLoggedRawBytes);
        var samplesDelta = Delta(samples, ref _lastLoggedSamples);
        var skippedBytesDelta = Delta(skippedBytes, ref _lastLoggedSkippedBytes);
        var receivedDelta = Delta(received, ref _lastLoggedUiReceived);
        var appliedDelta = Delta(applied, ref _lastLoggedUiApplied);

        AuditLogger.Action(AuditLogger.AdcDynamicDiag, "AdcDynamic",
            $"periodMs={elapsedMs}; bytes={rawBytesDelta}; samples={samplesDelta}; skipped={skippedBytesDelta}; " +
            $"uiReceived={receivedDelta}; uiApplied={appliedDelta}",
            "SimA04", _sim.PortName);
    }

    // Возвращает прирост монотонного счетчика за период диагностики и обновляет предыдущую точку отсчета.
    private static long Delta(long current, ref long previous)
    {
        var delta = current >= previous ? current - previous : current;
        previous = current;
        return delta;
    }

    private void UpdateChannelLabel() => _lblChannel.Text = _sim.Channel == ActiveChannel.Main ? "Канал: Основной (CH0)" : "Канал: Резервный (CH1)";

    private int ActiveCode(SimA04DynamicSample sample) => _sim.Channel == ActiveChannel.Main ? sample.Ch0 : sample.Ch1;

    private static void SetBogieValue(Label label, double tonnes) => label.Text = $"{tonnes:F2} т";

    private void ResetBogieValues()
    {
        _lblBogie1Value.Text = "—";
        _lblBogie2Value.Text = "—";
    }

    private void UpdateDirectionControls()
    {
        bool enabled = _state == WeighState.Idle && _wagonNumber == 0;
        _rbPlus.Enabled = enabled;
        _rbMinus.Enabled = enabled;
    }

    // ── Weighing logic ─────────────────────────────────────────────────────

    private void HandleWeighPress()
    {
        if (!_btnWeigh.Enabled) return;
        RefreshDisplayFromLatestSample();
        if (!ValidateBeforeWeigh()) return;

        if (_state == WeighState.Idle)
        {
            _wagonNumber++;
            if (_wagonNumber == 1)
                _trainStartTime = DateTime.Now;
            _bogie1Code         = ActiveCode(_lastSample);
            double   bogie1Tonnes = ToTonnes(_bogie1Code);
            _state              = WeighState.Bogie1Captured;
            SetBogieValue(_lblBogie1Value, bogie1Tonnes);
            _lblBogie2Value.Text = "—";
            _lblValue.Text      = bogie1Tonnes.ToString("F2");
            _lblValue.ForeColor = UiColors.PendingAction;
            _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 2";
            _btnWeigh.BackColor = UiColors.PendingAction;
        }
        else
        {
            int      bogie2    = ActiveCode(_lastSample);
            DateTime wagonTime = DateTime.Now;
            var record = new LocalWagon
            {
                Number    = _wagonNumber,
                TrainTime = _trainStartTime!.Value,
                WagonTime = wagonTime,
                Bogie1    = ToTonnes(_bogie1Code),
                Bogie2    = ToTonnes(bogie2),
                Direction = GetDirection(),
                Mode      = "ДИНАМИКА",
            };
            AddToGrid(record);
            SaveAsync(record);
            AuditLogger.Action(AuditLogger.WeighingSaved, "LocalWagon", $"вагон №{_wagonNumber} dir={record.Direction} total={record.Total:F2}", "PostgreSQL", _wagonNumber.ToString());
            _state              = WeighState.Idle;
            ResetBogieValues();
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
        RefreshDisplayFromLatestSample();

        double current = ReadRawTonnes(ActiveCode(_lastSample));
        double limit = _settings.Current.OperatorZeroLimitTonnes;
        if (Math.Abs(current) > limit)
        {
            MessageBox.Show($"Ноль можно установить только в пределах {limit:F2} т.", "Ноль", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _zeroOffsetTonnes = current;
        _lblValue.Text = ToTonnes(ActiveCode(_lastSample)).ToString("F2");
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
        AuditLogger.Action(AuditLogger.TrainFinished, "Train", $"ДИНАМИКА вагонов={finishedCount}", "PostgreSQL");
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        bool hasCalibration = HasDynamicCalibration();
        _btnWeigh.Enabled  = hasCalibration;
        _btnZero.Enabled   = _state == WeighState.Idle && hasCalibration;
        _btnFinish.Enabled = _state == WeighState.Idle && _wagonNumber > 0;
        UpdateDirectionControls();
    }

    private void AddToGrid(LocalWagon r)
    {
        _grid.Rows.Insert(0, 
            r.Direction, 
            r.Number.ToString(), 
            r.Bogie1.ToString("F2"), 
            r.Bogie2.ToString("F2"),
            r.Total.ToString("F2"), 
            r.WagonTime.ToString("HH:mm:ss"),
            r.TrainTime.ToString("dd.MM.yyyy"), 
            r.TrainTime.ToString("HH:mm:ss"));
    }

    private async void SaveAsync(LocalWagon record)
    {
        try
        {
            await _ldb.SaveWagonAsync(record);
            _weighingStorageAvailable = true;
            UpdateAdcDiagnostics();
        }
        catch (Exception ex)
        {
            _weighingStorageAvailable = false;
            UpdateAdcDiagnostics();
            AuditLogger.Error(AuditLogger.ErrorDb, "LocalWagon", $"вагон №{record.Number}", "PostgreSQL", ex.Message);
        }
    }

    private void UpdateStorageStatus()
    {
        var audit = AuditLogger.GetQueueStatus();
        var messages = new List<string>();
        if (!HasDynamicCalibration())
            messages.Add($"Динамическая калибровка: не задан коэффициент для {GetDirection()}");
        if (!_weighingStorageAvailable)
            messages.Add("Взвешивание: БД недоступна, запись не сохранена");

        var hasError = !HasDynamicCalibration() || !_weighingStorageAvailable || !audit.IsDatabaseAvailable || audit.DroppedCount > 0;
        if (!audit.IsDatabaseAvailable)
            messages.Add(string.Format("БД недоступна; очередь {0}/{1}; потеряно {2}", audit.PendingCount, AuditLogger.QueueCapacity, audit.DroppedCount));
        else if (audit.PendingCount > 0 || audit.DroppedCount > 0)
            messages.Add(string.Format("синхронизация {0}/{1}; потеряно {2}", audit.PendingCount, AuditLogger.QueueCapacity, audit.DroppedCount));

        _lblStorage.Text = string.Join(" | ", messages);
        _lblStorage.ForeColor = hasError ? Color.Red : UiColors.TextMuted;
    }

}
