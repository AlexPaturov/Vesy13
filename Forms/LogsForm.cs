using System.Text;
using Vesy13.Models;
using Vesy13.Services.Repositories;

namespace Vesy13.Forms;

/// <summary>
/// Форма просмотра журнала аудита из таблицы audit_log PostgreSQL.
/// Фильтр по дате и времени; строки Audit Failure выделяются красным.
/// </summary>
public partial class LogsForm : Form
{
    public LogsForm()
    {
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyTheme();
        SetupGrid();
        btnCsvImport.Click += BtnCsvImport_Click;
        _dtpFrom.Value = DateTime.Today;
        _dtpTo.Value = DateTime.Now;
    }

    private void ApplyTheme()
    {
        BackColor = UiColors.AppBackground;
        _pnlFilter.BackColor = UiColors.FilterBar;
        _pnlStatus.BackColor = UiColors.StatusBar;

        _lblFrom.Font = UiFonts.Body;
        _lblFrom.ForeColor = UiColors.TextPrimary;
        _lblTo.Font = UiFonts.Body;
        _lblTo.ForeColor = UiColors.TextPrimary;
        _btnFind.Font = UiFonts.Body;
        _btnFind.BackColor = UiColors.InfoAction;
        _btnFind.ForeColor = UiColors.TextOnDark;
        _grid.ColumnHeadersDefaultCellStyle.Font = UiFonts.GridHeader;
        _grid.ColumnHeadersDefaultCellStyle.BackColor = UiColors.GridHeaderBack;
        _grid.ColumnHeadersDefaultCellStyle.ForeColor = UiColors.GridHeaderText;
        _grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiColors.GridHeaderBack;
        _grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiColors.GridHeaderText;
        _grid.DefaultCellStyle.Font = UiFonts.GridBody;
        _grid.DefaultCellStyle.BackColor = UiColors.AppBackground;
        _grid.DefaultCellStyle.ForeColor = UiColors.TextPrimary;
        _grid.DefaultCellStyle.SelectionBackColor = UiColors.GridSelectionBack;
        _grid.DefaultCellStyle.SelectionForeColor = UiColors.GridSelectionText;
        _grid.AlternatingRowsDefaultCellStyle.BackColor = UiColors.GridAlternateRow;
        _grid.BackgroundColor = UiColors.AppBackground;
        _grid.GridColor = UiColors.GridLine;
    }

    private void SetupGrid()
    {
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Время", Width = 170, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "EventId", Width = 75, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Результат", Width = 130, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Пользователь", Width = 150, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Домен", Width = 115, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Сервис", Width = 100, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тип объекта", Width = 130, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Объект", Width = 260, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дескриптор", Width = 105, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Компьютер", Width = 120, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "IP", Width = 120, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "PID", Width = 65, SortMode = DataGridViewColumnSortMode.NotSortable });
    }

    private async void BtnFind_Click(object? sender, EventArgs e)
    {
        _btnFind.Enabled = false;
        try
        {
            var records = await AuditLogger.GetLogsAsync(_dtpFrom.Value, _dtpTo.Value);
            LoadGrid(records);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки логов:\r\n{ex.Message}", "Логи",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _btnFind.Enabled = true;
        }
    }

    private void LoadGrid(List<AuditRecord> records)
    {
        _grid.Rows.Clear();
        foreach (var r in records)
        {
            int idx = _grid.Rows.Add(
                r.TimeCreated?.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss") ?? "",
                r.EventId?.ToString() ?? "",
                r.Keywords ?? "",
                r.SubjectUserName ?? "",
                r.SubjectDomainName ?? "",
                r.ObjectServer ?? "",
                r.ObjectType ?? "",
                r.ObjectName ?? "",
                r.ObjectHandle ?? "",
                r.Computer ?? "",
                r.IpAddress ?? "",
                r.ProcessId?.ToString() ?? "");

            if (r.Keywords?.Contains("Failure") == true)
                _grid.Rows[idx].DefaultCellStyle.BackColor = UiColors.GridAlertRow;
        }
    }

    private void BtnCsvImport_Click(object? sender, EventArgs e)
    {
        if (_grid.Rows.Count == 0)
        {
            MessageBox.Show("Нет данных для выгрузки.", "CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dlg = new SaveFileDialog
        {
            Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
            DefaultExt = "csv",
            AddExtension = true,
            FileName = $"logs_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
        };

        if (dlg.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            using var stream = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            using var writer = new StreamWriter(stream, new UTF8Encoding(true));

            writer.WriteLine(string.Join(";", _grid.Columns.Cast<DataGridViewColumn>().Select(c => CsvEscape(c.HeaderText))));

            foreach (DataGridViewRow row in _grid.Rows.Cast<DataGridViewRow>())
            {
                if (row.IsNewRow)
                    continue;

                writer.WriteLine(string.Join(";", row.Cells.Cast<DataGridViewCell>().Select(c => CsvEscape(c.Value?.ToString() ?? string.Empty))));
            }

            writer.Flush();
            MessageBox.Show("CSV-файл выгружен.", "CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось выгрузить CSV:\r\n{ex.Message}", "CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static string CsvEscape(string value)
    {
        if (value.Contains('"') || value.Contains(';') || value.Contains('\r') || value.Contains('\n'))
            return '"' + value.Replace("\"", "\"\"") + '"';

        return value;
    }
}
