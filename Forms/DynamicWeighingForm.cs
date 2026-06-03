using Vesy13.Services;

namespace Vesy13.Forms;

public class DynamicWeighingForm : WeighingFormBase
{
    private RadioButton _rbPlus  = null!;
    private RadioButton _rbMinus = null!;

    protected override bool   ShowDirectionColumn => true;
    protected override string GetDirection()      => _rbPlus.Checked ? "→ (+)" : "← (–)";

    protected override bool ValidateBeforeWeigh()
    {
        if (_rbPlus.Checked || _rbMinus.Checked) return true;
        MessageBox.Show("Выберите направление движения состава.",
            "Взвешивание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
    }

    protected override double ToTonnes(int adcCode) =>
        _calib.ConvertDynamic(adcCode, GetDirection());

    protected override string GetMode() => "ДИНАМИКА";

    public DynamicWeighingForm(AdcService adc, CalibrationService calib, DatabaseService db) : base(adc, calib, db)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text          = "Взвешивание — Динамика";
        StartPosition = FormStartPosition.CenterScreen;
        KeyPreview    = true;
        BackColor     = Color.FromArgb(240, 242, 245);

        // Direction selector
        var gbDir = new GroupBox
        {
            Text     = "Направление движения состава",
            Location = new Point(8, 8),
            Size     = new Size(320, 50),
            Font     = new Font("Segoe UI", 9),
        };
        _rbPlus  = new RadioButton { Text = "→ (+)",  Location = new Point(12, 20), Font = new Font("Segoe UI", 10), Checked = true };
        _rbMinus = new RadioButton { Text = "← (–)", Location = new Point(130, 20), Font = new Font("Segoe UI", 10) };
        gbDir.Controls.AddRange(new Control[] { _rbPlus, _rbMinus });

        _lblChannel = new Label
        {
            Location  = new Point(336, 22),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 10),
            ForeColor = Color.FromArgb(80, 80, 80),
        };

        BuildSharedLayout(topY: 66);

        Controls.AddRange(new Control[] { gbDir, _lblChannel });

        // height = topY(66) + display(158) + gap(6) + weigh(54) + gap(4) + row(32) + gap(6) + grid(10*22+28) + statusbar(34)
        ClientSize = new Size(560, 66 + 158 + 6 + 54 + 4 + 32 + 6 + 248 + 34 + 8);
    }
}
