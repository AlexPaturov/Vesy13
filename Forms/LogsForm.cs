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
        SetupGrid();
        _dtpFrom.Value = DateTime.Today;
        _dtpTo.Value   = DateTime.Now;
    }

    private void SetupGrid()
    {
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Время",        Width = 170, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "EventId",      Width = 75,  SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Результат",    Width = 130, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Пользователь", Width = 150, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Домен",        Width = 115, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Сервис",       Width = 100, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тип объекта",  Width = 130, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Объект",       Width = 190, SortMode = DataGridViewColumnSortMode.NotSortable,
                                                          AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дескриптор",   Width = 105, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Компьютер",    Width = 120, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "IP",           Width = 120, SortMode = DataGridViewColumnSortMode.NotSortable });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "PID",          Width = 65,  SortMode = DataGridViewColumnSortMode.NotSortable });
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
            MessageBox.Show($"Ошибка загрузки логов:\n{ex.Message}", "Логи",
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

    private void BtnBack_Click(object? sender, EventArgs e) => Close();
}
