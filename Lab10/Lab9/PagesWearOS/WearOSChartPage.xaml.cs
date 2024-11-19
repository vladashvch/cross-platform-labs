using Lab9.ViewModels;

namespace Lab9.PagesWearOS
{
    public partial class WearOSChart : ContentPage
    {
        public WearOSChart()
        {
            InitializeComponent();
            BindingContext = new ChartViewModel();
        }
    }
}
