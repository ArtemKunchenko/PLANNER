using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TransactionApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string category = CategoryComboBox.Text;
            string date = DatePicker.SelectedDate?.ToString("dd-MM-yyyy");

            string note = NoteTextBox.Text;


        }
        private void AmountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
