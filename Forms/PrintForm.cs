using System.ComponentModel;
using System.Diagnostics;
using Vesy13.Application;
using Vesy13.Models;
using Vesy13.Services.Repositories;

namespace Vesy13.Forms;

/// <summary>
/// Форма формирования отвесных: поиск по Firebird с фильтрами, выбор строк чекбоксами,
/// генерация PDF через QuestPDF и открытие его средствами ОС.
/// </summary>
public partial class PrintForm : Form
{
    private FactoryRepository? _fdb;
    private List<GpriGras>             _records = [];

    public PrintForm()
    {
        InitializeComponent();
    }

    public PrintForm(FactoryRepository fdb)
    {
        _fdb = fdb;
        InitializeComponent();
    }

    // ── Lifecycle ──────────────────────────────────────────────────────────

    private void ApplyTheme()
    {
        BackColor = UiColors.AppBackground;
        _pnlTop.BackColor = UiColors.FilterBar;
        _pnlStatus.BackColor = UiColors.StatusBar;
        _rbGpri.Font          = UiFonts.BodyBold;
        _rbGpri.ForeColor     = UiColors.TextPrimary;
        _rbGras.Font          = UiFonts.BodyBold;
        _rbGras.ForeColor     = UiColors.TextPrimary;
        _lblDateFrom.Font     = UiFonts.Body;
        _lblDateFrom.ForeColor = UiColors.TextMuted;
        _dtpFrom.Font         = UiFonts.Body;
        _dtpFrom.BackColor    = UiColors.InputBack;
        _dtpFrom.ForeColor    = UiColors.InputFore;
        _lblDateTo.Font       = UiFonts.Body;
        _lblDateTo.ForeColor   = UiColors.TextMuted;
        _dtpTo.Font           = UiFonts.Body;
        _dtpTo.BackColor      = UiColors.InputBack;
        _dtpTo.ForeColor      = UiColors.InputFore;
        _lblGruz.Font         = UiFonts.Body;
        _lblGruz.ForeColor    = UiColors.TextMuted;
        _txtGruz.Font         = UiFonts.Body;
        _txtGruz.BackColor    = UiColors.InputBack;
        _txtGruz.ForeColor    = UiColors.InputFore;
        _lblNvag.Font         = UiFonts.Body;
        _lblNvag.ForeColor    = UiColors.TextMuted;
        _txtNvag.Font         = UiFonts.Body;
        _txtNvag.BackColor    = UiColors.InputBack;
        _txtNvag.ForeColor    = UiColors.InputFore;
        _lblPotr.Font         = UiFonts.Body;
        _lblPotr.ForeColor    = UiColors.TextMuted;
        _txtPotr.Font         = UiFonts.Body;
        _txtPotr.BackColor    = UiColors.InputBack;
        _txtPotr.ForeColor    = UiColors.InputFore;
        _btnFind.Font         = UiFonts.BodyBold;
        _btnFind.BackColor    = UiColors.PrimaryAction;
        _btnFind.ForeColor    = UiColors.TextOnDark;
        _btnClearFilters.Font = UiFonts.Body;
        _btnClearFilters.BackColor = UiColors.NeutralAction;
        _btnClearFilters.ForeColor = UiColors.TextPrimary;
        _grid.Font            = UiFonts.GridBody;
        _grid.BackgroundColor = UiColors.Surface;
        _grid.BorderStyle     = BorderStyle.None;
        _grid.ColumnHeadersDefaultCellStyle.Font = UiFonts.GridHeader;
        _grid.ColumnHeadersDefaultCellStyle.BackColor = UiColors.GridHeaderBack;
        _grid.ColumnHeadersDefaultCellStyle.ForeColor = UiColors.GridHeaderText;
        _grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiColors.GridHeaderBack;
        _grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiColors.GridHeaderText;
        _grid.DefaultCellStyle.BackColor = UiColors.Surface;
        _grid.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
        _grid.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
        _grid.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
        _grid.GridColor = UiColors.GridLine;
        _lblSlipNum.Font      = UiFonts.Body;
        _lblSlipNum.ForeColor  = UiColors.TextOnDarkMuted;
        _txtSlipNum.Font      = UiFonts.Body;
        _txtSlipNum.BackColor = UiColors.InputBack;
        _txtSlipNum.ForeColor = UiColors.InputFore;
        _lblFrom.Font         = UiFonts.Body;
        _lblFrom.ForeColor     = UiColors.TextOnDarkMuted;
        _txtFrom.Font         = UiFonts.Body;
        _txtFrom.BackColor    = UiColors.InputBack;
        _txtFrom.ForeColor    = UiColors.InputFore;
        _lblTo.Font           = UiFonts.Body;
        _lblTo.ForeColor      = UiColors.TextOnDarkMuted;
        _txtTo.Font           = UiFonts.Body;
        _txtTo.BackColor      = UiColors.InputBack;
        _txtTo.ForeColor      = UiColors.InputFore;
        _btnPreview.Font      = UiFonts.BodyBold;
        _btnPreview.BackColor = UiColors.InfoAction;
        _btnPreview.ForeColor = UiColors.TextOnDark;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        ApplyTheme();
        AuditLogger.Action(AuditLogger.FormOpened, "Form", "PrintForm");
        SetupGridColumns();
        _dtpFrom.Value = DateTime.Today.AddDays(-7);
        _dtpTo.Value   = DateTime.Today;
        _rbGpri.Checked = true;
    }

