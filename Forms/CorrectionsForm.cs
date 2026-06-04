using System.ComponentModel;
using System.Globalization;
using Vesy13.Models;
using Vesy13.Services;

namespace Vesy13.Forms;

[DesignerCategory("Code")]
public class CorrectionsForm : Form
{
    private readonly DatabaseService _db;
    private readonly FirebirdService _fb = new();

    // ── Верхняя панель ─────────────────────────────────────────────────────
    private RadioButton _rbGpri     = null!;
    private RadioButton _rbGras     = null!;
    private Label       _lblDt      = null!;
    private Label       _lblVr      = null!;
    private Label       _lblNpp     = null!;
    private Label       _lblMode    = null!;
    private Label       _lblDir     = null!;
    private Label       _lblBrutto  = null!;
    private Label       _lblNetto   = null!;
    private TextBox     _txtNvag    = null!;
    private TextBox     _txtGruz    = null!;
    private TextBox     _txtNdok    = null!;
    private TextBox     _txtTar     = null!;
    private Button      _btnTransfer = null!;

    // ── Таблицы ────────────────────────────────────────────────────────────
    private DataGridView _gridPend = null!;
    private DataGridView _gridDone = null!;

    private WagonWeighingRow? _selected;

    public static readonly Dictionary<string, string> GpGridChapters = new()
    {
        { "DT",          "Дата взв."    },
        { "VR",          "Время взв."   },
        { "NVAG",        "Ном. вагона"  },
        { "NDOK",        "Ном. докум."  },
        { "GRUZ",        "Груз"         },
        { "BRUTTO",      "Брутто"       },
        { "TAR_BRS",     "Тара факт"    },
        { "TAR_DOK",     "Тара док."    },
        { "NETTO",       "Нетто"        },
        { "NET_DOK",     "Нетто док."   },
        { "MUSOR",       "Мусор"        },
        { "CEX",         "Цех"          },
        { "TARIF",       "Тариф"        },
        { "POTR",        "Поставщик"    },
        { "PLAT",        "Плательщик"   },
        { "SKOR",        "Скорость"     },
        { "VESY",        "Весы"         },
        { "TN",          "Таб. ном."    },
        { "MPP",         "МПП"          },
        { "N_TEPLOVOZ",  "Ном. теплов." },
        { "POGRESHNOST", "Погрешность"  },
    };

    public CorrectionsForm(DatabaseService db)
    {
        _db = db;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text          = "Корректировки — перенос в учётную систему";
        ClientSize    = new Size(1050, 660);
        MinimumSize   = new Size(860, 560);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor     = Color.FromArgb(240, 242, 245);

        BuildTopPanel();
        BuildBottomTables();

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
        Controls.Add(pnlStatus);
    }

    // ── Верхняя панель ──────────────────────────────────────────────────────

