using Vesy13.Services;

namespace Vesy13.Forms;

public class StaticWeighingForm : WeighingFormBase
{
    protected override bool   ShowDirectionColumn => false;
    protected override string GetDirection()      => "";
    protected override bool   ValidateBeforeWeigh() => true;

    public StaticWeighingForm(AdcService adc) : base(adc)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text          = "Взвешивание — Статика";
        StartPosition = FormStartPosition.CenterScreen;
        KeyPreview    = true;
        BackColor     = Color.FromArgb(240, 242, 245);

        _lblChannel = new Label
        {
            Location  = new Point(8, 10),
            AutoSize  = true,
            Font      = new Font("Segoe UI", 10),
            ForeColor = Color.FromArgb(80, 80, 80),
        };

        BuildSharedLayout(topY: 34);

        Controls.Add(_lblChannel);

        // height = topY(34) + display(158) + gap(6) + weigh(54) + gap(4) + row(32) + gap(6) + grid(10*22+28) + statusbar(34)
        ClientSize = new Size(560, 34 + 158 + 6 + 54 + 4 + 32 + 6 + 248 + 34 + 8);
    }
}
