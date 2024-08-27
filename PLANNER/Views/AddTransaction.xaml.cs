using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PLANNER
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
            DataContext = new ViewModels.AddTransactionViewModel();

        }

        //private void AddButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
        //    string category = CategoryComboBox.Text;
        //    string date = DatePicker.SelectedDate?.ToString("dd-MM-yyyy");

        //    string note = NoteTextBox.Text;


        //}
        private void AmountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
