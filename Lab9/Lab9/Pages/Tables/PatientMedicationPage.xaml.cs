using Lab9.ViewModels.Tables;

namespace Lab9.Pages.Tables;

public partial class PatientMedication : ContentPage
{
    public PatientMedication()
    {
        InitializeComponent();
        BindingContext = new PatientMedicationViewModel();
    }
}