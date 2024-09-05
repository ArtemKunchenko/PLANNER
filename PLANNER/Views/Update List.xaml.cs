using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace PLANNER
{
    public partial class Update_List : Page
    {
        public ObservableCollection<Record> Records { get; set; }

        public Update_List()
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

        private void ClearFiltersButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGrid.ItemsSource = Records;

            DatePickerFilter.SelectedDate = null;
            FilterTypeComboBox.SelectedIndex = -1;
            CategoryComboBox.SelectedIndex = -1;
            AmountTextBox.Clear();
            NoteTextBox.Clear();
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