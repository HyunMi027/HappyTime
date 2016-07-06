using System.Windows;

namespace HappyTime
{
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {

        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            HappyTime.Properties.Settings.Default.Save();
        }
    }
}
