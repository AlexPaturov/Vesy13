namespace ScaleListener;

public class MainForm : Form
{
    public MainForm()
    {
        Text = "Scale Listener - главное меню";
        ClientSize = new Size(360, 180);
        MinimumSize = new Size(360, 180);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;

        var title = new Label
        {
            AutoSize = false,
            Location = new Point(16, 16),
            Size = new Size(328, 32),
            Text = "Эмулятор АЦП СИМ А04",
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(Font.FontFamily, 12f, FontStyle.Bold),
        };

        var btnStatic = new Button
        {
            Location = new Point(32, 68),
            Size = new Size(130, 52),
            Text = "Статика",
            FlatStyle = FlatStyle.Flat,
        };
        btnStatic.Click += (_, _) => OpenForm(new StaticForm());

        var btnDynamic = new Button
        {
            Location = new Point(198, 68),
            Size = new Size(130, 52),
            Text = "Динамика",
            FlatStyle = FlatStyle.Flat,
        };
        btnDynamic.Click += (_, _) => OpenForm(new DynamicForm());

        var hint = new Label
        {
            AutoSize = false,
            Location = new Point(16, 136),
            Size = new Size(328, 24),
            Text = "Открывайте один режим за раз: оба используют COM4.",
            TextAlign = ContentAlignment.MiddleCenter,
        };

        Controls.AddRange(new Control[] { title, btnStatic, btnDynamic, hint });
    }

    private void OpenForm(Form form)
    {
        using (form)
        {
            Enabled = false;
            try
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog(this);
            }
            finally
            {
                Enabled = true;
                Activate();
            }
        }
    }
}
