using Vesy13.Services.Hardware;
using Vesy13.Services.Repositories;

namespace Vesy13;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        using var sim = new SimA04Reader();
        var       ldb = new LocalRepository();
        ldb.LoadAsync().GetAwaiter().GetResult();
        System.Windows.Forms.Application.Run(new MainForm(sim, ldb));
    }
}