using Lab9.ViewModels;

namespace Lab9.Pages;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}