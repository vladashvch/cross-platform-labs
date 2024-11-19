using Lab9.ViewModels.Tables;

namespace Lab9.PagesWearOS.Tables;

public partial class WearOSPatientMedication : ContentPage
{
    public WearOSPatientMedication()
    {
        InitializeComponent();
        BindingContext = new PatientMedicationViewModel();
    }
}