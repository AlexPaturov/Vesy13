using System.Diagnostics;
using Vesy13.Application;
using Vesy13.Models;
using Vesy13.Services.Repositories;

namespace Vesy13.Forms;

public partial class PrintForm : Form
{
    private readonly FactoryRepository _fdb;
    private List<GpriGras>             _records = [];

    public PrintForm(FactoryRepository fdb)
    {
        _fdb = fdb;
        InitializeComponent();
    }

    // ── Lifecycle ──────────────────────────────────────────────────────────

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
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
            Width = 30,
            SortMode = DataGridViewColumnSortMode.NotSortable,
            ThreeState = false,
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата",        Width = 88,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Время",       Width = 65,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "№п/п",        Width = 45,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Вагон",       Width = 90,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Груз",        Width = 160, SortMode = DataGridViewColumnSortMode.NotSortable,
                                                          AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Режим",       Width = 80,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Брутто",      Width = 65,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тара",        Width = 65,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тар.Dok",     Width = 65,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Нетто",       Width = 65,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "НДОК",        Width = 80,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Потребитель", Width = 120, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Плательщик",  Width = 120, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Цех",         Width = 40,  SortMode = DataGridViewColumnSortMode.NotSortable });
    }

    // ── Handlers ───────────────────────────────────────────────────────────

    private async void BtnFind_Click(object? sender, EventArgs e)
    {
        var table = _rbGpri.Checked ? "GPRI" : "GRAS";
        var nvag  = NullIfEmpty(_txtNvag.Text);
        var gruz  = NullIfEmpty(_txtGruz.Text);
        var potr  = NullIfEmpty(_txtPotr.Text);
        long? ndok = long.TryParse(_txtNdok.Text.Trim(), out long n) ? n : null;

        _btnFind.Enabled = false;
        try
        {
            _records = await _fdb.GetForPrintAsync(
                table, _dtpFrom.Value.Date, _dtpTo.Value.Date,
                nvag, gruz, potr, ndok);
            LoadGrid();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки:\n{ex.Message}", "База данных",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            AuditLogger.Error(AuditLogger.ErrorDb, "FirebirdTable", table, "Firebird");
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
            _rbGpri.Checked ? "приход" : "расход",
            _chkPotr.Checked);

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
            MessageBox.Show($"Ошибка генерации PDF:\n{ex.Message}", "Печать",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            AuditLogger.Error(AuditLogger.ErrorPdf, "WeighingSlip", $"№{_txtSlipNum.Text.Trim()}", "QuestPDF");
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
        _txtNdok.Clear();
        _dtpFrom.Value = DateTime.Today.AddDays(-7);
        _dtpTo.Value   = DateTime.Today;
    }

    private void BtnBack_Click(object? sender, EventArgs e) => Close();

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
                r.Npp,
                r.Nvag,
                r.Gruz,
                r.Mode,
                r.Brutto.ToString("F2"),
                r.TarBrs?.ToString("F2") ?? "",
                r.TarDok?.ToString("F2") ?? "",
                r.Netto?.ToString("F2") ?? "",
                r.Ndok?.ToString() ?? "",
                r.Potr,
                r.Plat,
                r.Cex > 0 ? r.Cex.ToString() : "");
        }
    }

    private static string? NullIfEmpty(string s) =>
        string.IsNullOrWhiteSpace(s) ? null : s.Trim();
}
