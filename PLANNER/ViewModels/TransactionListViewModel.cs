using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PLANNER.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using static PLANNER.ViewModels.AddTransactionViewModel;

namespace PLANNER.ViewModels
{
    public class TransactionListViewModel : ViewModelBase
    {
        private string _selectedType;
        private FinancialCategory? _selectedCategory;
        private int? _selectedYear;
        private DateTime _selectedDate;

        public string SelectedType
        {
            get => _selectedType;
            set => Set(ref _selectedType, value);
        }

        public FinancialCategory? SelectedCategory
        {
            get => _selectedCategory;
            set => Set(ref _selectedCategory, value);
        }

        public int? SelectedYear
        {
            get => _selectedYear;
            set => Set(ref _selectedYear, value);
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => Set(ref _selectedDate, value);
        }

        public ObservableCollection<string> AvailableTypes { get; }
        public ObservableCollection<FinancialCategory> AvailableCategories { get; }
        public ObservableCollection<int> AvailableYears { get; }

        private ObservableCollection<Transaktion> _filteredTransactions;
        public ObservableCollection<Transaktion> FilteredTransactions
        {
            get => _filteredTransactions;
            set => Set(ref _filteredTransactions, value);
        }
        private ObservableCollection<ListTransactionForTablecs> _listTransaktionForTable;
        public ObservableCollection<ListTransactionForTablecs> ListTransaktionForTable
        {
            get => _listTransaktionForTable;
            set => Set(ref _listTransaktionForTable, value);
        }

        public ICommand ApplyFiltersCommand { get; }
        public ICommand ApplyDateFilterCommand { get; }
        public ICommand ClearFiltersCommand { get; }

        public TransactionListViewModel()
        {
            AvailableTypes = new ObservableCollection<string> { "Income", "Expense" };
            AvailableCategories = new ObservableCollection<FinancialCategory>(Enum.GetValues(typeof(FinancialCategory)).Cast<FinancialCategory>());
            AvailableYears = new ObservableCollection<int>(Enumerable.Range(DateTime.Now.Year - 50, 51).Reverse());
            SelectedDate = DateTime.Now;

            ApplyFiltersCommand = new RelayCommand(ApplyFilters);
            ApplyDateFilterCommand = new RelayCommand(ApplyDateFilter);
            ClearFiltersCommand = new RelayCommand(ClearFilters);

            LoadInitialTransactions();
        }

        private void LoadInitialTransactions()
        {
            var currentYear = DateTime.Now.Year;
            var transactions = ServiceTransaktion.GetTransaktionsWithCategories()
                .Where(t => t.transaktion_date.Year == currentYear)
                .OrderByDescending(t => t.transaktion_date);
            FilteredTransactions = new ObservableCollection<Transaktion>(transactions);
            SetDataForTable(FilteredTransactions);
        }

        private void ApplyFilters()
        {
            var transactions = ServiceTransaktion.GetTransaktionsWithCategories().AsQueryable();

            if (!string.IsNullOrEmpty(SelectedType))
            {
                bool isExpense = SelectedType.Equals("Expense", StringComparison.OrdinalIgnoreCase);
                transactions = transactions.Where(t => (isExpense && t.amount < 0) || (!isExpense && t.amount >= 0));
            }

            if (SelectedCategory.HasValue)
            {
                transactions = transactions.Where(t => t.category_id == (int)SelectedCategory.Value);
            }

            if (SelectedYear.HasValue)
            {
                transactions = transactions.Where(t => t.transaktion_date.Year == SelectedYear.Value);
            }

            FilteredTransactions = new ObservableCollection<Transaktion>(transactions.OrderByDescending(t => t.transaktion_date));
            SetDataForTable(FilteredTransactions);
        }

        private void ApplyDateFilter()
        {
            var transactions = ServiceTransaktion.GetTransaktionsWithCategories()
                .Where(t => t.transaktion_date.Date == SelectedDate.Date)
                .OrderByDescending(t => t.transaktion_date);
            FilteredTransactions = new ObservableCollection<Transaktion>(transactions);
            SetDataForTable(FilteredTransactions);
        }

        private void ClearFilters()
        {
            SelectedType = null;
            SelectedCategory = null;
            SelectedYear = null;
            SelectedDate = DateTime.Now;

            LoadInitialTransactions();
        }

        private void SetDataForTable(ObservableCollection<Transaktion> filteredTransaktions)
        {
            ListTransaktionForTable = new ObservableCollection<ListTransactionForTablecs>();
            foreach (var t in filteredTransaktions)
            {
                string type=String.Empty;
                if (t.amount > 0) type = "Income";
                else type = "Expense";
                ListTransaktionForTable.Add(new ListTransactionForTablecs { amount = t.amount, incomeOrExpence=type, Category=t.Category, note=t.note, transaktion_date=t.transaktion_date });

            }
        }
    }
}
