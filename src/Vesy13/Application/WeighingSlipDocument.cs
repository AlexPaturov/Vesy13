using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Vesy13.Models;

namespace Vesy13.Application;

/// <summary>
/// QuestPDF-документ отвесной: шапка с реквизитами, таблица взвешиваний, итоговая строка ВСЕГО.
/// Вызов Generate() записывает PDF на диск, после чего его открывает ОС.
/// </summary>
public sealed class WeighingSlipDocument : IDocument
{
    private readonly List<GpriGras> _records;
    private readonly string         _slipNumber;
    private readonly string         _from;
    private readonly string         _to;
    private readonly DateTime       _dateFrom;
    private readonly DateTime       _dateTo;
    private readonly string         _direction;

    public WeighingSlipDocument(
        List<GpriGras> records,
        string slipNumber, string from, string to,
        DateTime dateFrom, DateTime dateTo,
        string direction)
    {
        _records    = records;
        _slipNumber = slipNumber;
        _from       = from;
        _to         = to;
        _dateFrom   = dateFrom;
        _dateTo     = dateTo;
        _direction  = direction;
    }

    public void Generate(string path) => this.GeneratePdf(path);

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(1.5f, Unit.Centimetre);
            page.DefaultTextStyle(x => x.FontSize(9));

            page.Header().Column(col =>
            {
                col.Item().Row(row =>
                {
                    row.RelativeItem()
                        .Text(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"))
                        .FontSize(8);
                    row.RelativeItem()
                        .AlignCenter()
                        .Text("весы 13")
                        .FontSize(8);
                    row.RelativeItem()
                        .AlignRight()
                        .Text(txt =>
                        {
                            txt.Span("лист ").FontSize(8);
                            txt.CurrentPageNumber().FontSize(8);
                        });
                });

                col.Item()
                    .PaddingTop(6)
                    .AlignCenter()
                    .Text($"Отвесная №{_slipNumber}")
                    .FontSize(13).Bold();

                col.Item()
                    .PaddingTop(4)
                    .Text($"За период с {_dateFrom:dd.MM.yyyy} по {_dateTo:dd.MM.yyyy}    {_direction}")
                    .FontSize(9);

                col.Item().Text($"Откуда: {_from}").FontSize(9);
                col.Item().PaddingBottom(6).Text($"Куда: {_to}").FontSize(9);
            });

            page.Content().Table(table =>
            {
                table.ColumnsDefinition(cols =>
                {
                    cols.ConstantColumn(65);
                    cols.ConstantColumn(52);
                    cols.ConstantColumn(38);
                    cols.ConstantColumn(72);
                    cols.RelativeColumn();
                    cols.ConstantColumn(52);
                    cols.ConstantColumn(52);
                    cols.ConstantColumn(52);
                });

                table.Header(h =>
                {
                    static IContainer Hdr(IContainer c) =>
                        c.Background("#D0D8F0").BorderBottom(1).Padding(2);

                    Hdr(h.Cell()).Text("Дата").Bold().FontSize(8);
                    Hdr(h.Cell()).Text("Время").Bold().FontSize(8);
                    Hdr(h.Cell()).AlignLeft().Text("№п/п").Bold().FontSize(8);
                    Hdr(h.Cell()).Text("№вагона").Bold().FontSize(8);
                    Hdr(h.Cell()).Text("Наименование груза").Bold().FontSize(8);
                    Hdr(h.Cell()).AlignRight().Text("Брутто").Bold().FontSize(8);
                    Hdr(h.Cell()).AlignRight().Text("Тара").Bold().FontSize(8);
                    Hdr(h.Cell()).AlignRight().Text("нетто").Bold().FontSize(8);
                });

                for (int i = 0; i < _records.Count; i++)
                {
                    var r  = _records[i];
                    var bg = i % 2 == 0 ? "#FFFFFF" : "#F2F6FF";

                    IContainer Row(IContainer c) => c.Background(bg).Padding(2);

                    Row(table.Cell()).Text(r.Dt.ToString("dd.MM.yyyy")).FontSize(8);
                    Row(table.Cell()).Text(r.Vr.ToString(@"hh\:mm\:ss")).FontSize(8);
                    Row(table.Cell()).AlignLeft().Text(r.Npp.ToString()).FontSize(8);
                    Row(table.Cell()).Text(r.Nvag).FontSize(8);
                    Row(table.Cell()).Text(r.Gruz).FontSize(8);
                    Row(table.Cell()).AlignRight().Text(r.Brutto.ToString("F2")).FontSize(8);
                    Row(table.Cell()).AlignRight().Text(r.TarBrs?.ToString("F2") ?? "0.00").FontSize(8);
                    Row(table.Cell()).AlignRight().Text(r.Netto?.ToString("F2") ?? "").FontSize(8);
                }

                var totalBrutto = _records.Sum(r => r.Brutto);
                var totalTar    = _records.Sum(r => r.TarBrs ?? 0m);
                var totalNetto  = _records.Sum(r => r.Netto  ?? 0m);

                table.Cell().ColumnSpan(5u).BorderTop(1).Padding(2).Text("ВСЕГО").Bold().FontSize(8);
                table.Cell().BorderTop(1).Padding(2).AlignRight().Text(totalBrutto.ToString("F2")).Bold().FontSize(8);
                table.Cell().BorderTop(1).Padding(2).AlignRight().Text(totalTar.ToString("F2")).Bold().FontSize(8);
                table.Cell().BorderTop(1).Padding(2).AlignRight().Text(totalNetto.ToString("F2")).Bold().FontSize(8);
            });
        });
    }
}