    private void BuildTopPanel()
    {
        var pnlTop = new Panel
        {
            Location  = new Point(0, 0),
            Size      = new Size(1050, 168),
            Anchor    = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            BackColor = Color.FromArgb(215, 225, 248),
        };

        // Левая жёлтая полоса
        var pnlLeft = new Panel
        {
            Location  = new Point(0, 0),
            Size      = new Size(138, 168),
            BackColor = Color.FromArgb(190, 162, 38),
        };
        pnlLeft.Controls.Add(new Label
        {
            Text = "Вид взвешивания", Location = new Point(6, 8),
            AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.White,
        });
        _rbGpri = new RadioButton
        {
            Text = "Приход (GPRI)", Location = new Point(6, 36), Checked = true,
            AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.White,
        };
        _rbGras = new RadioButton
        {
            Text = "Расход (GRAS)", Location = new Point(6, 68),
            AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.White,
        };
        pnlLeft.Controls.AddRange(new Control[] { _rbGpri, _rbGras });

        // Поля справа
        const int rx = 148;

        // Строка 1: дата/время/номер/режим
        Add(pnlTop, FieldLabel("Дата:",   rx,       10)); _lblDt   = ValueLabel(rx + 43,  10); Add(pnlTop, _lblDt);
        Add(pnlTop, FieldLabel("Время:",  rx + 140, 10)); _lblVr   = ValueLabel(rx + 190, 10); Add(pnlTop, _lblVr);
        Add(pnlTop, FieldLabel("№п/п:",   rx + 310, 10)); _lblNpp  = ValueLabel(rx + 352, 10); Add(pnlTop, _lblNpp);
        Add(pnlTop, FieldLabel("Режим:",  rx + 430, 10)); _lblMode = ValueLabel(rx + 478, 10); Add(pnlTop, _lblMode);

        // Строка 2: номер вагона + направление
        Add(pnlTop, FieldLabel("№ вагона:", rx, 42));
        _txtNvag = Input(rx + 80, 38, 170); Add(pnlTop, _txtNvag);
        Add(pnlTop, FieldLabel("Направление:", rx + 276, 42)); _lblDir = ValueLabel(rx + 374, 42); Add(pnlTop, _lblDir);

        // Строка 3: груз + № документа
        Add(pnlTop, FieldLabel("Груз:", rx, 70));
        _txtGruz = Input(rx + 48, 66, 270); Add(pnlTop, _txtGruz);
        Add(pnlTop, FieldLabel("№ документа:", rx + 340, 70));
        _txtNdok = Input(rx + 442, 66, 130); Add(pnlTop, _txtNdok);

        // Строка 4: брутто / тара / нетто
        Add(pnlTop, FieldLabel("Брутто:", rx, 98));
        _lblBrutto = new Label { Location = new Point(rx + 58, 97), AutoSize = true, Font = new Font("Courier New", 10, FontStyle.Bold), ForeColor = Color.FromArgb(20, 20, 80) };
        _lblBrutto.Text = "—";
        Add(pnlTop, _lblBrutto);
        Add(pnlTop, FieldLabel("т", rx + 134, 98));
        Add(pnlTop, FieldLabel("Тара:", rx + 158, 98));
        _txtTar = Input(rx + 202, 94, 90);
        _txtTar.TextChanged += (_, _) => RecalcNetto();
        Add(pnlTop, _txtTar);
        Add(pnlTop, FieldLabel("т", rx + 300, 98));
        Add(pnlTop, FieldLabel("Нетто:", rx + 320, 98));
        _lblNetto = new Label { Location = new Point(rx + 372, 97), AutoSize = true, Font = new Font("Courier New", 10, FontStyle.Bold), ForeColor = Color.FromArgb(0, 110, 30) };
        _lblNetto.Text = "—";
        Add(pnlTop, _lblNetto);
        Add(pnlTop, FieldLabel("т", rx + 440, 98));

        // Строка 5: кнопки
        _btnTransfer = new Button
        {
            Text = "Перенести в Firebird  ▶", Location = new Point(rx, 128), Size = new Size(240, 32),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold),
            BackColor = Color.FromArgb(0, 120, 40), ForeColor = Color.White, Enabled = false,
        };
        _btnTransfer.FlatAppearance.BorderSize = 0;
        _btnTransfer.Click += BtnTransfer_Click;

