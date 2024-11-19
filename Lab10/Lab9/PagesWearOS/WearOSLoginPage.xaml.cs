using Lab9.ViewModels;

namespace Lab9.PagesWearOS;

public partial class WearOSLogin : ContentPage
{
	public WearOSLogin()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}