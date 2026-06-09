using System.Windows;
using StudentAccounting.Services;
using StudentAccounting.Views;

namespace StudentAccounting
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var dataService = new JsonDataService();
            Current.Properties["DataService"] = dataService;

            var authWindow = new AuthWindow();
            authWindow.Show();
        }
    }
}