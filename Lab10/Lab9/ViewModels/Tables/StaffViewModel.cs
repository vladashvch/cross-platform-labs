using Lab9.Models;
using Lab9.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Lab9.ViewModels.Tables
{
    public class StaffViewModel : BindableObject
    {
        private readonly StaffService _staffService;
        private ObservableCollection<Staff> _staffList;

        public string NewFirstName { get; set; }
        public string NewLastName { get; set; }
        public string NewGender { get; set; }
        public string NewQualifications { get; set; }

        public ICommand AddStaffCommand { get; }

        public ObservableCollection<Staff> StaffList
        {
            get => _staffList;
            set
            {
                _staffList = value;
                OnPropertyChanged();
            }
        }

        public StaffViewModel()
        {
            _staffService = new StaffService(new HttpClient());
            StaffList = new ObservableCollection<Staff>();
            AddStaffCommand = new Command(OnAddStaff);
            LoadStaff();
        }

        private async Task LoadStaff()
        {
            var staff = await _staffService.GetStaffAsync();
            StaffList.Clear();
            foreach (var item in staff)
            {
                StaffList.Add(item);
            }
        }

        private async void OnAddStaff()
        {
            var newStaff = new Staff
            {
                FirstName = NewFirstName,
                LastName = NewLastName,
                Gender = NewGender,
                Qualifications = NewQualifications
            };

            var isSuccess = await _staffService.CreateStaffAsync(newStaff);
            if (isSuccess)
            {
                StaffList.Add(newStaff);
                ClearInputFields();
            }
        }
        private void ClearInputFields()
        {
            NewFirstName = string.Empty;
            NewLastName = string.Empty;
            NewGender = string.Empty;
            NewQualifications = string.Empty;
        }
        
    }
}
