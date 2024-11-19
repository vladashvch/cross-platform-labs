using Lab9.ViewModels;
using Lab9.PagesWearOS.Tables;
using Lab9.PagesWearOS;

namespace Lab9.PagesWearOS
{
    public partial class WearOSMainPage : ContentPage
    {
        int count = 0;

        public WearOSMainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as MainViewModel;
            if (viewModel == null) return;

            viewModel.IsBusy = true;

            await Task.Delay(1000);

            var button = sender as Button;
            if (button != null)
            {
                switch (button.Text)
                {
                    case "About Page":
                        await Navigation.PushAsync(new WearOSAboutPage());
                        break;
                    case "Chart Page":
                        await Navigation.PushAsync(new WearOSChart());
                        break;
                    case "Log Out":
                        Application.Current.MainPage = new NavigationPage(new WearOSLogin());
                        break;
                    case "Staff Table":
                        await Navigation.PushAsync(new WearOSStaff());
                        break;
                    case "PatientMedication":
                        await Navigation.PushAsync(new WearOSPatientMedication());
                        break;
                    case "MedicationType":
                        await Navigation.PushAsync(new WearOSMedicatonType());
                        break;
                }
            }

            viewModel.IsBusy = false;
        }
    }
}
