using Vesy13.Application;
using Vesy13.Models;
using Vesy13.Services.Hardware;
using Vesy13.Services.Repositories;

namespace Vesy13.Forms;

/// <summary>
/// Форма статического взвешивания: вагон стоит, оператор последовательно фиксирует
/// две тележки нажатием пробела, результат сохраняется в PostgreSQL.
/// </summary>
public partial class StaticWeighingForm : Form
{
    private SimA04Reader    _sim = null!;
    private LocalRepository _ldb  = null!;

    private enum WeighState { Idle, Bogie1Captured }
    private WeighState _state = WeighState.Idle;
    private DateTime?  _trainStartTime;
    private int        _wagonNumber;
    private int        _bogie1Code;
    private SimA04Frame   _lastFrame;

    public StaticWeighingForm()
    {
        InitializeComponent();
    }

    public StaticWeighingForm(SimA04Reader sim, LocalRepository ldb)
    {
        _sim = sim;
        _ldb  = ldb;
        InitializeComponent();
    }

    private double ToTonnes(int adcCode) =>
        CalibrationCalculator.Convert(_ldb.Profile, adcCode, _sim.Channel);

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
        if (DesignMode || _sim is null) return;
        AuditLogger.Action(AuditLogger.FormOpened, "Form", "StaticWeighingForm");
        SetupGridColumns();
        _sim.FrameReceived     += OnFrame;
        _sim.ConnectionChanged += OnConnectionChanged;
        UpdateConn(_sim.IsConnected);
        UpdateChannelLabel();
        UpdateButtonStates();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (!DesignMode && _sim is not null)
        {
            _sim.FrameReceived     -= OnFrame;
            _sim.ConnectionChanged -= OnConnectionChanged;
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
    private void BtnBack_Click(object? sender, EventArgs e)   => Close();

    // ── ADC events ─────────────────────────────────────────────────────────

    private void OnFrame(object? sender, SimA04Frame frame)
    {
        if (InvokeRequired) 
        { 
            BeginInvoke(() => OnFrame(sender, frame)); 
            return; 
        }

        _lastFrame = frame;
        if (_state == WeighState.Idle)
        {
            _lblValue.Text      = ToTonnes(ActiveCode(frame)).ToString("F2");
            _lblValue.ForeColor = _sim.IsConnected ? Color.LimeGreen : Color.DimGray;
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
        _dotConn.BackColor = connected ? Color.LimeGreen : Color.Gray;
        _lblConn.Text      = connected ? $"АЦП: {_sim.PortName}" : "АЦП: отключён";
        if (_state == WeighState.Idle)
            _lblValue.ForeColor = connected ? Color.LimeGreen : Color.DimGray;
    }

    private void UpdateChannelLabel() => _lblChannel.Text = _sim.Channel == ActiveChannel.Main ? "Канал: Основной (CH0)" : "Канал: Резервный (CH1)";

    private int ActiveCode(SimA04Frame f) => _sim.Channel == ActiveChannel.Main ? f.Ch0 : f.Ch1;

    // ── Weighing logic ─────────────────────────────────────────────────────

    private void HandleWeighPress()
    {
        if (!_btnWeigh.Enabled) return;
        if (_state == WeighState.Idle)
        {
            _wagonNumber++;
            if (_wagonNumber == 1)
                _trainStartTime = DateTime.Now;
            _bogie1Code         = ActiveCode(_lastFrame);
            _state              = WeighState.Bogie1Captured;
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
            var record = new LocalWagon
            {
                Number    = _wagonNumber,
                TrainTime = _trainStartTime!.Value,
                WagonTime = wagonTime,
                Bogie1    = ToTonnes(_bogie1Code),
                Bogie2    = ToTonnes(bogie2),
                Mode      = "СТАТИКА",
            };
            AddToGrid(record);
            SaveAsync(record);
            AuditLogger.Action(AuditLogger.WeighingSaved,
                "LocalWagon", $"вагон №{_wagonNumber} total={record.Total:F2}",
                "PostgreSQL", _wagonNumber.ToString());
            _state              = WeighState.Idle;
            _lblValue.Text      = record.Total.ToString("F2");
            _lblValue.ForeColor = Color.Cyan;
            _lblStatus.Text     = $"Вагон №{_wagonNumber}: {record.Total:F2} т  —  Готов к следующему";
            _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
            _btnWeigh.BackColor = Color.FromArgb(0, 130, 0);
        }
        UpdateButtonStates();
    }

    private void OnZeroClick() =>
        MessageBox.Show("Ноль установлен (в разработке)", "Ноль",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

    private void HandleFinish()
    {
        if (_wagonNumber == 0) return;
        if (MessageBox.Show($"Завершить состав?\nВзвешено вагонов: {_wagonNumber}",
                "Завершить состав", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        int finishedCount   = _wagonNumber;
        _trainStartTime     = null;
        _wagonNumber        = 0;
        _state              = WeighState.Idle;
        _grid.Rows.Clear();
        _lblValue.Text      = "—";
        _lblValue.ForeColor = _sim.IsConnected ? Color.LimeGreen : Color.DimGray;
        _lblStatus.Text     = "Готов к взвешиванию  —  Тележка 1";
        _btnWeigh.Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1";
        _btnWeigh.BackColor = Color.FromArgb(0, 130, 0);
        AuditLogger.Action(AuditLogger.TrainFinished,
            "Train", $"СТАТИКА вагонов={finishedCount}", "PostgreSQL");
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        _btnZero.Enabled   = _state == WeighState.Idle;
        _btnFinish.Enabled = _state == WeighState.Idle && _wagonNumber > 0;
    }

    private void AddToGrid(LocalWagon r)
    {
        _grid.Rows.Insert(0, r.Number.ToString(),
            r.Bogie1.ToString("F2"), r.Bogie2.ToString("F2"),
            r.Total.ToString("F2"), r.WagonTime.ToString("HH:mm:ss"),
            r.TrainTime.ToString("dd.MM.yyyy"), r.TrainTime.ToString("HH:mm:ss"));
        while (_grid.Rows.Count > 10)
            _grid.Rows.RemoveAt(_grid.Rows.Count - 1);
    }

    private async void SaveAsync(LocalWagon record)
    {
        try   { await _ldb.SaveWagonAsync(record); }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb,
                "LocalWagon", $"вагон №{record.Number}", "PostgreSQL", ex.Message);
            MessageBox.Show("Не удалось сохранить результат взвешивания.\nОбратитесь к администратору.",
                "База данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
