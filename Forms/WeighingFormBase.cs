using Vesy13.Models;
using Vesy13.Services;

namespace Vesy13.Forms;

public abstract class WeighingFormBase : Form
{
    protected readonly AdcService         _adc;
    protected readonly CalibrationService _calib;
    protected readonly DatabaseService    _db;

    // ── State machine ──────────────────────────────────────────────────────
    private enum WeighState { Idle, Bogie1Captured }
    private WeighState _state = WeighState.Idle;

    private DateTime? _trainStartTime;
    private int       _wagonNumber;
    private int       _bogie1Code;

    protected AdcFrame _lastFrame;

    // ── Shared controls ────────────────────────────────────────────────────
    protected Label        _lblValue   = null!;
    protected Label        _lblStatus  = null!;
    protected Button       _btnWeigh   = null!;
    protected Button       _btnZero    = null!;
    protected Button       _btnFinish  = null!;
    protected Label        _lblChannel = null!;
    private   DataGridView _grid       = null!;
    private   Panel        _dotConn    = null!;
    private   Label        _lblConn    = null!;

    // ── Abstract interface ─────────────────────────────────────────────────
    protected abstract string GetDirection();
    protected abstract bool   ValidateBeforeWeigh();
    protected abstract bool   ShowDirectionColumn { get; }
    protected abstract double ToTonnes(int adcCode);
    protected abstract string GetMode();

    protected WeighingFormBase(AdcService adc, CalibrationService calib, DatabaseService db)
    {
        _adc   = adc;
        _calib = calib;
        _db    = db;
    }

    // ── Shared UI (call from derived InitializeComponent) ──────────────────
    protected void BuildSharedLayout(int topY)
    {
        var pnlDisplay = new Panel
        {
            Location  = new Point(8, topY),
            Size      = new Size(544, 158),
            BackColor = Color.Black,
        };
        _lblValue = new Label
        {
            Text      = "—",
            Font      = new Font("Courier New", 60, FontStyle.Bold),
            ForeColor = Color.DimGray,
            TextAlign = ContentAlignment.MiddleRight,
            Bounds    = new Rectangle(8, 4, 450, 106),
            AutoSize  = false,
        };
        var lblUnit = new Label
        {
            Text      = "т",
            Font      = new Font("Segoe UI", 20),
            ForeColor = Color.Gray,
            TextAlign = ContentAlignment.BottomLeft,
            Bounds    = new Rectangle(462, 60, 60, 62),
            AutoSize  = false,
        };
        _lblStatus = new Label
        {
            Text      = "Готов к взвешиванию  —  Тележка 1",
            Font      = new Font("Segoe UI", 10),
            ForeColor = Color.Silver,
            TextAlign = ContentAlignment.MiddleCenter,
            Bounds    = new Rectangle(8, 118, 528, 34),
            AutoSize  = false,
        };
        pnlDisplay.Controls.AddRange(new Control[] { _lblValue, lblUnit, _lblStatus });

        int y = topY + 158 + 6;

        _btnWeigh = new Button
        {
            Location  = new Point(8, y),
            Size      = new Size(544, 54),
            Text      = "ВЗВЕСИТЬ   [Пробел]   —   Тележка 1",
            Font      = new Font("Segoe UI", 14, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(0, 130, 0),
            ForeColor = Color.White,
        };
        _btnWeigh.Click += (_, _) => HandleWeighPress();
        y += 54 + 4;

        _btnZero = new Button
        {
            Location  = new Point(8, y),
            Size      = new Size(100, 32),
            Text      = "Ноль",
            Font      = new Font("Segoe UI", 10),
            FlatStyle = FlatStyle.Flat,
        };
        _btnZero.Click += (_, _) => OnZeroClick();

        _btnFinish = new Button
        {
            Location  = new Point(116, y),
            Size      = new Size(244, 32),
            Text      = "Завершить состав",
            Font      = new Font("Segoe UI", 10),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(120, 40, 0),
            ForeColor = Color.White,
        };
        _btnFinish.Click += (_, _) => HandleFinish();
        y += 32 + 6;

        _grid          = BuildGrid();
        _grid.Location = new Point(8, y);
        _grid.Size     = new Size(544, 10 * 22 + 28);

        var pnlStatus = new Panel
        {
            Dock      = DockStyle.Bottom,
            Height    = 34,
            BackColor = Color.FromArgb(18, 32, 65),
        };
        var btnBack = new Button
        {
            Text      = "← Назад",
            Location  = new Point(8, 6),
            Size      = new Size(80, 22),
            FlatStyle = FlatStyle.Flat,
            Font      = new Font("Segoe UI", 8),
            BackColor = Color.FromArgb(40, 70, 130),
            ForeColor = Color.White,
        };
        btnBack.FlatAppearance.BorderSize = 0;
        btnBack.Click += (_, _) => Close();
        _dotConn = new Panel { Size = new Size(10, 10), Location = new Point(100, 12), BackColor = Color.Gray };
        _lblConn = new Label { Text = "АЦП: —", Font = new Font("Segoe UI", 9), ForeColor = Color.Silver, Location = new Point(116, 8), AutoSize = true };
        pnlStatus.Controls.AddRange(new Control[] { btnBack, _dotConn, _lblConn });

        Controls.AddRange(new Control[] { pnlDisplay, _btnWeigh, _btnZero, _btnFinish, _grid, pnlStatus });
    }

    private DataGridView BuildGrid()
    {
        var g = new DataGridView
        {
            ReadOnly                    = true,
            AllowUserToAddRows          = false,
            AllowUserToDeleteRows       = false,
            AllowUserToResizeRows       = false,
            RowHeadersVisible           = false,
            SelectionMode               = DataGridViewSelectionMode.FullRowSelect,
            BackgroundColor             = Color.White,
            BorderStyle                 = BorderStyle.None,
            Font                        = new Font("Segoe UI", 9),
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
            ColumnHeadersHeight         = 28,
        };
        g.RowTemplate.Height = 22;

        if (ShowDirectionColumn)
            g.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Напр.", Width = 52,  SortMode = DataGridViewColumnSortMode.NotSortable });

        g.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "№",     Width = 38,  SortMode = DataGridViewColumnSortMode.NotSortable });
        g.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тел.1", Width = 90,  SortMode = DataGridViewColumnSortMode.NotSortable });
        g.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тел.2", Width = 90,  SortMode = DataGridViewColumnSortMode.NotSortable });
        g.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Вагон", Width = 90,  SortMode = DataGridViewColumnSortMode.NotSortable });
        g.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Время", Width = ShowDirectionColumn ? 80 : 100, SortMode = DataGridViewColumnSortMode.NotSortable });

        g.RowsDefaultCellStyle.BackColor            = Color.White;
        g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 245, 255);
        return g;
    }

    // ── Lifecycle ──────────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _adc.FrameReceived     += OnFrame;
        _adc.ConnectionChanged += OnConnectionChanged;
        UpdateConn(_adc.IsConnected);
        UpdateChannelLabel();
        UpdateButtonStates();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _adc.FrameReceived     -= OnFrame;
        _adc.ConnectionChanged -= OnConnectionChanged;
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
        // TODO: сохранить смещение нуля в ZeroService
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
