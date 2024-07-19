using PLANNER.Models;
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
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            LoadExchangingRatesAsync();
        }
        private async Task LoadExchangingRatesAsync()
        {
            await CurrentExchangingRate.InitializeExangingRates(); 
            InitializeTextBlocks(); 
        }
        public void InitializeTextBlocks()
        {
            var USD = CurrentExchangingRate.GetExhangingRateByCurrencyName("USD");
            var EUR = CurrentExchangingRate.GetExhangingRateByCurrencyName("EUR");

            USDTextBlock.Text = USD != null ? USD.ToString() : "USD rate not found";
            EURTextBlock.Text = EUR != null ? EUR.ToString() : "EUR rate not found";
        }
    }
}
