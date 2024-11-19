using Lab9.ViewModels.Tables;

namespace Lab9.Pages.Tables;

public partial class Staff : ContentPage
{
	public Staff()
	{
		InitializeComponent();
        BindingContext = new StaffViewModel();
    }
}