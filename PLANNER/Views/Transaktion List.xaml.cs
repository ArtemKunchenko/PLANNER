using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TransaktionList
{
    public partial class FilterPage : Page
    {
        public ObservableCollection<Record> Records { get; set; }

        public FilterPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Records = new ObservableCollection<Record>
            {
                new Record { Date = "01/01/2024", IncomeExpense = "Income", Category = "Salary", Amount = "5000", Note = "Monthly Salary" },
                new Record { Date = "02/01/2024", IncomeExpense = "Expense", Category = "Rent", Amount = "1500", Note = "Monthly Rent" }
            };

            DataGrid.ItemsSource = Records;
        }

        private void ApplyFiltersButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           
        }

        private void ApplyDateFilterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           
        }

        private void ClearFiltersButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           
        }
    }

    public class Record
    {
        public string Date { get; set; }
        public string IncomeExpense { get; set; }
        public string Category { get; set; }
        public string Amount { get; set; }
        public string Note { get; set; }
    }
}

