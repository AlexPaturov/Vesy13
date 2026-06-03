using System.Globalization;
using Vesy13.Models;
using Vesy13.Services;

namespace Vesy13.Forms;

public class CorrectionsForm : Form
{
    private readonly DatabaseService _db;
    private readonly FirebirdService _fb = new();

    private DataGridView _grid        = null!;
    private RadioButton  _rbGpri      = null!;
    private RadioButton  _rbGras      = null!;
    private TextBox      _txtNvag     = null!;
    private TextBox      _txtNdok     = null!;
    private TextBox      _txtGruz     = null!;
    private TextBox      _txtTar      = null!;
    private Label        _lblNetto    = null!;
    private Button       _btnTransfer = null!;

    public CorrectionsForm(DatabaseService db)
    {
        _db = db;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text          = "Корректировки — перенос в учётную систему";
        ClientSize    = new Size(780, 510);
        MinimumSize   = new Size(680, 470);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor     = Color.FromArgb(240, 242, 245);

        // ── Grid ────────────────────────────────────────────────────────────
        _grid = new DataGridView
        {
            Location  = new Point(8, 8),
            Size      = new Size(764, 256),
            Anchor    = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            ReadOnly  = true,
            AllowUserToAddRows    = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            RowHeadersVisible     = false,
            MultiSelect           = false,
            SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
            BackgroundColor       = Color.White,
            BorderStyle           = BorderStyle.None,
            Font                  = new Font("Segoe UI", 9),
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
            ColumnHeadersHeight   = 26,
        };
        _grid.RowTemplate.Height = 22;
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата",     Width = 84,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Время",    Width = 66,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "№",        Width = 36,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тел.1 т",  Width = 82,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тел.2 т",  Width = 82,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Брутто т", Width = 82,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Режим",    Width = 90,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Напр.",    Width = 64,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.RowsDefaultCellStyle.BackColor            = Color.White;
        _grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 255);
        _grid.SelectionChanged += Grid_SelectionChanged;

        // ── Edit panel ───────────────────────────────────────────────────────
        var pnlEdit = new Panel
        {
            Location  = new Point(8, 272),
            Size      = new Size(764, 168),
            Anchor    = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            BackColor = Color.FromArgb(225, 231, 245),
        };

