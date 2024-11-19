using Microcharts.Maui;
using Lab9.Models;
using Lab9.Services;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using Microcharts;
using Lab9.ViewModels;

namespace Lab9.Pages
{
    public partial class Chart : ContentPage
    {
        public Chart()
        {
            InitializeComponent();
            BindingContext = new ChartViewModel();
        }
    }
}
