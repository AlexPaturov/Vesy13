using Vesy13.Application;
using Vesy13.Models;
using Vesy13.Services.Hardware;
using Vesy13.Services.Repositories;

namespace Vesy13.Forms;

public partial class StaticWeighingForm : Form
{
    private SimA04Reader    _adc = null!;
    private LocalRepository _db  = null!;

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

    public StaticWeighingForm(SimA04Reader adc, LocalRepository db)
    {
        _adc = adc;
        _db  = db;
        InitializeComponent();
    }

    private double ToTonnes(int adcCode) =>
        CalibrationCalculator.Convert(_db.Profile, adcCode, _adc.Channel);

    private void SetupGridColumns()
    {
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "№",     Width = 38,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тел.1", Width = 90,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тел.2", Width = 90,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Вагон", Width = 90,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Время", Width = 100, SortMode = DataGridViewColumnSortMode.NotSortable });
    }

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

    // ── Designer event handlers ────────────────────────────────────────────

    private void BtnWeigh_Click(object? sender, EventArgs e)  => HandleWeighPress();
    private void BtnZero_Click(object? sender, EventArgs e)   => OnZeroClick();
    private void BtnFinish_Click(object? sender, EventArgs e) => HandleFinish();
    private void BtnBack_Click(object? sender, EventArgs e)   => Close();

    // ── ADC events ─────────────────────────────────────────────────────────

    private void OnFrame(object? sender, SimA04Frame frame)
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

    private void UpdateChannelLabel() =>
        _lblChannel.Text = _adc.Channel == ActiveChannel.Main
            ? "Канал: Основной (CH0)"
            : "Канал: Резервный (CH1)";

    private int ActiveCode(SimA04Frame f) =>
        _adc.Channel == ActiveChannel.Main ? f.Ch0 : f.Ch1;

    // ── Weighing logic ─────────────────────────────────────────────────────

    private void HandleWeighPress()
    {
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
        _btnZero.Enabled   = _state == WeighState.Idle;
        _btnFinish.Enabled = _state == WeighState.Idle && _wagonNumber > 0;
    }

    private void AddToGrid(LocalWagon r)
    {
        _grid.Rows.Insert(0, r.Number.ToString(),
            r.Bogie1.ToString("F2"), r.Bogie2.ToString("F2"),
            r.Total.ToString("F2"), r.WagonTime.ToString("HH:mm:ss"));
        while (_grid.Rows.Count > 10)
            _grid.Rows.RemoveAt(_grid.Rows.Count - 1);
    }

    private async void SaveAsync(LocalWagon record)
    {
        try   { await _db.SaveWagonAsync(record); }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения в БД:\n{ex.Message}",
                "База данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
