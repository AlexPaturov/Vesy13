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
    private const int MaxAdcCode = 65535;

    private enum WeighState { Idle, Bogie1Captured }
    private WeighState _state = WeighState.Idle;
    private DateTime?  _trainStartTime;
    private int        _wagonNumber;
    private int        _bogie1Code;
    private SimA04DynamicSample _lastSample;
    private double     _zeroOffsetTonnes;
    private readonly System.Windows.Forms.Timer _adcDiagTimer = new();

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
        _adcDiagTimer.Interval = 500;
        _adcDiagTimer.Tick += (_, _) => UpdateAdcDiagnostics();
    }

    private string GetDirection() => _rbPlus.Checked ? "→ (+)" : "← (–)";

    private double ReadRawTonnes(int adcCode)
    {
        double k = GetDirection().StartsWith("→") ? _ldb.Dynamic.KPlus : _ldb.Dynamic.KMinus;
        if (Math.Abs(k) < double.Epsilon)
            k = _settings.Current.MaxCapacityTonnes / MaxAdcCode;

        return k * adcCode;
    }

    private double ToTonnes(int adcCode) =>
        WeightFormatter.RoundToDiscretization(
            ReadRawTonnes(adcCode) - _zeroOffsetTonnes,
            _settings.Current.WeightDiscretizationTonnes);

    private bool ValidateBeforeWeigh()
    {
        if (_rbPlus.Checked || _rbMinus.Checked) return true;
        MessageBox.Show("Выберите направление движения состава.", "Взвешивание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        _lblUnit.Font     = UiFonts.UnitLabel;
        _lblUnit.ForeColor = UiColors.TextOnDarkMuted;
        _lblStatus.Font   = UiFonts.Medium;
        _lblStatus.ForeColor = UiColors.TextOnDarkMuted;
        _lblBogie1Caption.Font = UiFonts.Body;
        _lblBogie1Caption.ForeColor = UiColors.TextOnDarkMuted;
        _lblBogie1Value.Font = UiFonts.Medium;
        _lblBogie1Value.ForeColor = UiColors.TextOnDarkMuted;
        _lblBogie2Caption.Font = UiFonts.Body;
        _lblBogie2Caption.ForeColor = UiColors.TextOnDarkMuted;
        _lblBogie2Value.Font = UiFonts.Medium;
        _lblBogie2Value.ForeColor = UiColors.TextOnDarkMuted;
        _lblTrainInfo.Font = UiFonts.Body;
        _lblTrainInfo.ForeColor = UiColors.TextOnDarkMuted;
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

        _grid.Columns.Add(Col("Напр.", 50));
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
        _sim.ConnectionTimeoutMs = 1000;
        SetupGridColumns();
        _sim.SampleReceived    += OnSample;
        _sim.ConnectionChanged += OnConnectionChanged;
        _adcDiagTimer.Start();
        EnsureAdcConnected(showError: true);
        UpdateConn(_sim.IsConnected);
        UpdateChannelLabel();
        ResetBogieValues();
        UpdateTrainInfo();
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
            _sim.SampleReceived    -= OnSample;
            _sim.ConnectionChanged -= OnConnectionChanged;
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
    private void Direction_CheckedChanged(object? sender, EventArgs e) => UpdateTrainInfo();

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

    private void OnSample(object? sender, SimA04DynamicSample sample)
    {
        if (InvokeRequired) 
        { 
            BeginInvoke(() => OnSample(sender, sample)); 
            return; 
        }

        _lastSample = sample;
        if (_state == WeighState.Idle)
        {
            _lblValue.Text      = ToTonnes(ActiveCode(sample)).ToString("F2");
            _lblValue.ForeColor = _sim.IsConnected ? UiColors.PrimaryAction : UiColors.Disconnected;
        }
        else
        {
            SetBogieValue(_lblBogie2Value, ToTonnes(ActiveCode(sample)));
        }
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
        _lblConn.Text      = connected ? $"АЦП: {_sim.PortName}" : "АЦП: отключён";
        if (_state == WeighState.Idle)
            _lblValue.ForeColor = connected ? UiColors.PrimaryAction : UiColors.Disconnected;
        UpdateAdcDiagnostics();
    }

    private void UpdateAdcDiagnostics()
    {
        if (DesignMode || _sim is null) return;

        if (!_sim.IsPortOpen)
        {
            _lblConn.Text = "АЦП: порт закрыт";
            return;
        }

        var state = _sim.IsConnected
            ? $"АЦП: {_sim.PortName}"
            : $"АЦП: нет валидного потока ({_sim.PortName})";
        _lblConn.Text = $"{state}  samples={_sim.SamplesReceived} raw={_sim.RawBytesReceived} skipped={_sim.SkippedBytes}";
    }

    private void UpdateChannelLabel() =>
        _lblChannel.Text = _sim.Channel == ActiveChannel.Main ? "Канал: Основной (CH0)" : "Канал: Резервный (CH1)";

    private int ActiveCode(SimA04DynamicSample sample) => _sim.Channel == ActiveChannel.Main ? sample.Ch0 : sample.Ch1;

    private static void SetBogieValue(Label label, double tonnes) => label.Text = $"{tonnes:F2} т";

    private void ResetBogieValues()
    {
        _lblBogie1Value.Text = "—";
        _lblBogie2Value.Text = "—";
    }

    private void UpdateTrainInfo()
    {
        string direction = GetDirection();
        _lblTrainInfo.Text = _wagonNumber == 0
            ? $"Состав: —\n{direction}"
            : $"Состав: {_wagonNumber} ваг.\n{direction}";
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
            _lblStatus.Text     = $"Вагон №{_wagonNumber}: тележка 1 зафиксирована  —  Ожидание тележки 2";
            UpdateTrainInfo();
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
            AuditLogger.Action(AuditLogger.WeighingSaved,
                "LocalWagon", $"вагон №{_wagonNumber} dir={record.Direction} total={record.Total:F2}",
                "PostgreSQL", _wagonNumber.ToString());
            _state              = WeighState.Idle;
            SetBogieValue(_lblBogie1Value, record.Bogie1);
            SetBogieValue(_lblBogie2Value, record.Bogie2);
            _lblValue.Text      = record.Total.ToString("F2");
            _lblValue.ForeColor = UiColors.Info;
            _lblStatus.Text     = $"Вагон №{_wagonNumber}: {record.Total:F2} т  —  Готов к следующему";
            UpdateTrainInfo();
            _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
            _btnWeigh.BackColor = UiColors.PrimaryAction;
        }
        UpdateButtonStates();
    }

    private void OnZeroClick()
    {
        if (!ValidateBeforeWeigh()) return;

        double current = ReadRawTonnes(ActiveCode(_lastSample));
        double limit = _settings.Current.OperatorZeroLimitTonnes;
        if (Math.Abs(current) > limit)
        {
            MessageBox.Show($"Ноль можно установить только в пределах {limit:F2} т.", "Ноль",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        _lblStatus.Text     = "Готов к взвешиванию  —  Тележка 1";
        ResetBogieValues();
        UpdateTrainInfo();
        _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
        _btnWeigh.BackColor = UiColors.PrimaryAction;
        AuditLogger.Action(AuditLogger.TrainFinished,
            "Train", $"ДИНАМИКА вагонов={finishedCount}", "PostgreSQL");
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        _btnZero.Enabled   = _state == WeighState.Idle;
        _btnFinish.Enabled = _state == WeighState.Idle && _wagonNumber > 0;
        UpdateDirectionControls();
    }

    private void AddToGrid(LocalWagon r)
    {
        _grid.Rows.Insert(0, r.Direction, r.Number.ToString(),
            r.Bogie1.ToString("F2"), r.Bogie2.ToString("F2"),
            r.Total.ToString("F2"), r.WagonTime.ToString("HH:mm:ss"),
            r.TrainTime.ToString("dd.MM.yyyy"), r.TrainTime.ToString("HH:mm:ss"));
        while (_grid.Rows.Count > 10)
            _grid.Rows.RemoveAt(_grid.Rows.Count - 1);
    }

    private async void SaveAsync(LocalWagon record)
    {
        try
        {
            await _ldb.SaveWagonAsync(record);
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb,
                "LocalWagon", $"вагон №{record.Number}", "PostgreSQL", ex.Message);
            MessageBox.Show("Не удалось сохранить результат взвешивания.\nОбратитесь к администратору.",
                "База данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
