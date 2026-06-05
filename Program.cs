using Vesy13.Services.Hardware;
using Vesy13.Services.Repositories;

namespace Vesy13;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        using var adc = new SimA04Reader();
        var       db  = new LocalRepository();
        db.LoadAsync().GetAwaiter().GetResult();
        System.Windows.Forms.Application.Run(new MainForm(adc, db));
    }
}