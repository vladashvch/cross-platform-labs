using Lab9.ViewModels.Tables;

namespace Lab9.PagesWearOS.Tables;

public partial class WearOSMedicatonType : ContentPage
{
    public WearOSMedicatonType()
    {
        InitializeComponent();
        BindingContext = new MedicationTypeViewModel();
    }
}