        _rbGpri = new RadioButton
        {
            Text = "Приход  (GPRI)", Location = new Point(12, 10),
            AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), Checked = true,
        };
        _rbGras = new RadioButton
        {
            Text = "Расход  (GRAS)", Location = new Point(210, 10),
            AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold),
        };

        // Input rows
        int y = 42, step = 28;
        const int lx = 12, tx = 200, tw = 180;

        Label Lbl(string t) { var l = new Label { Text = t, Location = new Point(lx, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9) }; return l; }
        TextBox Txt()       { var t = new TextBox { Location = new Point(tx, y), Size = new Size(tw, 24), Font = new Font("Segoe UI", 9) }; return t; }

        var lblNvag = Lbl("Номер вагона (NVAG):"); _txtNvag = Txt(); y += step;
        var lblNdok = Lbl("Номер документа:");     _txtNdok = Txt(); y += step;
        var lblGruz = Lbl("Наименование груза:");  _txtGruz = Txt(); y += step;
        var lblTar  = Lbl("Тара по документу (т):");
        _txtTar = new TextBox { Location = new Point(tx, y), Size = new Size(100, 24), Font = new Font("Segoe UI", 9) };
        _txtTar.TextChanged += (_, _) => RecalcNetto();

        var lblNettoTitle = new Label { Text = "Нетто:", Location = new Point(tx + 112, y + 3), AutoSize = true, Font = new Font("Segoe UI", 9) };
        _lblNetto = new Label
        {
            Text = "—", Location = new Point(tx + 166, y + 1),
            AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(0, 90, 30),
        };

        _btnTransfer = new Button
        {
            Text = "Перенести в Firebird", Location = new Point(12, 136), Size = new Size(210, 28),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10),
            BackColor = Color.FromArgb(0, 110, 40), ForeColor = Color.White, Enabled = false,
        };
        _btnTransfer.FlatAppearance.BorderSize = 0;
        _btnTransfer.Click += BtnTransfer_Click;

        var btnRefresh = new Button
        {
            Text = "Обновить список", Location = new Point(234, 136), Size = new Size(160, 28),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9),
        };
        btnRefresh.Click += async (_, _) => await LoadGridAsync();

        pnlEdit.Controls.AddRange(new Control[]
        {
            _rbGpri, _rbGras,
            lblNvag, _txtNvag,
            lblNdok, _txtNdok,
            lblGruz, _txtGruz,
            lblTar, _txtTar, lblNettoTitle, _lblNetto,
            _btnTransfer, btnRefresh,
        });

        // ── Status bar ───────────────────────────────────────────────────────
        var pnlStatus = new Panel { Dock = DockStyle.Bottom, Height = 34, BackColor = Color.FromArgb(18, 32, 65) };
        var btnBack   = new Button
        {
            Text = "← Назад", Location = new Point(8, 6), Size = new Size(80, 22),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 8),
            BackColor = Color.FromArgb(40, 70, 130), ForeColor = Color.White,
        };
        btnBack.FlatAppearance.BorderSize = 0;
        btnBack.Click += (_, _) => Close();
        pnlStatus.Controls.Add(btnBack);

        Controls.AddRange(new Control[] { _grid, pnlEdit, pnlStatus });
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await LoadGridAsync();
    }

    private async Task LoadGridAsync()
    {
        try
        {
            var rows = await _db.GetPendingAsync();
            _grid.Rows.Clear();
            foreach (var r in rows)
            {
                int idx = _grid.Rows.Add(
                    r.WagonTime.ToString("dd.MM.yyyy"),
                    r.WagonTime.ToString("HH:mm:ss"),
                    r.WagonNum,
                    r.Bogie1.ToString("F2"),
                    r.Bogie2.ToString("F2"),
                    r.Total.ToString("F2"),
                    r.Mode,
                    r.Direction);
                _grid.Rows[idx].Tag = r;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки из БД:\n{ex.Message}", "База данных",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void Grid_SelectionChanged(object? sender, EventArgs e)
    {
        bool hasRow = _grid.SelectedRows.Count > 0;
        _btnTransfer.Enabled = hasRow;
        ClearEditFields();
        if (hasRow) RecalcNetto();
    }

    private void RecalcNetto()
    {
        if (_grid.SelectedRows.Count == 0) return;
        if (_grid.SelectedRows[0].Tag is not WagonWeighingRow row) return;

        _lblNetto.Text = decimal.TryParse(_txtTar.Text, NumberStyles.Float,
            CultureInfo.InvariantCulture, out decimal tar)
            ? (row.Total - tar).ToString("F2") + " т"
            : row.Total.ToString("F2") + " т";
    }

    private async void BtnTransfer_Click(object? sender, EventArgs e)
    {
        if (_grid.SelectedRows.Count == 0) return;
        if (_grid.SelectedRows[0].Tag is not WagonWeighingRow row) return;

        string nvag = _txtNvag.Text.Trim();
        if (string.IsNullOrEmpty(nvag))
        {
            MessageBox.Show("Введите номер вагона (NVAG).", "Перенос",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtNvag.Focus();
            return;
        }

        long?    ndok   = long.TryParse(_txtNdok.Text.Trim(), out long nd) ? nd : null;
        string?  gruz   = string.IsNullOrWhiteSpace(_txtGruz.Text) ? null : _txtGruz.Text.Trim();
        decimal? tarDok = decimal.TryParse(_txtTar.Text, NumberStyles.Float,
            CultureInfo.InvariantCulture, out decimal td) ? td : null;
        string table = _rbGpri.Checked ? "GPRI" : "GRAS";

        _btnTransfer.Enabled = false;
        try
        {
            await _fb.InsertAsync(table, row, nvag, ndok, gruz, tarDok);
            await _db.MarkTransferredAsync(row.Id);

            var selected = _grid.SelectedRows[0];
            _grid.Rows.Remove(selected);
            ClearEditFields();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка переноса:\n{ex.Message}", "Перенос",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            _btnTransfer.Enabled = true;
        }
    }

    private void ClearEditFields()
    {
        _txtNvag.Clear();
        _txtNdok.Clear();
        _txtGruz.Clear();
        _txtTar.Clear();
        _lblNetto.Text = "—";
    }
}