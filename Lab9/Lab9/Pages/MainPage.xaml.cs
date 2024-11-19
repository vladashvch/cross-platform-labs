using Lab9.ViewModels;

namespace Lab9
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
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
                    case "Go to About Page":
                        await Navigation.PushAsync(new Pages.AboutPage());
                        break;
                    case "Go to Chart Page":
                        await Navigation.PushAsync(new Pages.Chart());
                        break;
                    case "Log Out":
                        Application.Current.MainPage = new NavigationPage(new Pages.Login());
                        break;
                    case "Go to Staff table Page":
                        await Navigation.PushAsync(new Pages.Tables.Staff());
                        break;
                    case "Go to PatientMedication Page":
                        await Navigation.PushAsync(new Pages.Tables.PatientMedication());
                        break;
                    case "Go to MedicatonType table Page":
                        await Navigation.PushAsync(new Pages.Tables.MedicatonType());
                        break;
                }
            }

            viewModel.IsBusy = false;
        }
    }

}
