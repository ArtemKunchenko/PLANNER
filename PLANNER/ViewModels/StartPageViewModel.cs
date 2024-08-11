using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using PLANNER.Models;

namespace PLANNER.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        private decimal _usdRate;
        private decimal _eurRate;
        private decimal _currentBalance;
        private decimal _currentBalanceUSD;
        private decimal _currentBalanceEUR;
        private decimal _monthlyExpenses;
        private decimal _annualExpenses;
        private decimal _monthlyIncomes;
        private decimal _annualIncomes;

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

        public StartPageViewModel()
        {
            //Значения для примера. Необходима интеграция с СУБД
            CurrentBalance = 1000000;
            MonthlyExpenses = 500;
            AnnualExpenses = 2000;
            MonthlyIncomes = 30000;
            AnnualIncomes = 900000;
            LoadExchangingRatesAsync();
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
    }
}
