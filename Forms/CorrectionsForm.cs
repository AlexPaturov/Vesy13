using System.Globalization;
using Vesy13.Models;
using Vesy13.Services.Repositories;

namespace Vesy13.Forms;

/// <summary>
/// Форма корректировок: переносит взвешивания из PostgreSQL в Firebird (GPRI/GRAS)
/// и позволяет редактировать уже перенесённые записи.
/// </summary>
public partial class CorrectionsForm : Form
{
    private LocalRepository? _ldb;
    private FactoryRepository? _fdb;

    private LocalWagon? _selected;
    private GpriGras? _selectedFb;
    private TaraOption? _localInjectedTareOption;
    private string? _lastProcessedFirebirdIdKey;
    private string? _lastProcessedFirebirdValueKey;

    public static readonly Dictionary<string, string> GpGridChapters = new()
    {
        { "DT",          "Дата взв."    },
        { "VR",          "Время взв."   },
        { "NVAG",        "№ вагона"     },
        { "GRUZ",        "Груз"         },
        { "BRUTTO",      "Брутто"       },
        { "TAR_BRS",     "Тара факт"    },
        { "NETTO",       "Нетто"        },
        { "CEX",         "Цех"          },
        { "POTR",        "Поставщик"    },
        { "PLAT",        "Плательщик"   },
        { "VESY",        "Весы"         },
        { "TN",          "Таб. ном."    },
        { "NPP",         "НПП"          },
        { "N_TEPLOVOZ",  "Ном. теплов." },
    };

    public CorrectionsForm()
    {
        InitializeComponent();
        AddPendingGridColumns(_gridPend);
        AddFirebirdGridColumns(_gridDone);
        WireContextEvents();
        InitializeContextUi();
    }

    public CorrectionsForm(LocalRepository ldb)
    {
        _ldb = ldb;
        InitializeComponent();
        AddPendingGridColumns(_gridPend);
        AddFirebirdGridColumns(_gridDone);
        WireContextEvents();
        InitializeContextUi();
    }

    private void WireContextEvents()
    {
        rbTrainMode.CheckedChanged += ContextMode_CheckedChanged;
        rbDayMode.CheckedChanged += ContextMode_CheckedChanged;
    }

    private void InitializeContextUi()
    {
        if (!rbTrainMode.Checked && !rbDayMode.Checked)
            rbTrainMode.Checked = true;

        UpdateContextUi();
    }

    private void ContextMode_CheckedChanged(object? sender, EventArgs e) => UpdateContextUi();

    private void UpdateContextUi()
    {
        bool trainMode = rbTrainMode.Checked;
        _dtpTrainTime.Enabled = trainMode;
        label4.Enabled = trainMode;
    }

    private DateTime SelectedContextDate => _dtpTrainDate.Value.Date;

    private DateTime SelectedContextTrainTime => SelectedContextDate.Add(_dtpTrainTime.Value.TimeOfDay);

