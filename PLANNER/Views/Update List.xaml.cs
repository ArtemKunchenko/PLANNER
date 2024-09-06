using PLANNER.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace PLANNER
{
    public partial class Update_List : Page
    {


        public Update_List()
        {
            InitializeComponent();
            DataContext= ViewModelLocatorProvider.Locator.UpdateListViewModel;
        }

    }
}