using PLANNER.Models;
using PLANNER.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLANNER
{
    
    public partial class Page1: Page
    {
        
        public Page1()
        {
            InitializeComponent();
            DataContext = ViewModelLocatorProvider.Locator.StartPageViewModel;
        }

      
    }
}