    private void SetupGridColumns()
    {
        _grid.Columns.Add(new DataGridViewCheckBoxColumn
        {
            HeaderText = "✓",
            Width = 38,
            SortMode = DataGridViewColumnSortMode.NotSortable,
            ThreeState = false,
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата",        Width = 115, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Время",       Width = 85,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Вагон",       Width = 120, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Груз",        Width = 210, SortMode = DataGridViewColumnSortMode.NotSortable,
                                                          AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Режим",       Width = 105, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Брутто",      Width = 85,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тара",        Width = 85,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Нетто",       Width = 85,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Потребитель", Width = 155, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Плательщик",  Width = 155, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Цех",         Width = 55,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "№п/п",        Width = 60,  SortMode = DataGridViewColumnSortMode.NotSortable });
    }

    // ── Handlers ───────────────────────────────────────────────────────────

    private async void BtnFind_Click(object? sender, EventArgs e)
    {
        var table = _rbGpri.Checked ? "GPRI" : "GRAS";
        var nvag  = NullIfEmpty(_txtNvag.Text);
        var gruz  = NullIfEmpty(_txtGruz.Text);
        var potr  = NullIfEmpty(_txtPotr.Text);
        //long? ndok = long.TryParse(_txtNdok.Text.Trim(), out long n) ? n : null;

        if (_fdb is null) return;

        _btnFind.Enabled = false;
        try
        {
            _records = await _fdb.GetForPrintAsync(table, _dtpFrom.Value.Date, _dtpTo.Value.Date, nvag, gruz, potr, null);
            LoadGrid();
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorDb, "FirebirdTable", table, "Firebird", ex.Message);
            MessageBox.Show("Не удалось загрузить данные из системы учёта.\nОбратитесь к администратору.",
                "База данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _btnFind.Enabled = true;
        }
    }

    private async void BtnPreview_Click(object? sender, EventArgs e)
    {
        if (_records.Count == 0)
        {
            MessageBox.Show("Сначала выполните поиск.", "Просмотр печати",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var toPrint = _records
            .Where((_, i) => i < _grid.Rows.Count && (bool)(_grid.Rows[i].Cells[0].Value ?? false))
            .ToList();

        if (toPrint.Count == 0) toPrint = _records;

        var doc = new WeighingSlipDocument(
            toPrint,
            _txtSlipNum.Text.Trim(),
            _txtFrom.Text.Trim(),
            _txtTo.Text.Trim(),
            _dtpFrom.Value.Date,
            _dtpTo.Value.Date,
            _rbGpri.Checked ? "приход" : "расход");

        var tempPath = Path.Combine(Path.GetTempPath(), $"otves_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

        _btnPreview.Enabled = false;
        try
        {
            await Task.Run(() => doc.Generate(tempPath));
            Process.Start(new ProcessStartInfo(tempPath) { UseShellExecute = true });
            AuditLogger.Action(AuditLogger.SlipGenerated,
                "WeighingSlip", $"№{_txtSlipNum.Text.Trim()} rows={toPrint.Count}",
                "QuestPDF", tempPath);
        }
        catch (Exception ex)
        {
            AuditLogger.Error(AuditLogger.ErrorPdf, "WeighingSlip", $"№{_txtSlipNum.Text.Trim()}", "QuestPDF", ex.Message);
            MessageBox.Show("Не удалось сформировать PDF.\nОбратитесь к администратору.",
                "Печать", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _btnPreview.Enabled = true;
        }
    }

    private void BtnClearFilters_Click(object? sender, EventArgs e)
    {
        _txtGruz.Clear();
        _txtNvag.Clear();
        _txtPotr.Clear();
        //_txtNdok.Clear();
        _dtpFrom.Value = DateTime.Today.AddDays(-7);
        _dtpTo.Value   = DateTime.Today;
    }

    // ── Helpers ────────────────────────────────────────────────────────────

    private void LoadGrid()
    {
        _grid.Rows.Clear();
        foreach (var r in _records)
        {
            _grid.Rows.Add(
                false,
                r.Dt.ToString("dd.MM.yyyy"),
                r.Vr.ToString(@"hh\:mm\:ss"),
                r.Nvag,
                r.Gruz,
                r.Mode,
                r.Brutto.ToString("F2"),
                r.TarBrs?.ToString("F2") ?? "",
                r.Netto?.ToString("F2") ?? "",
                r.Potr,
                r.Plat,
                r.Cex > 0 ? r.Cex.ToString() : "",
                r.Npp);
        }
    }

    private static string? NullIfEmpty(string s) =>
        string.IsNullOrWhiteSpace(s) ? null : s.Trim();
}
