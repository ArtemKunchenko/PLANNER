using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GalaSoft.MvvmLight;
using PLANNER.Models;

namespace PLANNER.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        #region VARIABLES
        private decimal _usdRate;
        private decimal _eurRate;
        private decimal _currentBalance;
        private decimal _currentBalanceUSD;
        private decimal _currentBalanceEUR;
        private decimal _monthlyExpenses;
        private decimal _annualExpenses;
        private decimal _monthlyIncomes;
        private decimal _annualIncomes;
        private DateTime? _currentDate = DateTime.Now;

        public decimal USDRate
        {
            get => _usdRate;
            set => Set(ref _usdRate, Math.Round(value, 2));
        }

        public decimal EURRate
        {
            get => _eurRate;
            set => Set(ref _eurRate, Math.Round(value, 2));
        }

        public decimal CurrentBalance
        {
            get => _currentBalance;
            set => Set(ref _currentBalance, Math.Round(value, 2));
        }

        public decimal CurrentBalanceUSD
        {
            get => _currentBalanceUSD;
            set => Set(ref _currentBalanceUSD, Math.Round(value, 2));
        }

        public decimal CurrentBalanceEUR
        {
            get => _currentBalanceEUR;
            set => Set(ref _currentBalanceEUR, Math.Round(value, 2));
        }

        public decimal MonthlyExpenses
        {
            get => _monthlyExpenses;
            set => Set(ref _monthlyExpenses, Math.Round(value, 2));
        }

        public decimal AnnualExpenses
        {
            get => _annualExpenses;
            set => Set(ref _annualExpenses, Math.Round(value, 2));
        }

        public decimal MonthlyIncomes
        {
            get => _monthlyIncomes;
            set => Set(ref _monthlyIncomes, Math.Round(value, 2));
        }

        public decimal AnnualIncomes
        {
            get => _annualIncomes;
            set => Set(ref _annualIncomes, Math.Round(value, 2));
        }

        #endregion

        public StartPageViewModel()
        {
            try
            {
                InitializeDefaultData();
                LoadCurrentValues();
                LoadExchangingRatesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private async void LoadExchangingRatesAsync()
        {
            await CurrentExchangingRate.InitializeExangingRates();
            var usdRate = CurrentExchangingRate.GetExhangingRateByCurrencyName("USD");
            var eurRate = CurrentExchangingRate.GetExhangingRateByCurrencyName("EUR");

            if (usdRate != null)
            {
                USDRate = Math.Round(usdRate.rate, 2);
                CurrentBalanceUSD = Math.Round(CurrentBalance / usdRate.rate, 2);
            }

            if (eurRate != null)
            {
                EURRate = Math.Round(eurRate.rate, 2);
                CurrentBalanceEUR = Math.Round(CurrentBalance / eurRate.rate, 2);
            }
                      
        }

        private void LoadCurrentValues()
        {
            int? currentMonth = _currentDate?.Month;
            int? currentYear = _currentDate?.Year;

            //Set current balance
            var balances = ServiceBalance.GetBalances();
            if(balances.Count != 0) 
            { 
                Balance balance=balances.Last();
                CurrentBalance = balance.total_amount;
            }
            else CurrentBalance = 0;

            //Set expenses and incomes
            var transactions = ServiceTransaktion.GetTransaktions();
            if (transactions.Count != 0)
            {

                foreach (var t in transactions)
                {
                    if (t.transaktion_date.Month == currentMonth && t.amount < 0) MonthlyExpenses += t.amount;
                    if (t.transaktion_date.Month == currentMonth && t.amount > 0) MonthlyIncomes += t.amount;
                    if (t.transaktion_date.Year == currentYear && t.amount < 0) AnnualExpenses += t.amount;
                    if (t.transaktion_date.Year == currentYear && t.amount > 0) AnnualIncomes += t.amount;
                }
                //Change sign before value
                MonthlyExpenses = -MonthlyExpenses;
                AnnualExpenses = -AnnualExpenses;
            }
            else
            {

                MonthlyExpenses = 0;
                AnnualExpenses = 0;
                MonthlyIncomes = 0;
                AnnualIncomes = 0;
            }
        }

        private void InitializeDefaultData()
        {


            var currencies = ServiceCurrency.GetCurrencies();
            if (currencies.Count == 0)
            {
                ServiceCurrency.CreateCurrency(new Currency { currency_code = "UAN" });
                ServiceCurrency.CreateCurrency(new Currency { currency_code = "USD" });
                ServiceCurrency.CreateCurrency(new Currency { currency_code = "EUR" });

            }
            var users = ServiceUser.GetUsers();
            int usered=1;

            if (users.Count == 0)
            {
                var admin = new User { username = "Admin", password = "Admin" };
                ServiceUser.CreateUser(admin );
                usered = admin.user_id;
                

            }
            else
            {
                usered = users[0].user_id;

            }
           
            try
            {
                var bankaccounts = ServiceBankaccount.GetBankaccounts();
                if (bankaccounts.Count == 0) ServiceBankaccount.CreateBankaccount(new Bankaccount { user_id = usered, balance_id = 0 });
            }
            catch (Exception ex)
            {
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
            


        }

        public void ReloadData()
        {
            MonthlyExpenses = 0;
            MonthlyIncomes= 0;
            AnnualExpenses = 0;
            AnnualIncomes = 0;
            LoadCurrentValues();
            LoadExchangingRatesAsync();
        }
    }
}
