using System;
using System.Collections.Generic;
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
        }

        private void DropDownButton_Click(object sender, RoutedEventArgs e)
        {

            DropDownPopup.IsOpen = !DropDownPopup.IsOpen;
        }
        private void Page1Button_Click(object sender, RoutedEventArgs e)
        {
            FrameContent.Navigate(new Page1());
            DropDownPopup.IsOpen = false;
        }
        private void Page2Button_Click(object sender, RoutedEventArgs e)
        {
            FrameContent.Navigate(new Page2());
            DropDownPopup.IsOpen = false;
        }



        private void Page3Button_Click(object sender, RoutedEventArgs e)
        {
            FrameContent.Navigate(new Page3());
            DropDownPopup.IsOpen = false;
        }
    }
}
