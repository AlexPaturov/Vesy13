using System.Globalization;
using Vesy13.Models;
using Vesy13.Services.Repositories;

namespace Vesy13.Forms;

public partial class CorrectionsForm : Form
{
    private readonly LocalRepository _ldb;
    private FactoryRepository? _fdb;

    private LocalWagon? _selected;
    private GpriGras?         _selectedFb;

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
        { "NPP",         "НПП"          },
        { "N_TEPLOVOZ",  "Ном. теплов." },
        { "POGRESHNOST", "Погрешность"  },
    };

    public CorrectionsForm()
    {
        InitializeComponent();
        AddPendingGridColumns(_gridPend);
        AddFirebirdGridColumns(_gridDone);
    }

    public CorrectionsForm(LocalRepository ldb)
    {
        _ldb = ldb;
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
        g.Columns.Add(Col("T1+T2 т",  78));
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
        "NVAG"        => 110,
        "NDOK"        => 100,
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
        "NPP"         => 55,
        "N_TEPLOVOZ"  => 120,
        "POGRESHNOST" => 85,
        _             => 80,
    };

    // ── Загрузка данных ──────────────────────────────────────────────────────

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (DesignMode || _ldb is null) return;
        AuditLogger.Action(AuditLogger.FormOpened, "Form", "CorrectionsForm");
        await LoadBothGridsAsync();
    }

    private async Task LoadBothGridsAsync()
    {
        try
        {
            var pending = await _ldb.GetPendingAsync();
            FillPendingGrid(_gridPend, pending);
            _gridPend.ClearSelection();
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "LocalWagon", "GetPendingAsync", "PostgreSQL", ex.Message);
            MessageBox.Show("Не удалось загрузить список ожидающих взвешиваний.\nОбратитесь к администратору.", "База данных",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        try
        {
            _fdb ??= new FactoryRepository();
            var done = await _fdb.GetRecentAsync();
            FillDoneGrid(_gridDone, done);
            _gridDone.ClearSelection();
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "GpriGras", "GetRecentAsync", "Firebird", ex.Message);
            MessageBox.Show("Данные из системы учёта предприятия недоступны.\nПроверьте подключение к серверу.", "Firebird",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private static void FillPendingGrid(DataGridView g, List<LocalWagon> rows)
    {
        g.Rows.Clear();
        foreach (var r in rows)
        {
            int idx = g.Rows.Add(
                r.WagonTime.ToString("dd.MM.yyyy"),
                r.WagonTime.ToString("HH:mm:ss"),
                r.Number,
                r.Bogie1.ToString("F2"),
                r.Bogie2.ToString("F2"),
                r.Total.ToString("F2"),
                r.Mode,
                r.Direction);
            g.Rows[idx].Tag = r;
        }
    }

    private static void FillDoneGrid(DataGridView g, List<GpriGras> rows)
    {
        g.Rows.Clear();
        foreach (var r in rows)
        {
            int idx = g.Rows.Add();
            var row = g.Rows[idx];
            row.Cells["DT"     ].Value = r.Dt.ToString("dd.MM.yyyy");
            row.Cells["VR"     ].Value = r.Vr.ToString(@"hh\:mm\:ss");
            row.Cells["NVAG"   ].Value = r.Nvag;
            row.Cells["NDOK"   ].Value = r.Ndok;
            row.Cells["GRUZ"   ].Value = r.Gruz;
            row.Cells["BRUTTO" ].Value = r.Brutto.ToString("F2");
            row.Cells["TAR_BRS"].Value = r.TarBrs.HasValue ? r.TarBrs.Value.ToString("F2") : "";
            row.Cells["TAR_DOK"].Value = r.TarDok.HasValue ? r.TarDok.Value.ToString("F2") : "";
            row.Cells["NETTO"  ].Value = r.Netto.HasValue  ? r.Netto.Value.ToString("F2")  : "";
            row.Cells["POTR"   ].Value = r.Potr;
            row.Cells["PLAT"   ].Value = r.Plat;
            row.Cells["VESY"   ].Value = "13";
            row.Cells["NPP"    ].Value = r.Npp;
            row.Cells["CEX"    ].Value = r.Cex;
            row.Tag = r;
        }
    }

    // ── Обработчики ──────────────────────────────────────────────────────────

    private void CmbTar_SelectedIndexChanged(object? sender, EventArgs e) => RecalcNetto();

    private async void TxtNvag_Leave(object? sender, EventArgs e)
    {
        string nvag = _txtNvag.Text.Trim();
        _cmbTar.Items.Clear();
        if (string.IsNullOrEmpty(nvag)) return;
        try
        {
            _fdb ??= new FactoryRepository();
            var options = await _fdb.GetTaraOptionsAsync(nvag);
            foreach (var opt in options)
                _cmbTar.Items.Add(opt);
        }
        catch
        {
            // Firebird недоступен — комбо остаётся пустым, это допустимо
        }
    }
    private void BtnClear_Click(object? sender, EventArgs e)     => ClearTopPanel();
    private void BtnBack_Click(object? sender, EventArgs e)      => Close();

    private async void BtnRefresh_Click(object? sender, EventArgs e)
    {
        if (_ldb is null) return;
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

        _selectedFb = null;
        _gridDone.ClearSelection();
        _btnSave.Visible  = false;
        _btnTransfer.Visible = true;

        _selected = _gridPend.SelectedRows[0].Tag as LocalWagon;
        if (_selected == null) return;

        _lblDt    .Text = _selected.WagonTime.ToString("dd.MM.yyyy");
        _lblVr    .Text = _selected.WagonTime.ToString("HH:mm:ss");
        _lblNpp   .Text = _selected.Number.ToString();
        _lblMode  .Text = _selected.Mode;
        _lblDir   .Text = _selected.Direction;
        _lblBrutto.Text = _selected.Total.ToString("F2");
        _btnTransfer.Enabled = true;
        RecalcNetto();
    }

    private void RecalcNetto()
    {
        if (_rbTara.Checked)
        {
            _lblNetto.Text = "—";
            return;
        }

        if (_selected == null) return;
        _lblNetto.Text = _cmbTar.SelectedItem is TaraOption opt ? ((decimal)_selected.Total - opt.Brutto).ToString("F2") : _selected.Total.ToString("F2");
    }

    private void RbTara_CheckedChanged(object? sender, EventArgs e)
    {
        if (_rbTara.Checked)
        {
            _txtGruz.Text    = "Тара";
            _txtGruz.Enabled = false;
        }
        else
        {
            if (_txtGruz.Text == "Тара") _txtGruz.Clear();
            _txtGruz.Enabled = true;
        }
        RecalcNetto();
    }

    private async void GridDone_SelectionChanged(object? sender, EventArgs e)
    {
        if (_gridDone.SelectedRows.Count == 0)
        {
            _selectedFb = null;
            _btnSave.Enabled = false;
            return;
        }

        var fb = _gridDone.SelectedRows[0].Tag as GpriGras;
        if (fb == null) return;

        _selectedFb = fb;
        _selected   = null;
        _gridPend.ClearSelection();

        _lblDt   .Text = fb.Dt.ToString("dd.MM.yyyy");
        _lblVr   .Text = fb.Vr.ToString(@"hh\:mm\:ss");
        _lblNpp  .Text = fb.Npp.ToString();
        _lblMode .Text = "—";
        _lblDir  .Text = "—";
        _lblBrutto.Text = fb.Brutto.ToString("F2");

        _txtNvag.Text = fb.Nvag;
        _txtNdok.Text = fb.Ndok?.ToString() ?? "";
        _tbPotr .Text = fb.Potr;
        _tbPlat .Text = fb.Plat;
        tbCex   .Text = fb.Cex > 0 ? fb.Cex.ToString() : "";

        _rbGpri.Checked = fb.Table == "GPRI";
        _rbGras.Checked = fb.Table == "GRAS";

        bool isTara = fb.Gruz.Trim() == "Тара";
        _rbTara  .Checked = isTara;
        _rbBrutto.Checked = !isTara;

        if (!isTara)
            _txtGruz.Text = fb.Gruz;

        _cmbTar.Items.Clear();
        try
        {
            _fdb ??= new FactoryRepository();
            var options = await _fdb.GetTaraOptionsAsync(fb.Nvag);
            foreach (var opt in options)
                _cmbTar.Items.Add(opt);

            decimal? tarVal = fb.TarDok ?? fb.TarBrs;
            if (tarVal.HasValue)
            {
                foreach (TaraOption opt in _cmbTar.Items)
                    if (opt.Brutto == tarVal.Value) { _cmbTar.SelectedItem = opt; break; }
            }
        }
        catch 
        {
        // todo
        }

        _btnSave    .Visible = true;
        _btnSave    .Enabled = true;
        _btnTransfer.Visible = false;
        RecalcNetto();
    }

    // ── Перенос в Firebird ───────────────────────────────────────────────────

    private async void BtnTransfer_Click(object? sender, EventArgs e)
    {
        if (_selected == null) return;

        string nvag = _txtNvag.Text.Trim();
        if (string.IsNullOrEmpty(nvag))
        {
            MessageBox.Show("Введите номер вагона (NVAG).", "Перенос", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            _txtNvag.Focus();
            return;
        }

        bool     isTara = _rbTara.Checked;
        long?    ndok   = long.TryParse(_txtNdok.Text.Trim(), out long nd) ? nd : null;
        decimal? tarDok = _cmbTar.SelectedItem is TaraOption taraOpt ? taraOpt.Brutto : null;
        string?  potr   = string.IsNullOrWhiteSpace(_tbPotr.Text) ? null : _tbPotr.Text.Trim();
        string?  plat   = string.IsNullOrWhiteSpace(_tbPlat.Text) ? null : _tbPlat.Text.Trim();
        decimal  total  = (decimal)_selected.Total;

        int cex = int.TryParse(tbCex.Text.Trim(), out int c) ? c : 0;

        var transfer = new GpriGras
        {
            Table  = _rbGpri.Checked ? "GPRI" : "GRAS",
            Dt     = _selected.WagonTime.Date,
            Vr     = _selected.WagonTime.TimeOfDay,
            Nvag   = nvag,
            Ndok   = ndok,
            Gruz   = isTara ? "Тара" : _txtGruz.Text.Trim(),
            Brutto = isTara ? 0m     : total,
            TarBrs = isTara ? total  : null,
            TarDok = tarDok,
            Netto  = isTara ? null   : (tarDok.HasValue ? total - tarDok.Value : null),
            Npp    = _selected.Number,
            Mode   = _selected.Mode,
            Potr   = potr ?? "",
            Plat   = plat ?? "",
            Cex    = cex,
        };

        _btnTransfer.Enabled = false;
        try
        {
            _fdb ??= new FactoryRepository();
            await _fdb.InsertAsync(transfer);
            await _ldb.MarkTransferredAsync(_selected.Id);
            AuditLogger.Action(AuditLogger.RecordTransferred,
                "FirebirdRecord", $"{transfer.Table} nvag={transfer.Nvag}",
                "Firebird", _selected.Id.ToString());

            if (_gridPend.SelectedRows.Count > 0)
            {
                _gridPend.Rows.Remove(_gridPend.SelectedRows[0]);
                _gridDone.Rows.Insert(0, 1);
                var r = _gridDone.Rows[0];
                r.Cells["DT"     ].Value = transfer.Dt.ToString("dd.MM.yyyy");
                r.Cells["VR"     ].Value = transfer.Vr.ToString(@"hh\:mm\:ss");
                r.Cells["NVAG"   ].Value = transfer.Nvag;
                r.Cells["NDOK"   ].Value = transfer.Ndok?.ToString() ?? "";
                r.Cells["GRUZ"   ].Value = transfer.Gruz;
                r.Cells["BRUTTO" ].Value = transfer.Brutto.ToString("F2");
                r.Cells["TAR_BRS"].Value = transfer.TarBrs?.ToString("F2") ?? "";
                r.Cells["TAR_DOK"].Value = transfer.TarDok?.ToString("F2") ?? "";
                r.Cells["NETTO"  ].Value = transfer.Netto?.ToString("F2")  ?? "";
                r.Cells["POTR"   ].Value = transfer.Potr;
                r.Cells["PLAT"   ].Value = transfer.Plat;
                r.Cells["VESY"   ].Value = "13";
                r.Cells["CEX"    ].Value = transfer.Cex > 0 ? transfer.Cex.ToString() : "";
                r.DefaultCellStyle.ForeColor = Color.FromArgb(0, 100, 30);
            }
            ClearTopPanel();
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "FirebirdRecord", transfer.Table, "Firebird", ex.Message);
            MessageBox.Show("Не удалось перенести запись в систему учёта.\nОбратитесь к администратору.",
                "Перенос", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _btnTransfer.Enabled = true;
        }
    }

    private async void BtnSave_Click(object? sender, EventArgs e)
    {
        if (_selectedFb == null) return;

        string nvag = _txtNvag.Text.Trim();
        if (string.IsNullOrEmpty(nvag))
        {
            MessageBox.Show("Введите номер вагона (NVAG).", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtNvag.Focus();
            return;
        }

        bool     isTara = _rbTara.Checked;
        long?    ndok   = long.TryParse(_txtNdok.Text.Trim(), out long nd) ? nd : null;
        decimal? tarDok = (!isTara && _cmbTar.SelectedItem is TaraOption taraOpt) ? taraOpt.Brutto : null;
        string?  potr   = string.IsNullOrWhiteSpace(_tbPotr.Text) ? null : _tbPotr.Text.Trim();
        string?  plat   = string.IsNullOrWhiteSpace(_tbPlat.Text) ? null : _tbPlat.Text.Trim();

        var updated = new GpriGras
        {
            Id     = _selectedFb.Id,
            Table  = _selectedFb.Table,
            Dt     = _selectedFb.Dt,
            Vr     = _selectedFb.Vr,
            Nvag   = nvag,
            Ndok   = ndok,
            Gruz   = isTara ? "Тара" : (string.IsNullOrWhiteSpace(_txtGruz.Text) ? "" : _txtGruz.Text.Trim()),
            Brutto = _selectedFb.Brutto,
            TarBrs = isTara ? _selectedFb.Brutto : null,
            TarDok = tarDok,
            Netto  = isTara ? null : (tarDok.HasValue ? _selectedFb.Brutto - tarDok.Value : null),
            Npp    = _selectedFb.Npp,
            Cex    = int.TryParse(tbCex.Text.Trim(), out int c) ? c : _selectedFb.Cex,
            Potr   = potr ?? "",
            Plat   = plat ?? "",
        };

        _btnSave.Enabled = false;

        try
        {
            _fdb ??= new FactoryRepository();
            await _fdb.UpdateAsync(updated);
            AuditLogger.Action(AuditLogger.RecordUpdated,
                "FirebirdRecord", $"{updated.Table} nvag={updated.Nvag}",
                "Firebird", updated.Id.ToString());

            if (_gridDone.SelectedRows.Count > 0)
            {
                var r = _gridDone.SelectedRows[0];
                r.Cells["NVAG"   ].Value = updated.Nvag;
                r.Cells["NDOK"   ].Value = updated.Ndok?.ToString() ?? "";
                r.Cells["GRUZ"   ].Value = updated.Gruz;
                r.Cells["TAR_BRS"].Value = updated.TarBrs?.ToString("F2") ?? "";
                r.Cells["TAR_DOK"].Value = updated.TarDok?.ToString("F2") ?? "";
                r.Cells["NETTO"  ].Value = updated.Netto?.ToString("F2")  ?? "";
                r.Cells["POTR"   ].Value = updated.Potr;
                r.Cells["PLAT"   ].Value = updated.Plat;
            }
            ClearTopPanel();
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "FirebirdRecord", _selectedFb?.Table ?? "GPRI", "Firebird", ex.Message);
            MessageBox.Show("Не удалось сохранить изменения в системе учёта.\nОбратитесь к администратору.",
                "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _btnSave.Enabled = true;
        }
    }

    private void ClearTopPanel()
    {
        _selected   = null;
        _selectedFb = null;
        _lblDt.Text = _lblVr.Text = _lblNpp.Text = _lblMode.Text = _lblDir.Text = "—";
        _lblBrutto.Text = _lblNetto.Text = "—";
        _txtNvag.Clear(); _txtNdok.Clear(); tbCex.Clear();
        _cmbTar.Items.Clear();
        _cmbTar.SelectedIndex = -1;
        _rbBrutto.Checked  = true;
        _txtGruz.Clear();
        _txtGruz.Enabled   = true;
        _btnTransfer.Enabled  = false;
        _btnTransfer.Visible  = true;
        _btnSave    .Enabled  = false;
        _btnSave    .Visible  = false;
        _gridPend.ClearSelection();
        _gridDone.ClearSelection();
    }
}
