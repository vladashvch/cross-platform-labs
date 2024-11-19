using Lab9.ViewModels.Tables;

namespace Lab9.Pages.Tables;

public partial class MedicatonType : ContentPage
{
    public MedicatonType()
    {
        InitializeComponent();
        BindingContext = new MedicationTypeViewModel();
    }
}