namespace ScaleListener.FaultInjection;

/// <summary>
/// Owner-drawn список истории сбоев. Сбои случаются редко (не на каждый сэмпл потока),
/// но всё равно применяем тот же паттерн, что и в основном приложении для живого лога:
/// без RichTextBox/RTF, лимит числа строк, минимум аллокаций на отрисовку - см. урок
/// про GC-давление в Vesy13 (docs/status_2026-07-03.md, "Проблема GC-давления сервисного лога").
/// </summary>
public sealed class FaultHistoryListBox : ListBox
{
    private const int LineLimit = 300;

    public FaultHistoryListBox()
    {
        DrawMode = DrawMode.OwnerDrawFixed;
        BorderStyle = BorderStyle.FixedSingle;
        BackColor = Color.White;
        Font = new Font("Courier New", 12f);
        ItemHeight = Font.Height + 2;
        IntegralHeight = false;
    }

    public void Append(FaultHistoryEntry entry)
    {
        BeginUpdate();
        try
        {
            Items.Insert(0, entry);
            while (Items.Count > LineLimit)
                Items.RemoveAt(Items.Count - 1);
        }
        finally
        {
            EndUpdate();
        }
    }

    public void ClearHistory() => Items.Clear();

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= Items.Count) return;
        e.DrawBackground();
        if (Items[e.Index] is FaultHistoryEntry entry)
        {
            Color color = entry.Kind == FaultEventKind.Stopped ? Color.DimGray : Color.Firebrick;
            TextRenderer.DrawText(e.Graphics, entry.Format(), Font, e.Bounds, color,
                TextFormatFlags.Left | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
        }
        e.DrawFocusRectangle();
    }
}
