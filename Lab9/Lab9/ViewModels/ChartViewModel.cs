using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab9.Services;
using Microcharts;
using SkiaSharp;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab9.ViewModels
{
    public class ChartViewModel : INotifyPropertyChanged
    {
        private readonly StaffService _staffService;
        private Chart _chart;

        public Chart ChartData
        {
            get => _chart;
            set
            {
                _chart = value;
                OnPropertyChanged();
            }
        }

        public ChartViewModel()
        {
            _staffService = new StaffService(new HttpClient());
            LoadChartData();
        }

        private async void LoadChartData()
        {
            var staffList = await _staffService.GetStaffAsync();

            int maleCount = staffList.Count(s => s.Gender == "M");
            int femaleCount = staffList.Count(s => s.Gender == "F");

            var entries = new List<ChartEntry>
            {
                new ChartEntry(maleCount)
                {
                    Label = "Male",
                    ValueLabel = maleCount.ToString(),
                    Color = SKColor.Parse("#FF6347")
                },
                new ChartEntry(femaleCount)
                {
                    Label = "Female",
                    ValueLabel = femaleCount.ToString(),
                    Color = SKColor.Parse("#87CEFA")
                }
            };

            ChartData = new PieChart { Entries = entries };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}