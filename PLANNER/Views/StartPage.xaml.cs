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
        Balance currentBalance = new Balance { total_amount = 1000000 };
        Balance monthlyExpences = new Balance { total_amount = 500 };
        Balance annualExpences = new Balance { total_amount = 2000 };
        Balance monthlyIncomes = new Balance { total_amount = 30000 };
        Balance annualIncomes = new Balance { total_amount = 900000 };

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
            CurrentBalanceBlock.Text = currentBalance.total_amount.ToString();
            CurrentBalanceUSDBlock.Text = (currentBalance.total_amount / USD.rate).ToString();
            CurrentBalanceEURBlock.Text = (currentBalance.total_amount / EUR.rate).ToString();
            MonthlyExpensesBlock.Text=monthlyExpences.total_amount.ToString();
            AnnualExpensesBlock.Text=annualExpences.total_amount.ToString();
            MonthlyIncomesBlock.Text=monthlyIncomes.total_amount.ToString();
            AnnualIncomesBlock.Text=annualIncomes.total_amount.ToString();
        }
    }
}