        var btnClear = new Button
        {
            Text = "Очистить", Location = new Point(rx + 254, 128), Size = new Size(110, 32),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9),
        };
        btnClear.Click += (_, _) => ClearTopPanel();

        var btnRefresh = new Button
        {
            Text = "Обновить", Location = new Point(rx + 378, 128), Size = new Size(110, 32),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9),
        };
        btnRefresh.Click += async (_, _) => await LoadBothGridsAsync();

        pnlTop.Controls.AddRange(new Control[] { pnlLeft, _btnTransfer, btnClear, btnRefresh });
        Controls.Add(pnlTop);
    }

    private static void Add(Control parent, Control child) => parent.Controls.Add(child);

    private static Label FieldLabel(string text, int x, int y) => new()
    {
        Text = text, Location = new Point(x, y + 3),
        AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(60, 60, 80),
    };

    private static Label ValueLabel(int x, int y) => new()
    {
        Text = "—", Location = new Point(x, y + 1),
        AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(20, 20, 80),
    };

    private static TextBox Input(int x, int y, int w) => new()
    {
        Location = new Point(x, y), Size = new Size(w, 24), Font = new Font("Segoe UI", 9),
    };

    // ── Нижние таблицы ──────────────────────────────────────────────────────

    private void BuildBottomTables()
    {
        var split = new SplitContainer
        {
            Location         = new Point(0, 168),
            Size             = new Size(1050, 660 - 168 - 34),
            Anchor           = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            Orientation      = Orientation.Vertical,
            SplitterDistance = 524,
            Panel1MinSize    = 280,
            Panel2MinSize    = 280,
            BackColor        = Color.FromArgb(18, 32, 65),
            SplitterWidth    = 4,
        };

        _gridPend = MakeGrid();
        _gridDone = MakeGrid();
        AddGridColumns(_gridPend);
        AddGridColumns(_gridDone);
        _gridPend.SelectionChanged += GridPend_SelectionChanged;

        split.Panel1.Controls.Add(TableHeader("Не перенесённые"));
        split.Panel1.Controls.Add(_gridPend);
        split.Panel2.Controls.Add(TableHeader("Перенесённые"));
        split.Panel2.Controls.Add(_gridDone);

        Controls.Add(split);
    }

    private static DataGridView MakeGrid()
    {
        var g = new DataGridView
        {
            Dock      = DockStyle.Fill,
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
            ColumnHeadersHeight   = 24,
        };
        g.RowTemplate.Height = 22;
        g.RowsDefaultCellStyle.BackColor            = Color.White;
        g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 255);
        return g;
    }

    private static void AddGridColumns(DataGridView g)
    {
        foreach (var (key, header) in GpGridChapters)
            g.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name       = key,
                HeaderText = header,
                Width      = ColumnWidth(key),
                SortMode   = DataGridViewColumnSortMode.NotSortable,
            });
    }

    private static int ColumnWidth(string key) => key switch
    {
        "DT"          => 90,
        "VR"          => 70,
        "NVAG"        => 90,
        "NDOK"        => 75,
        "GRUZ"        => 120,
        "BRUTTO"      => 70,
        "TAR_BRS"     => 70,
        "TAR_DOK"     => 70,
        "NETTO"       => 70,
        "NET_DOK"     => 70,
        "MUSOR"       => 60,
        "CEX"         => 55,
        "TARIF"       => 60,
        "POTR"        => 100,
        "PLAT"        => 100,
        "SKOR"        => 65,
        "VESY"        => 50,
        "TN"          => 60,
        "MPP"         => 55,
        "N_TEPLOVOZ"  => 85,
        "POGRESHNOST" => 85,
        _             => 80,
    };

    private static Label TableHeader(string text) => new()
    {
        Text      = "  " + text,
        Dock      = DockStyle.Top,
        Height    = 24,
        Font      = new Font("Segoe UI", 9, FontStyle.Bold),
        BackColor = Color.FromArgb(25, 45, 90),
        ForeColor = Color.White,
        TextAlign = ContentAlignment.MiddleLeft,
    };

    // ── Загрузка данных ─────────────────────────────────────────────────────

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await LoadBothGridsAsync();
    }

    private async Task LoadBothGridsAsync()
    {
        try
        {
            var pending     = await _db.GetPendingAsync();
            var transferred = await _db.GetTransferredAsync();
            FillGrid(_gridPend, pending);
            FillGrid(_gridDone, transferred);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки из БД:\n{ex.Message}", "База данных",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private static void FillGrid(DataGridView g, List<WagonWeighingRow> rows)
    {
        g.Rows.Clear();
        foreach (var r in rows)
        {
            int idx = g.Rows.Add();
            var row = g.Rows[idx];
            row.Cells["DT"    ].Value = r.WagonTime.ToString("dd.MM.yyyy");
            row.Cells["VR"    ].Value = r.WagonTime.ToString("HH:mm:ss");
            row.Cells["BRUTTO"].Value = r.Total.ToString("F2");
            row.Cells["VESY"  ].Value = "13";
            row.Tag = r;
        }
    }

    // ── Выбор строки в левой таблице ────────────────────────────────────────

    private void GridPend_SelectionChanged(object? sender, EventArgs e)
    {
        if (_gridPend.SelectedRows.Count == 0)
        {
            _selected = null;
            _btnTransfer.Enabled = false;
            return;
        }

        _selected = _gridPend.SelectedRows[0].Tag as WagonWeighingRow;
        if (_selected == null) return;

        _lblDt    .Text = _selected.WagonTime.ToString("dd.MM.yyyy");
        _lblVr    .Text = _selected.WagonTime.ToString("HH:mm:ss");
        _lblNpp   .Text = _selected.WagonNum.ToString();
        _lblMode  .Text = _selected.Mode;
        _lblDir   .Text = _selected.Direction;
        _lblBrutto.Text = _selected.Total.ToString("F2");
        _btnTransfer.Enabled = true;
        RecalcNetto();
    }

    private void RecalcNetto()
    {
        if (_selected == null) return;
        _lblNetto.Text = decimal.TryParse(_txtTar.Text, NumberStyles.Float,
            CultureInfo.InvariantCulture, out decimal tar)
            ? (_selected.Total - tar).ToString("F2")
            : _selected.Total.ToString("F2");
    }

    // ── Перенос в Firebird ──────────────────────────────────────────────────

    private async void BtnTransfer_Click(object? sender, EventArgs e)
    {
        if (_selected == null) return;

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
            await _fb.InsertAsync(table, _selected, nvag, ndok, gruz, tarDok);
            await _db.MarkTransferredAsync(_selected.Id);

            // Переносим строку из левой таблицы в правую
            if (_gridPend.SelectedRows.Count > 0)
            {
                var doneRow = _selected;
                _gridPend.Rows.Remove(_gridPend.SelectedRows[0]);
                _gridDone.Rows.Insert(0, 1);
                var r = _gridDone.Rows[0];
                r.Cells["DT"     ].Value = doneRow.WagonTime.ToString("dd.MM.yyyy");
                r.Cells["VR"     ].Value = doneRow.WagonTime.ToString("HH:mm:ss");
                r.Cells["NVAG"   ].Value = nvag;
                r.Cells["NDOK"   ].Value = _txtNdok.Text.Trim();
                r.Cells["GRUZ"   ].Value = gruz;
                r.Cells["BRUTTO" ].Value = doneRow.Total.ToString("F2");
                r.Cells["TAR_BRS"].Value = _txtTar.Text.Trim();
                r.Cells["NETTO"  ].Value = _lblNetto.Text;
                r.Cells["VESY"   ].Value = "13";
                r.DefaultCellStyle.ForeColor = Color.FromArgb(0, 100, 30);
            }
            ClearTopPanel();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка переноса:\n{ex.Message}", "Перенос",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            _btnTransfer.Enabled = true;
        }
    }

    private void ClearTopPanel()
    {
        _selected = null;
        _lblDt.Text = _lblVr.Text = _lblNpp.Text = _lblMode.Text = _lblDir.Text = "—";
        _lblBrutto.Text = _lblNetto.Text = "—";
        _txtNvag.Clear(); _txtGruz.Clear(); _txtNdok.Clear(); _txtTar.Clear();
        _btnTransfer.Enabled = false;
        _gridPend.ClearSelection();
    }
}