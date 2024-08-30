using System.Windows;
using System.Windows.Controls;

namespace PLANNER
{
    public partial class ChartPage : Page
    {
        public ChartPage()
        {
            InitializeComponent();

            ComboBox1.ItemsSource = new string[] { "Option 1", "Option 2", "Option 3" };
            ComboBox2.ItemsSource = new string[] { "Option A", "Option B", "Option C" };
        }

        private void UpdateChartButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedOption1 = ComboBox1.SelectedItem?.ToString();
            string selectedOption2 = ComboBox2.SelectedItem?.ToString();

        }
    }
}

