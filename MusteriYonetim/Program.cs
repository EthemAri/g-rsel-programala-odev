using MusteriYonetim.Data;

namespace MusteriYonetim;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        DatabaseHelper.Initialize();
        Application.Run(new MainForm());
    }
}
