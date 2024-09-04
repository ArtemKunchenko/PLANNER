using PLANNER.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TransaktionList
{
    public partial class FilterPage : Page
    {


        public FilterPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocatorProvider.Locator.TransactionListViewModel;
        }

    }
}

