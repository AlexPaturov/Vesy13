namespace ScaleListener;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void BtnStatic_Click(object? sender, EventArgs e) => OpenForm(new StaticForm());
    private void BtnDynamic_Click(object? sender, EventArgs e) => OpenForm(new DynamicForm());

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