    private void ApplyTheme()
    {
        BackColor = UiColors.AppBackground;
        _pnlTop.BackColor = UiColors.AppBackground;
        _pnlTopActions.BackColor = UiColors.Surface;
        _actionPanel.BackColor = UiColors.SurfaceMuted;
        tableLayoutPanel5.BackColor = UiColors.Surface;
        tableLayoutPanel7.BackColor = UiColors.Surface;
        tableLayoutPanel4.BackColor = UiColors.Surface;
        tableLayoutPanel1.BackColor = UiColors.Surface;
        tableLayoutPanel2.BackColor = UiColors.Surface;
        tableLayoutPanel3.BackColor = UiColors.Surface;
        tableLayoutPanel6.BackColor = UiColors.Surface;
        tableLayoutPanel8.BackColor = UiColors.SurfaceMuted;
        tableLayoutPanel9.BackColor = UiColors.Surface;
        tabPanTrainTimeDayMode.BackColor = UiColors.SurfaceMuted;
        _split.Panel1.BackColor = UiColors.AppBackground;
        _split.Panel2.BackColor = UiColors.AppBackground;
        _lblHeaderPend.BackColor = UiColors.NavigationAction;
        _lblHeaderDone.BackColor = UiColors.NavigationAction;
        _pnlStatus.BackColor = UiColors.StatusBar;

        label2.Font = UiFonts.Body;
        label2.ForeColor = UiColors.TextMuted;
        lblpotr.Font = UiFonts.Body;
        lblpotr.ForeColor = UiColors.TextMuted;
        _tbPlat.Font = UiFonts.Body;
        _tbPlat.BackColor = UiColors.InputBack;
        _tbPlat.ForeColor = UiColors.InputFore;
        _tbPotr.Font = UiFonts.Body;
        _tbPotr.BackColor = UiColors.InputBack;
        _tbPotr.ForeColor = UiColors.InputFore;
        _lblVidVzv.Font = UiFonts.Body;
        _lblVidVzv.ForeColor = UiColors.TextMuted;
        _rbGpri.Font = UiFonts.Body;
        _rbGpri.ForeColor = UiColors.TextPrimary;
        _rbGras.Font = UiFonts.Body;
        _rbGras.ForeColor = UiColors.TextPrimary;
        _lblVidMode.Font = UiFonts.Body;
        _lblVidMode.ForeColor = UiColors.TextMuted;
        _rbBrutto.Font = UiFonts.Body;
        _rbBrutto.ForeColor = UiColors.TextPrimary;
        _rbTara.Font = UiFonts.Body;
        _rbTara.ForeColor = UiColors.TextPrimary;
        _btnSave.Font = UiFonts.MediumBold;
        _btnSave.BackColor = UiColors.InfoAction;
        _btnSave.ForeColor = UiColors.TextOnDark;
        _lblDateCap.Font = UiFonts.Body;
        _lblDateCap.ForeColor = UiColors.TextMuted;
        _lblDt.Font = UiFonts.BodyBold;
        _lblDt.ForeColor = UiColors.TextPrimary;
        _lblTimeCap.Font = UiFonts.Body;
        _lblTimeCap.ForeColor = UiColors.TextMuted;
        _lblVr.Font = UiFonts.BodyBold;
        _lblVr.ForeColor = UiColors.TextPrimary;
        _lblNppCap.Font = UiFonts.Body;
        _lblNppCap.ForeColor = UiColors.TextMuted;
        _lblNpp.Font = UiFonts.BodyBold;
        _lblNpp.ForeColor = UiColors.TextPrimary;
        _lblModeCap.Font = UiFonts.Body;
        _lblModeCap.ForeColor = UiColors.TextMuted;
        _lblMode.Font = UiFonts.BodyBold;
        _lblMode.ForeColor = UiColors.TextPrimary;
        _lblNvagCap.Font = UiFonts.Body;
        _lblNvagCap.ForeColor = UiColors.TextMuted;
        _tbNvag.Font = UiFonts.Body;
        _tbNvag.BackColor = UiColors.InputBack;
        _tbNvag.ForeColor = UiColors.InputFore;
        _lblGruzCap.Font = UiFonts.Body;
        _lblGruzCap.ForeColor = UiColors.TextMuted;
        _tbGruz.Font = UiFonts.Body;
        _tbGruz.BackColor = UiColors.InputBack;
        _tbGruz.ForeColor = UiColors.InputFore;
        _lblBruttoCap.Font = UiFonts.Body;
        _lblBruttoCap.ForeColor = UiColors.TextMuted;
        _lblBrutto.Font = UiFonts.MonoBold;
        _lblBrutto.ForeColor = UiColors.TextPrimary;
        _lblTarCap.Font = UiFonts.Body;
        _lblTarCap.ForeColor = UiColors.TextMuted;
        _cmbTar.Font = UiFonts.Body;
        _cmbTar.BackColor = UiColors.InputBack;
        _cmbTar.ForeColor = UiColors.InputFore;
        _lblNettoCap.Font = UiFonts.Body;
        _lblNettoCap.ForeColor = UiColors.TextMuted;
        _lblNetto.Font = UiFonts.MonoBold;
        _lblNetto.ForeColor = UiColors.PrimaryAction;
        _btnTransfer.Font = UiFonts.MediumBold;
        _btnTransfer.BackColor = UiColors.PrimaryAction;
        _btnTransfer.ForeColor = UiColors.TextOnDark;
        _btnClear.Font = UiFonts.Body;
        _btnClear.BackColor = UiColors.NeutralAction;
        _btnClear.ForeColor = UiColors.TextPrimary;
        _btnRefresh.Font = UiFonts.Body;
        _btnRefresh.BackColor = UiColors.NeutralAction;
        _btnRefresh.ForeColor = UiColors.TextPrimary;
        labTrainDate.Font = UiFonts.Body;
        labTrainDate.ForeColor = UiColors.TextMuted;
        label4.Font = UiFonts.Body;
        label4.ForeColor = UiColors.TextMuted;
        rbTrainMode.Font = UiFonts.Body;
        rbTrainMode.ForeColor = UiColors.TextPrimary;
        rbDayMode.Font = UiFonts.Body;
        rbDayMode.ForeColor = UiColors.TextPrimary;
        _dtpTrainDate.Font = UiFonts.Body;
        _dtpTrainDate.BackColor = UiColors.InputBack;
        _dtpTrainDate.ForeColor = UiColors.InputFore;
        _dtpTrainTime.Font = UiFonts.Body;
        _dtpTrainTime.BackColor = UiColors.InputBack;
        _dtpTrainTime.ForeColor = UiColors.InputFore;
        _gridPend.Font = UiFonts.GridBody;
        _gridPend.BackgroundColor = UiColors.InputBack;
        _gridPend.DefaultCellStyle.BackColor = UiColors.InputBack;
        _gridPend.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
        _gridPend.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
        _gridPend.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
        _gridPend.ColumnHeadersDefaultCellStyle.Font = UiFonts.GridHeader;
        _gridPend.ColumnHeadersDefaultCellStyle.BackColor = UiColors.GridHeaderBack;
        _gridPend.ColumnHeadersDefaultCellStyle.ForeColor = UiColors.GridHeaderText;
        _gridPend.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiColors.GridHeaderBack;
        _gridPend.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiColors.GridHeaderText;
        _lblHeaderPend.Font = UiFonts.BodyBold;
        _lblHeaderPend.ForeColor = UiColors.TextOnDark;
        _gridDone.Font = UiFonts.GridBody;
        _gridDone.BackgroundColor = UiColors.InputBack;
        _gridDone.DefaultCellStyle.BackColor = UiColors.InputBack;
        _gridDone.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
        _gridDone.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
        _gridDone.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
        _gridDone.ColumnHeadersDefaultCellStyle.Font = UiFonts.GridHeader;
        _gridDone.ColumnHeadersDefaultCellStyle.BackColor = UiColors.GridHeaderBack;
        _gridDone.ColumnHeadersDefaultCellStyle.ForeColor = UiColors.GridHeaderText;
        _gridDone.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiColors.GridHeaderBack;
        _gridDone.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiColors.GridHeaderText;
        _lblHeaderDone.Font = UiFonts.BodyBold;
        _lblHeaderDone.ForeColor = UiColors.TextOnDark;
        _tbCex.Font = UiFonts.Body;
        _tbCex.BackColor = UiColors.InputBack;
        _tbCex.ForeColor = UiColors.InputFore;
        label3.Font = UiFonts.Body;
        label3.ForeColor = UiColors.TextMuted;
    }

