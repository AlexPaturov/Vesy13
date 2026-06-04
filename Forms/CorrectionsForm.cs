using System.Globalization;
using Vesy13.Models;
using Vesy13.Services;

namespace Vesy13.Forms;

public partial class CorrectionsForm : Form
{
    private readonly DatabaseService _db;
    private FirebirdService? _fb;

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

    public CorrectionsForm()
    {
        InitializeComponent();
        AddPendingGridColumns(_gridPend);
        AddFirebirdGridColumns(_gridDone);
    }

    public CorrectionsForm(DatabaseService db)
    {
        _db = db;
        InitializeComponent();
        AddPendingGridColumns(_gridPend);
        AddFirebirdGridColumns(_gridDone);
    }

    // ── Grid columns ────────────────────────────────────────────────────────

    private static void AddPendingGridColumns(DataGridView g)
    {
        DataGridViewTextBoxColumn Col(string header, int width) =>
            new() { HeaderText = header, Width = width, SortMode = DataGridViewColumnSortMode.NotSortable };
        g.Columns.Add(Col("Дата",     84));
        g.Columns.Add(Col("Время",    66));
        g.Columns.Add(Col("№",        36));
        g.Columns.Add(Col("Тел.1 т",  74));
        g.Columns.Add(Col("Тел.2 т",  74));
        g.Columns.Add(Col("Брутто т", 78));
        g.Columns.Add(Col("Режим",    90));
        g.Columns.Add(Col("Напр.",    60));
    }

    private static void AddFirebirdGridColumns(DataGridView g)
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

    // ── Загрузка данных ──────────────────────────────────────────────────────

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (DesignMode || _db is null) return;
        await LoadBothGridsAsync();
    }

    private async Task LoadBothGridsAsync()
    {
        try
        {
            var pending     = await _db.GetPendingAsync();
            var transferred = await _db.GetTransferredAsync();
            FillPendingGrid(_gridPend, pending);
            FillDoneGrid(_gridDone, transferred);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки из БД:\n{ex.Message}", "База данных",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private static void FillPendingGrid(DataGridView g, List<WagonWeighingRow> rows)
    {
        g.Rows.Clear();
        foreach (var r in rows)
        {
            int idx = g.Rows.Add(
                r.WagonTime.ToString("dd.MM.yyyy"),
                r.WagonTime.ToString("HH:mm:ss"),
                r.WagonNum,
                r.Bogie1.ToString("F2"),
                r.Bogie2.ToString("F2"),
                r.Total.ToString("F2"),
                r.Mode,
                r.Direction);
            g.Rows[idx].Tag = r;
        }
    }

    private static void FillDoneGrid(DataGridView g, List<WagonWeighingRow> rows)
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

    // ── Обработчики ──────────────────────────────────────────────────────────

    private void TxtTar_TextChanged(object? sender, EventArgs e) => RecalcNetto();
    private void BtnClear_Click(object? sender, EventArgs e)     => ClearTopPanel();
    private void BtnBack_Click(object? sender, EventArgs e)      => Close();

    private async void BtnRefresh_Click(object? sender, EventArgs e)
    {
        if (_db is null) return;
        await LoadBothGridsAsync();
    }

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

    // ── Перенос в Firebird ───────────────────────────────────────────────────

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
            _fb ??= new FirebirdService();
            await _fb.InsertAsync(table, _selected, nvag, ndok, gruz, tarDok);
            await _db.MarkTransferredAsync(_selected.Id);

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
