using GalaSoft.MvvmLight;
using PLANNER.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

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
        private string _category;
        private DateTime _date;
        private decimal _amount;
        private string _note;
        private List<string> _categories;

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

        public string Category
        {
            get => _category;
            set => Set(ref _category, value);
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

        public List<string> Categories
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
            var categotries=ServiceCategory.GetCategories();
        }

        private void LoadCategories()
        {
            // Загрузка категорий на основе типа транзакции
            if (TransactionType == "Expense")
            {
                Categories = new List<string> { "Food", "Rent", "Utilities", "Transportation", "Entertainment", "Healthcare", "Education", "Clothing", "Insurance", "Miscellaneous" };
            }
            else if (TransactionType == "Income")
            {
                Categories = new List<string> { "Salary", "Business", "Investment", "Freelance", "RentalIncome", "Royalties", "Dividends", "Gifts", "Grants", "Miscellaneous" };
            }
            else
            {
                Categories = new List<string>();
            }
        }

        private void AddTransaction()
        {
            // Создаем новую транзакцию и сохраняем её
            var transaction = new Transaktion
            {
                transaktion_date = Date,
                amount = TransactionType == "Expense" ? -Math.Abs(Amount) : Math.Abs(Amount), // Если тип "Expense", делаем сумму отрицательной
                note_id = 0, // Предположим, что это значение будет получено или установлено
                category_id = 1, // Здесь нужно связать с выбранной категорией, например, через ID
                bankaccount_id = 1 // Примерное значение, должно быть получено из текущего контекста
            };

            // Вызов метода сервиса для добавления транзакции
            ServiceTransaktion.CreateTransaktion(transaction);
        }
    }
}
