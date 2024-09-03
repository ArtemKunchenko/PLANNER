using GalaSoft.MvvmLight;
using PLANNER.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PLANNER.ViewModels
{
    public class AddTransactionViewModel : ViewModelBase
    {
        #region VARIABLES
        public enum FinancialCategory
        {
            // Expense Categories
            Food = 1,
            Rent,
            Utilities,
            Transportation,
            Entertainment,
            Healthcare,
            Education,
            Clothing,
            Insurance,
            MiscellaneousExpense,

            // Income Categories
            Salary,
            Business,
            Investment,
            Freelance,
            RentalIncome,
            Royalties,
            Dividends,
            Gifts,
            Grants,
            MiscellaneousIncome
        }
        private string _transactionType;
        private Category _selectedCategory;
        private DateTime _date;
        private decimal _amount;
        private string _note;
        private ObservableCollection<Category> _categories;

        public string TransactionType
        {
            get => _transactionType;
            set
            {
                if (Set(ref _transactionType, value))
                {
                    LoadCategories();
                }
            }
        }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => Set(ref _selectedCategory, value);
        }

        public DateTime Date
        {
            get => _date;
            set => Set(ref _date, value);
        }

        public decimal Amount
        {
            get => _amount;
            set => Set(ref _amount, value);
        }

        public string Note
        {
            get => _note;
            set => Set(ref _note, value);
        }

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => Set(ref _categories, value);
        }

        public ICommand AddTransactionCommand { get; }

        #endregion

        public AddTransactionViewModel()
        {
            Date = DateTime.Now;
            AddTransactionCommand = new RelayCommand(AddTransaction);
            InitializeDefaultData();
            LoadCategories();
        }

        private void InitializeDefaultData()
        {
            var categories = ServiceCategory.GetCategories();
            if (categories.Count == 0)
            {
                var categoriesEnum = Enum.GetValues(typeof(FinancialCategory));

                foreach (var categoryEnum in categoriesEnum)
                {
                    var categoryName = categoryEnum.ToString();
                    var category = new Category { name_category = categoryName };
                    ServiceCategory.CreateCategory(category);
                }
            }
        }

        private void LoadCategories()
        {
            var categoriesFromDb = ServiceCategory.GetCategories();
            var sortedCategories = categoriesFromDb.OrderBy(c => c.name_category).ToList();
            Categories = new ObservableCollection<Category>(sortedCategories);

        }
        private void ResetFields()
        {
            TransactionType = null; 
            SelectedCategory = null;
            Date = DateTime.Now; 
            Amount = 0; 
            Note = string.Empty; 
        }
        private void UpdateStartPageData()
        {
            var startPageViewModel = ViewModelLocatorProvider.Locator.StartPageViewModel;
            startPageViewModel.ReloadData();
        }

        private void AddTransaction()
        {
           
            if (SelectedCategory == null)
            {
                return;
            }
            
            int selectedCurrencyId = 1; 
            if(string.IsNullOrWhiteSpace(Note)) Note= string.Empty;

            
            var transaction = new Transaktion
            {
                transaktion_date = Date, 
                amount = TransactionType == "Expense" ? -Math.Abs(Amount) : Math.Abs(Amount), 
                currency_id = selectedCurrencyId,   
                note=Note,
                category_id = SelectedCategory.category_id, 
                bankaccount_id = 1 
            };
           

            ServiceTransaktion.CreateTransaktion(transaction);
            var transactions = ServiceTransaktion.GetTransaktions();
            var savedTransaction = transactions.Last();
            

            var balances=ServiceBalance.GetBalances();
            Balance currentBalance = null;
            
            if (balances != null && balances.Any()) 
            {
                currentBalance = balances.Last(); 
            }
            else
            {
                currentBalance = new Balance { bankaccount_id = 1, total_amount = 0, created_at = Date };
            }

            Balance newBalance=new Balance { bankaccount_id = 1, total_amount = currentBalance.total_amount += transaction.amount, created_at = Date }
            ;
            
            ServiceBalance.CreateBalance(newBalance);

            var currentBankaccount=ServiceBankaccount.GetBankaccount(transaction.bankaccount_id);
            balances = ServiceBalance.GetBalances();
            var lastBalance= balances.Last();
            currentBankaccount.balance_id = lastBalance.balance_id;
            ServiceBankaccount.UpdateBankaccount(currentBankaccount);

            MessageBox.Show("Transaktion added");
            ResetFields();
            UpdateStartPageData();
        }

    }
}
