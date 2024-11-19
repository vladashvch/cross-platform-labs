using Lab9.Models;
using Lab9.Services;
using System.Collections.ObjectModel;

namespace Lab9.ViewModels.Tables
{
    public class PatientMedicationViewModel : BindableObject
    {
        private readonly PatientMedicationService _patientMedicationService;
        private ObservableCollection<PatientMedication> _patientMedicationsList;

        public ObservableCollection<PatientMedication> PatientMedicationsList
        {
            get => _patientMedicationsList;
            set
            {
                _patientMedicationsList = value;
                OnPropertyChanged();
            }
        }

        public PatientMedicationViewModel()
        {
            _patientMedicationService = new PatientMedicationService(new HttpClient());
            PatientMedicationsList = new ObservableCollection<PatientMedication>();
            LoadPatientMedications();
        }

        private async Task LoadPatientMedications()
        {
            var patientMedications = await _patientMedicationService.GetPatientMedicationsAsync();
            PatientMedicationsList.Clear();
            foreach (var item in patientMedications)
            {
                PatientMedicationsList.Add(item);
            }
        }
    }
}
