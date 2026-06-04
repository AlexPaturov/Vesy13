using Vesy13.Services;

namespace Vesy13.Forms;

public partial class DynamicWeighingForm : WeighingFormBase
{
    protected override bool   ShowDirectionColumn => true;
    protected override string GetDirection()      => _rbPlus.Checked ? "→ (+)" : "← (–)";
    protected override string GetMode()           => "ДИНАМИКА";

    protected override bool ValidateBeforeWeigh()
    {
        if (_rbPlus.Checked || _rbMinus.Checked) return true;
        MessageBox.Show("Выберите направление движения состава.",
            "Взвешивание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
    }

    protected override double ToTonnes(int adcCode) =>
        _calib.ConvertDynamic(adcCode, GetDirection());

    public DynamicWeighingForm()
    {
        InitializeComponent();
    }

    public DynamicWeighingForm(AdcService adc, CalibrationService calib, DatabaseService db)
        : base(adc, calib, db)
    {
        InitializeComponent();
    }
}
