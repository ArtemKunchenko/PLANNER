using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PLANNER.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;

namespace PLANNER.ViewModels
{
    public class ChartPageViewModel : ViewModelBase
    {
        private Canvas _chartCanvas;
        public ChartPageViewModel()
        {
            
            _chartCanvas = new Canvas();
          
        }

        public ObservableCollection<string> AvailableTypes { get; }
        public ObservableCollection<int> AvailableYears { get; }

        private string _selectedType;
        public string SelectedType
        {
            get => _selectedType;
            set => Set(ref _selectedType, value);
        }

        private int? _selectedYear;
        public int? SelectedYear
        {
            get => _selectedYear;
            set => Set(ref _selectedYear, value);
        }

        public ICommand UpdateChartCommand { get; }

        public ChartPageViewModel(Canvas chartCanvas)
        {
            _chartCanvas = chartCanvas;

            AvailableTypes = new ObservableCollection<string> { "Income", "Expense" };
            AvailableYears = new ObservableCollection<int>(Enumerable.Range(DateTime.Now.Year - 50, 51).Reverse());

            SelectedType = "Income";  
            SelectedYear = DateTime.Now.Year;  

            UpdateChartCommand = new RelayCommand(UpdateChart);

            UpdateChart();
        }

        private void UpdateChart()
        {
            var transactions = ApplyFilters();

           
            _chartCanvas.Children.Clear();

            
            const int barWidth = 40;
            int shiftRight = 50;

            
            var monthlyData = Enumerable.Range(1, 12)
                .Select(month => new MonthlyData { Month = month, Amount = 0 })
                .ToList();

            
            var transactionData = transactions
                .GroupBy(t => t.transaktion_date.Month)
                .Select(g => new MonthlyData { Month = g.Key, Amount = g.Sum(t => Math.Abs(t.amount)) })
                .ToList();

           
            foreach (var data in transactionData)
            {
                var existingData = monthlyData.First(d => d.Month == data.Month);
                existingData.Amount = data.Amount;
            }

            
            foreach (var data in monthlyData)
            {
                Rectangle bar = new Rectangle
                {
                    Width = barWidth,
                    Height = Math.Min((double)(data.Amount / 100000 * 450), 450),  
                    Fill = Brushes.SteelBlue
                };

              
                Canvas.SetBottom(bar, 20);  
                Canvas.SetLeft(bar, shiftRight);
                _chartCanvas.Children.Add(bar);

                shiftRight += 50;
            }

            
            AddAxesLabels();
        }



        private void AddAxesLabels()
        {
            
            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            for (int i = 0; i < months.Length; i++)
            {
                TextBlock monthLabel = new TextBlock
                {
                    Text = months[i],
                    FontSize = 16,
                    FontWeight = FontWeights.Bold
                };
                Canvas.SetLeft(monthLabel, 50 + i * 50);
                Canvas.SetBottom(monthLabel, 0);
                _chartCanvas.Children.Add(monthLabel);
            }

           
            for (int i = 0; i <= 20; i++)  
            {
                TextBlock yLabel = new TextBlock
                {
                    Text = $"{i * 5}k",
                    FontSize = 16,
                    FontWeight = FontWeights.Bold
                };
                Canvas.SetLeft(yLabel, 0);
                Canvas.SetBottom(yLabel, 20 + i * 20);  
                _chartCanvas.Children.Add(yLabel);
            }
        }

        private IQueryable<Transaktion> ApplyFilters()
        {
            var transactions = ServiceTransaktion.GetTransaktionsWithCategories().AsQueryable();

            if (!string.IsNullOrEmpty(SelectedType))
            {
                bool isExpense = SelectedType.Equals("Expense", StringComparison.OrdinalIgnoreCase);
                transactions = transactions.Where(t => (isExpense && t.amount < 0) || (!isExpense && t.amount >= 0));
            }

            if (SelectedYear.HasValue)
            {
                transactions = transactions.Where(t => t.transaktion_date.Year == SelectedYear.Value);
            }

            return transactions;
        }
    }
}
