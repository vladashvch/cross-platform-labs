using System.Windows.Input;
using Lab9.Services;
using Microsoft.Maui.Controls;

namespace Lab9.ViewModels
{
    public class LoginViewModel : BindableObject
    {
        private readonly LoginService _loginService;
        private string username;
        private string password;
        private string token;
        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _loginService = new LoginService(new HttpClient());
            LoginCommand = new Command(OnLogin);
        }


        private async void OnLogin()
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Username and password are required.", "OK");
                return;
            }

            string tokenResult = await _loginService.Login(username, password);

            token = tokenResult;
            if (!string.IsNullOrEmpty(tokenResult))
            {

                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password.", "OK");
            }

        }
    }
}
