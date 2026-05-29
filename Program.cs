using Vesy13.Services;

namespace Vesy13;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        using var adc = new AdcService();
        Application.Run(new MainForm(adc));
    }
}