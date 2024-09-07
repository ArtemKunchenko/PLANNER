using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PLANNER.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PLANNER.ViewModels
{
    public class UpdateListViewModel : ViewModelBase
    {
        private ListTransactionForTablecs _selectedTransaction;

        public ObservableCollection<ListTransactionForTablecs> Records { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => Set(ref _selectedDate, value);
        }

        private string _selectedType;
        public string SelectedType
        {
            get => _selectedType;
            set => Set(ref _selectedType, value);
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => Set(ref _selectedCategory, value);
        }

        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set => Set(ref _amount, value);
        }

        private string _note;
        public string Note
        {
            get => _note;
            set => Set(ref _note, value);
        }

        public UpdateListViewModel()
        {
            Records = new ObservableCollection<ListTransactionForTablecs>();
            SelectedDate = DateTime.Now; 
            LoadTransactions();
            UpdateCommand = new RelayCommand(UpdateTransaction);
            DeleteCommand = new RelayCommand(DeleteTransaction);
        }

        public void LoadTransactions()
        {
            var transactions = ServiceTransaktion.GetTransaktionsWithCategories()
                .OrderByDescending(t => t.transaktion_date)
                .Select(t => new ListTransactionForTablecs
                {
                    id = t.transaktion_id,
                    amount = t.amount,
                    incomeOrExpence = t.amount >= 0 ? "Income" : "Expense",
                    note = t.note,
                    transaktion_date = t.transaktion_date,
                    Category = t.Category
                });

           
            Records.Clear();
            foreach (var transaction in transactions)
            {
                Records.Add(transaction);
            }

           
            RaisePropertyChanged(nameof(Records));
        }

        public ListTransactionForTablecs SelectedTransaction
        {
            get => _selectedTransaction;
            set
            {
                if (Set(ref _selectedTransaction, value) && value != null)
                {
                    SelectedDate = _selectedTransaction.transaktion_date;
                    SelectedType = _selectedTransaction.incomeOrExpence;
                    SelectedCategory = _selectedTransaction.Category;
                    Amount = _selectedTransaction.amount;
                    Note = _selectedTransaction.note;

                    RaisePropertyChanged(nameof(SelectedType));  
                    RaisePropertyChanged(nameof(SelectedCategory));
                }
            }
        }

        private void UpdateTransaction()
        {
            if (SelectedTransaction != null)
            {
                var updatedTransaktion = new Transaktion
                {
                    transaktion_id = SelectedTransaction.id,
                    amount = Amount,
                    note = Note,
                    transaktion_date = SelectedDate,
                    category_id = SelectedCategory.category_id,
                };

                Transaktion updateForDB = ServiceTransaktion.GetTransaktion(updatedTransaktion.transaktion_id);
                updateForDB.amount = updatedTransaktion.amount;
                updateForDB.note = updatedTransaktion.note;
                updateForDB.transaktion_date = updatedTransaktion.transaktion_date;
                updateForDB.category_id = updatedTransaktion.category_id;

                ServiceTransaktion.UpdateTransaktion(updateForDB);

                SelectedTransaction.amount = Amount;
                SelectedTransaction.note = Note;
                SelectedTransaction.transaktion_date = SelectedDate;
                SelectedTransaction.incomeOrExpence = Amount >= 0 ? "Income" : "Expense";
                SelectedTransaction.Category = SelectedCategory;

              
                LoadTransactions();

                MessageBox.Show("Updated");
                UpdatePages();
            }
        }

        private void DeleteTransaction()
        {
            if (SelectedTransaction != null)
            {
                ServiceTransaktion.DeleteTransaktion(SelectedTransaction.id);

                
                Records.Remove(SelectedTransaction);

                
                SelectedTransaction = null;
                SelectedDate = DateTime.Now;
                SelectedType = null;
                SelectedCategory = default;
                Amount = 0;
                Note = string.Empty;

                MessageBox.Show("Deleted");
                UpdatePages();
            }
        }

        private void UpdatePages()
        {
            var startPageViewModel = ViewModelLocatorProvider.Locator.StartPageViewModel;
            startPageViewModel.ReloadData();
            var transaktionListViewModel = ViewModelLocatorProvider.Locator.TransactionListViewModel;
            transaktionListViewModel.LoadInitialTransactions();
        }
    }
}
