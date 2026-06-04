namespace Vesy13.Forms;

partial class DynamicWeighingForm
{
    private void InitializeComponent()
    {
        _gbDir   = new GroupBox();
        _rbPlus  = new RadioButton();
        _rbMinus = new RadioButton();

        _gbDir.SuspendLayout();
        SuspendLayout();

        // ── Direction group box ───────────────────────────────────────────────
        _rbPlus.Text     = "→ (+)";
        _rbPlus.Location = new Point(12, 20);
        _rbPlus.AutoSize = true;
        _rbPlus.Checked  = true;
        _rbPlus.Font     = new Font("Segoe UI", 10F);

        _rbMinus.Text     = "← (–)";
        _rbMinus.Location = new Point(130, 20);
        _rbMinus.AutoSize = true;
        _rbMinus.Font     = new Font("Segoe UI", 10F);

        _gbDir.Text     = "Направление движения состава";
        _gbDir.Location = new Point(8, 8);
        _gbDir.Size     = new Size(320, 50);
        _gbDir.Font     = new Font("Segoe UI", 9F);
        _gbDir.Controls.Add(_rbPlus);
        _gbDir.Controls.Add(_rbMinus);

        // ── Form ──────────────────────────────────────────────────────────────
        Text       = "Взвешивание — Динамика";
        ClientSize = new Size(560, 616);
        Controls.Add(_gbDir);

        _gbDir.ResumeLayout(false);
        _gbDir.PerformLayout();
        ResumeLayout(false);
    }

    private GroupBox    _gbDir;
    private RadioButton _rbPlus;
    private RadioButton _rbMinus;
}