    // ── Grid columns ────────────────────────────────────────────────────────

    private static void AddPendingGridColumns(DataGridView g)
    {
        DataGridViewTextBoxColumn Col(string header, int width, int minWidth) =>
            new()
            {
                HeaderText = header,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = width,
                MinimumWidth = minWidth,
                SortMode = DataGridViewColumnSortMode.NotSortable,
            };

        g.Columns.Add(Col("Дата", 120, 100));
        g.Columns.Add(Col("Время", 106, 88));
        g.Columns.Add(Col("№", 48, 48));
        g.Columns.Add(Col("Вес", 92, 74));
        g.Columns.Add(Col("Режим", 160, 110));
        g.Columns.Add(Col("Напр.", 80, 70));
    }

    private static void AddFirebirdGridColumns(DataGridView g)
    {
        foreach (var (key, header) in GpGridChapters)
            g.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = key,
                HeaderText = header,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = ColumnWidth(key),
                MinimumWidth = ColumnMinWidth(key),
                SortMode = DataGridViewColumnSortMode.NotSortable,
            });
    }

    private static int ColumnWidth(string key) => key switch
    {
        "DT" => 126,
        "VR" => 104,
        "NVAG" => 150,
        "GRUZ" => 180,
        "BRUTTO" => 95,
        "TAR_BRS" => 95,
        "NETTO" => 95,
        "CEX" => 75,
        "POTR" => 180,
        "PLAT" => 180,
        "VESY" => 74,
        "TN" => 80,
        "NPP" => 75,
        "N_TEPLOVOZ" => 180,
        _ => 105,
    };

    private static int ColumnMinWidth(string key) => key switch
    {
        "DT" => 102,
        "VR" => 90,
        "NVAG" => 90,
        "GRUZ" => 120,
        "BRUTTO" => 80,
        "TAR_BRS" => 80,
        "NETTO" => 80,
        "CEX" => 60,
        "POTR" => 120,
        "PLAT" => 120,
        "VESY" => 60,
        "TN" => 70,
        "NPP" => 70,
        "N_TEPLOVOZ" => 120,
        _ => 80,
    };

    // ── Загрузка данных ──────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyTheme();
        _gridPend.AllowUserToResizeColumns = true;
        _gridDone.AllowUserToResizeColumns = true;
        if (!DesignMode)
        {
            ApplyGridDpiSizing();
        }
        if (DesignMode) return;
        UpdateContextUi();
        AuditLogger.Action(AuditLogger.FormOpened, "Form", "CorrectionsForm");
    }

    private void ApplyGridDpiSizing()
    {
        double dpiScale = DeviceDpi / 96.0;
        double widthScale = dpiScale <= 1.0 ? 1.0 : 1.0 + ((dpiScale - 1.0) * 0.65);
        ScaleGrid(_gridPend, widthScale);
        ScaleGrid(_gridDone, widthScale);
    }

    private static void ScaleGrid(DataGridView grid, double scale)
    {
        foreach (DataGridViewColumn column in grid.Columns)
            column.Width = Math.Max(column.MinimumWidth, (int)Math.Round(column.Width * scale));
    }


    private async Task<(int PendingCount, int DoneCount, bool HadError)> LoadBothGridsAsync()
    {
        if (_ldb is null) return (0, 0, true);

        ClearTopPanel();

        bool trainMode = rbTrainMode.Checked;
        int pendingCount = 0;
        int doneCount = 0;
        bool hadError = false;

        try
        {
            var pending = trainMode
                ? await _ldb.GetAllByTrainTimeAsync(SelectedContextTrainTime)
                : await _ldb.GetAllByDateAsync(SelectedContextDate);
            pendingCount = pending.Count;
            FillPendingGrid(_gridPend, pending);
            _gridPend.ClearSelection();
        }
        catch (Exception ex)
        {
            hadError = true;
            string op = trainMode ? "GetAllByTrainTimeAsync" : "GetAllByDateAsync";
            AuditLogger.Error(AuditLogger.ErrorDb, "LocalWagon", op, "PostgreSQL", ex.Message);
            MessageBox.Show("Не удалось загрузить список ожидающих взвешиваний.\nОбратитесь к администратору.", "База данных",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        try
        {
            _fdb ??= new FactoryRepository();
            var done = trainMode
                ? await _fdb.GetByTrainTimeAsync(SelectedContextTrainTime)
                : await _fdb.GetByDateAsync(SelectedContextDate);
            doneCount = done.Count;
            FillDoneGrid(_gridDone, done);
            _gridDone.ClearSelection();
        }
        catch (Exception ex)
        {
            hadError = true;
            string op = trainMode ? "GetByTrainTimeAsync" : "GetByDateAsync";
            AuditLogger.Error(AuditLogger.ErrorDb, "GpriGras", op, "Firebird", ex.Message);
            MessageBox.Show("Данные из системы учёта предприятия недоступны.\nПроверьте подключение к серверу.", "Firebird",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        return (pendingCount, doneCount, hadError);
    }

    private static void FillPendingGrid(DataGridView g, List<LocalWagon> rows)
    {
        g.Rows.Clear();
        foreach (var r in rows)
        {
            int idx = g.Rows.Add(
                r.TrainTime.ToString("dd.MM.yyyy"),
                r.TrainTime.ToString("HH:mm:ss"),
                r.Number,
                r.Total.ToString("F2"),
                r.Mode,
                r.Direction);
            var row = g.Rows[idx];
            row.Tag = r;
            ApplyPendingRowStyle(row, r.Transferred);
        }
    }

    private static void ApplyPendingRowStyle(DataGridViewRow row, bool transferred)
    {
        if (!transferred)
        {
            row.DefaultCellStyle.BackColor = UiColors.InputBack;
            row.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
            row.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
            row.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
            return;
        }

        var transferredColor = ColorTranslator.FromHtml("#FFDAB9");
        row.DefaultCellStyle.BackColor = transferredColor;
        row.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
        row.DefaultCellStyle.SelectionBackColor = transferredColor;
        row.DefaultCellStyle.SelectionForeColor = UiColors.TextPrimary;
    }

    private void SelectPendingRowById(int id)
    {
        foreach (DataGridViewRow row in _gridPend.Rows)
        {
            if (row.Tag is not LocalWagon wagon || wagon.Id != id)
                continue;

            _gridPend.ClearSelection();
            row.Selected = true;

            var firstVisibleCell = row.Cells
                .Cast<DataGridViewCell>()
                .FirstOrDefault(cell => cell.OwningColumn?.Visible == true);
            if (firstVisibleCell is not null)
                _gridPend.CurrentCell = firstVisibleCell;

            _gridPend.FirstDisplayedScrollingRowIndex = row.Index;
            _gridPend.Focus();
            return;
        }
    }

    private void FillDoneGrid(DataGridView g, List<GpriGras> rows)
    {
        g.Rows.Clear();
        foreach (var r in rows)
        {
            int idx = g.Rows.Add();
            var row = g.Rows[idx];
            row.Cells["DT"].Value = r.Dt.ToString("dd.MM.yyyy");
            row.Cells["VR"].Value = r.Vr.ToString(@"hh\:mm\:ss");
            row.Cells["NVAG"].Value = r.Nvag;
            row.Cells["GRUZ"].Value = r.Gruz;
            row.Cells["BRUTTO"].Value = r.Brutto.ToString("F2");
            row.Cells["TAR_BRS"].Value = r.TarBrs.HasValue ? r.TarBrs.Value.ToString("F2") : "";
            row.Cells["NETTO"].Value = r.Netto.HasValue ? r.Netto.Value.ToString("F2") : "";
            row.Cells["POTR"].Value = r.Potr;
            row.Cells["PLAT"].Value = r.Plat;
            row.Cells["VESY"].Value = "13";
            row.Cells["NPP"].Value = r.Npp;
            row.Cells["CEX"].Value = r.Cex;
            row.Tag = r;
            ApplyDoneRowStyle(row, IsLastProcessedFirebirdRecord(r));
        }
    }

    private bool IsLastProcessedFirebirdRecord(GpriGras record) =>
        (_lastProcessedFirebirdIdKey is not null && GetFirebirdIdKey(record) == _lastProcessedFirebirdIdKey) ||
        (_lastProcessedFirebirdValueKey is not null && GetFirebirdValueKey(record) == _lastProcessedFirebirdValueKey);

    private void RememberLastProcessedFirebirdRecord(GpriGras record)
    {
        _lastProcessedFirebirdIdKey = record.Id > 0 ? GetFirebirdIdKey(record) : null;
        _lastProcessedFirebirdValueKey = GetFirebirdValueKey(record);
    }

    private static string GetFirebirdIdKey(GpriGras record) => $"id:{record.Table}:{record.Id}";

    private static string GetFirebirdValueKey(GpriGras record)
    {
        var vr = new TimeSpan(record.Vr.Hours, record.Vr.Minutes, record.Vr.Seconds);
        return $"value:{record.Table}:{record.Dt:yyyyMMdd}:{vr:c}:{record.Nvag.Trim()}:{record.Npp}:{record.Gruz.Trim()}";
    }

    private static void ApplyDoneRowStyle(DataGridViewRow row, bool processed)
    {
        if (!processed)
        {
            row.DefaultCellStyle.BackColor = UiColors.InputBack;
            row.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
            row.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
            row.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
            return;
        }

        var processedColor = ColorTranslator.FromHtml("#98FB98");
        row.DefaultCellStyle.BackColor = processedColor;
        row.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
        row.DefaultCellStyle.SelectionBackColor = processedColor;
        row.DefaultCellStyle.SelectionForeColor = UiColors.TextPrimary;
    }

    private void ClearDoneRowStyles()
    {
        foreach (DataGridViewRow row in _gridDone.Rows)
            ApplyDoneRowStyle(row, false);
    }

    // ── Обработчики ──────────────────────────────────────────────────────────

    private void CmbTar_SelectedIndexChanged(object? sender, EventArgs e) => RecalcNetto();

    private async void TxtNvag_Leave(object? sender, EventArgs e)
    {
        string nvag = _tbNvag.Text.Trim();
        _cmbTar.Items.Clear(); // не заходим если вышли в режиме тара
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
    private void BtnClear_Click(object? sender, EventArgs e) => ClearTopPanel();
    private async void BtnRefresh_Click(object? sender, EventArgs e)
    {
        if (_ldb is null) return;

        string originalText = _btnRefresh.Text;
        bool originalEnabled = _btnRefresh.Enabled;

        _btnRefresh.Text = "Загрузка...";
        _btnRefresh.Enabled = false;

        try
        {
            var (pendingCount, doneCount, hadError) = await LoadBothGridsAsync();
            if (!hadError && pendingCount == 0 && doneCount == 0)
            {
                MessageBox.Show("Нет вагонов за данный период.", "Обновление",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        finally
        {
            _btnRefresh.Text = originalText;
            _btnRefresh.Enabled = originalEnabled;
        }
    }

    private async void GridPend_SelectionChanged(object? sender, EventArgs e)
    {
        if (_gridPend.SelectedRows.Count == 0)
        {
            _selected = null;
            _btnTransfer.Enabled = false;
            return;
        }

        var selected = _gridPend.SelectedRows[0].Tag as LocalWagon;
        if (selected == null) return;
        if (selected.Transferred)
        {
            ClearTopPanel(clearGridSelection: false);
            return;
        }

        _selectedFb = null;
        _gridDone.ClearSelection();
        _btnSave.Visible = false;
        _btnTransfer.Visible = true;

        _selected = selected;

        _lblDt.Text = _selected.WagonTime.ToString("dd.MM.yyyy");
        _lblVr.Text = _selected.WagonTime.ToString("HH:mm:ss");
        _lblNpp.Text = _selected.Number.ToString();
        _lblMode.Text = _selected.Mode;
        _cmbTar.Items.Clear();
        try
        {
            _fdb ??= new FactoryRepository();
            var options = await _fdb.GetTaraOptionsAsync(_selected.Number.ToString());
            foreach (var opt in options)
                _cmbTar.Items.Add(opt);
        }
        catch
        {
            // Firebird недоступен — оставляем список пустым
        }

        _btnTransfer.Enabled = true;
        ApplyLocalWeightPresentation();
        RecalcNetto();
    }

    private void RecalcNetto()
    {
        if (_rbTara.Checked)
        {
            _lblNetto.Text = "—";
            return;
        }

        decimal? brutto = _selected is not null
            ? (decimal)_selected.Total
            : _selectedFb?.Brutto;

        if (brutto is null)
        {
            _lblNetto.Text = "—";
            return;
        }

        _lblNetto.Text = _cmbTar.SelectedItem is TaraOption opt
            ? (brutto.Value - opt.Brutto).ToString("F2")
            : "—";
    }

    /// <summary>
    /// Applies the selected pending wagon weight to the visible fields based on the active mode.
    /// </summary>
    private void ApplyLocalWeightPresentation()
    {
        if (_selected is null || _gridDone.SelectedRows.Count > 0)
            return;

        if (_localInjectedTareOption is not null)
        {
            _cmbTar.Items.Remove(_localInjectedTareOption);
            _localInjectedTareOption = null;
        }

        if (_rbTara.Checked)
        {
            _lblBrutto.Text = "—";
            _localInjectedTareOption = new TaraOption(_selected.WagonTime, (decimal)_selected.Total, WeightOnly: true);
            _cmbTar.Items.Insert(0, _localInjectedTareOption);
            _cmbTar.SelectedItem = _localInjectedTareOption;
        }
        else
        {
            _lblBrutto.Text = _selected.Total.ToString("F2");
            if (_cmbTar.SelectedItem is TaraOption)
                _cmbTar.SelectedIndex = -1;
        }
    }

    private void RbTara_CheckedChanged(object? sender, EventArgs e)
    {
        if (_rbTara.Checked)
        {
            _tbGruz.Text = "Тара";
            _tbGruz.Enabled = false;
        }
        else
        {
            if (_tbGruz.Text == "Тара") _tbGruz.Clear();
            _tbGruz.Enabled = true;
        }

        ApplyLocalWeightPresentation();
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
        _selected = null;
        _gridPend.ClearSelection();

        _lblDt.Text = fb.Dt.ToString("dd.MM.yyyy");
        _lblVr.Text = fb.Vr.ToString(@"hh\:mm\:ss");
        _lblNpp.Text = fb.Npp.ToString();
        _lblMode.Text = "—";

        _tbNvag.Text = fb.Nvag;
        _tbPotr.Text = fb.Potr;
        _tbPlat.Text = fb.Plat;
        _tbCex.Text = fb.Cex > 0 ? fb.Cex.ToString() : "";

        _rbGpri.Checked = fb.Table == "GPRI";
        _rbGras.Checked = fb.Table == "GRAS";

        bool isTara = fb.Gruz.Trim() == "Тара";
        _rbTara.Checked = isTara;
        _rbBrutto.Checked = !isTara;
        _lblBrutto.Text = isTara ? "—" : fb.Brutto.ToString("F2");

        if (!isTara)
            _tbGruz.Text = fb.Gruz;

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
                TaraOption? selectedTar = null;
                foreach (TaraOption opt in _cmbTar.Items)
                {
                    if (opt.Brutto == tarVal.Value)
                    {
                        selectedTar = opt;
                        break;
                    }
                }

                if (selectedTar is null && isTara)
                {
                    selectedTar = new TaraOption(fb.Dt.Date.Add(fb.Vr), tarVal.Value, WeightOnly: true);
                    _cmbTar.Items.Insert(0, selectedTar);
                }

                if (selectedTar is not null)
                    _cmbTar.SelectedItem = selectedTar;
            }
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "GpriGras", "GridDone_SelectionChanged", "Firebird", ex.Message);
            MessageBox.Show("Данные из системы учёта предприятия недоступны.\nПроверьте подключение к серверу.", "Firebird",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        _btnSave.Visible = true;
        _btnSave.Enabled = true;
        _btnTransfer.Visible = false;
        RecalcNetto();
    }

    // ── Перенос в Firebird ───────────────────────────────────────────────────

    private async void BtnTransfer_Click(object? sender, EventArgs e)
    {
        if (_selected == null) return;

        int transferredLocalId = _selected.Id;

        string nvag = _tbNvag.Text.Trim();
        if (string.IsNullOrEmpty(nvag))
        {
            MessageBox.Show("Введите номер вагона (NVAG).", "Перенос", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _tbNvag.Focus();
            return;
        }

        bool isTara = _rbTara.Checked;
        decimal? tarDok = _cmbTar.SelectedItem is TaraOption taraOpt ? taraOpt.Brutto : null;
        string? potr = string.IsNullOrWhiteSpace(_tbPotr.Text) ? null : _tbPotr.Text.Trim();
        string? plat = string.IsNullOrWhiteSpace(_tbPlat.Text) ? null : _tbPlat.Text.Trim();
        decimal total = (decimal)_selected.Total;

        int cex = int.TryParse(_tbCex.Text.Trim(), out int c) ? c : 0;

        var transfer = new GpriGras
        {
            Table = _rbGpri.Checked ? "GPRI" : "GRAS",
            Dt = _selected.WagonTime.Date,
            Vr = _selected.TrainTime.TimeOfDay,
            VR_PRV = _selected.WagonTime.TimeOfDay,
            Nvag = nvag,
            Gruz = isTara ? "Тара" : _tbGruz.Text.Trim(),
            Brutto = isTara ? 0m : total,
            TarBrs = isTara ? total : null,
            Netto = isTara ? null : (tarDok.HasValue ? total - tarDok.Value : null),
            Npp = _selected.Number,
            Mode = _selected.Mode,
            Potr = potr ?? "",
            Plat = plat ?? "",
            Cex = cex,
        };

        _btnTransfer.Enabled = false;
        try
        {
            _fdb ??= new FactoryRepository();
            await _fdb.InsertAsync(transfer);
            RememberLastProcessedFirebirdRecord(transfer);
            AuditLogger.Action(AuditLogger.RecordTransferred,
                "FirebirdRecord", $"{transfer.Table} nvag={transfer.Nvag}",
                "Firebird", _selected.Id.ToString());
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "FirebirdRecord", transfer.Table, "Firebird", ex.Message);
            MessageBox.Show("Не удалось перенести запись в систему учёта.\nОбратитесь к администратору.",
                "Перенос", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _btnTransfer.Enabled = true;
            return;
        }

        try
        {
            if (_ldb is null)
                throw new InvalidOperationException("Local repository is not initialized.");

            await _ldb.MarkTransferredAsync(_selected.Id);
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "LocalWagon", "MarkTransferredAsync", "PostgreSQL", ex.Message);
            MessageBox.Show("Запись перенесена в систему учёта, но локальная запись не помечена как переданная.\nОбновите список или обратитесь к администратору.",
                "Локальная база", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        await LoadBothGridsAsync();
        SelectPendingRowById(transferredLocalId);
    }

    private async void BtnSave_Click(object? sender, EventArgs e)
    {
        if (_selectedFb == null) return;

        string nvag = _tbNvag.Text.Trim();
        if (string.IsNullOrEmpty(nvag))
        {
            MessageBox.Show("Введите номер вагона (NVAG).", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _tbNvag.Focus();
            return;
        }

        bool isTara = _rbTara.Checked;
        decimal? selectedTara = _cmbTar.SelectedItem is TaraOption taraOpt ? taraOpt.Brutto : null;
        if (isTara && !selectedTara.HasValue)
        {
            MessageBox.Show("Выберите значение тары.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _cmbTar.Focus();
            return;
        }

        string? potr = string.IsNullOrWhiteSpace(_tbPotr.Text) ? null : _tbPotr.Text.Trim();
        string? plat = string.IsNullOrWhiteSpace(_tbPlat.Text) ? null : _tbPlat.Text.Trim();

        var updated = new GpriGras
        {
            Id = _selectedFb.Id,
            Table = _selectedFb.Table,
            Dt = _selectedFb.Dt,
            Vr = _selectedFb.Vr,
            Nvag = nvag,
            Gruz = isTara ? "Тара" : (string.IsNullOrWhiteSpace(_tbGruz.Text) ? "" : _tbGruz.Text.Trim()),
            Brutto = isTara ? 0m : _selectedFb.Brutto,
            TarBrs = isTara ? selectedTara : null,
            Netto = isTara ? null : (selectedTara.HasValue ? _selectedFb.Brutto - selectedTara.Value : null),
            Npp = _selectedFb.Npp,
            Cex = int.TryParse(_tbCex.Text.Trim(), out int c) ? c : _selectedFb.Cex,
            Potr = potr ?? "",
            Plat = plat ?? "",
        };

        _btnSave.Enabled = false;

        try
        {
            _fdb ??= new FactoryRepository();
            await _fdb.UpdateAsync(updated);
            RememberLastProcessedFirebirdRecord(updated);
            AuditLogger.Action(AuditLogger.RecordUpdated,
                "FirebirdRecord", $"{updated.Table} nvag={updated.Nvag}",
                "Firebird", updated.Id.ToString());
            await LoadBothGridsAsync();
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "FirebirdRecord", _selectedFb?.Table ?? "GPRI", "Firebird", ex.Message);
            MessageBox.Show("Не удалось сохранить изменения в системе учёта.\nОбратитесь к администратору.",
                "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _btnSave.Enabled = true;
        }
    }

    private void ClearTopPanel(bool clearGridSelection = true)
    {
        _selected = null;
        _selectedFb = null;
        _lblDt.Text = _lblVr.Text = _lblNpp.Text = _lblMode.Text = "—";
        _lblBrutto.Text = _lblNetto.Text = "—";
        _cmbTar.Items.Clear();
        _cmbTar.SelectedIndex = -1;
        _localInjectedTareOption = null;
        _rbBrutto.Checked = true;
        _tbNvag.Clear();
        _tbGruz.Clear();
        _tbGruz.Enabled = true;
        _btnTransfer.Enabled = false;
        _btnTransfer.Visible = true;
        _btnSave.Enabled = false;
        _btnSave.Visible = false;
        if (clearGridSelection)
        {
            _gridPend.ClearSelection();
            _gridDone.ClearSelection();
        }
        UpdateContextUi();
    }
}
