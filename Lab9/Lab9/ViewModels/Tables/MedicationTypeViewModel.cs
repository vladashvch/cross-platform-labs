using Lab9.Models;
using Lab9.Services;
using System.Collections.ObjectModel;

namespace Lab9.ViewModels.Tables
{
    public class MedicationTypeViewModel : BindableObject
    {
        private readonly MedicationTypeService _medicationTypeService;
        private ObservableCollection<MedicationType> _medicationTypeList;

        public ObservableCollection<MedicationType> MedicationTypesList
        {
            get => _medicationTypeList;
            set
            {
                _medicationTypeList = value;
                OnPropertyChanged();
            }
        }

        public MedicationTypeViewModel()
        {
            _medicationTypeService = new MedicationTypeService(new HttpClient());
            MedicationTypesList = new ObservableCollection<MedicationType>();
            LoadMedicationTypes();
        }

        private async Task LoadMedicationTypes()
        {
            var medicationTypes = await _medicationTypeService.GetMedicationTypesAsync();
            MedicationTypesList.Clear();
            foreach (var item in medicationTypes)
            {
                MedicationTypesList.Add(item);
            }
        }
    }
}