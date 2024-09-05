using PLANNER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PLANNER
{
    public partial class ChartPage : Page
    {
        public ChartPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocatorProvider.Locator.CreateChartPageViewModel(chartCanvas);

        }

      
    }
}

