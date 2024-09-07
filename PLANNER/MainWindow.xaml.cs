using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PLANNER.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLANNER
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();           
            DataContext = new ViewModels.MainViewModel();
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
