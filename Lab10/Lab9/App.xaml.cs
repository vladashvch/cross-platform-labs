using Lab9.Pages;
using Lab9.PagesWearOS;

namespace Lab9
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            if (DeviceInfo.Idiom == DeviceIdiom.Watch)
            {
                MainPage = new NavigationPage(new PagesWearOS.WearOSLogin());
            }
            else
            {
                MainPage = new NavigationPage(new Pages.Login());
            }
        }
    }
}
