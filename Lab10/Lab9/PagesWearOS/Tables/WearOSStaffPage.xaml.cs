using Lab9.ViewModels.Tables;

namespace Lab9.PagesWearOS.Tables;
public partial class WearOSStaff : ContentPage
{
	public WearOSStaff()
	{
		InitializeComponent();
        BindingContext = new StaffViewModel();
    }
}