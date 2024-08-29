using PLANNER.ViewModels;
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
           
            DataContext = ViewModelLocatorProvider.Locator.AddTransactionViewModel;

        }

     
        private void AmountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
