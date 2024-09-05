using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;

namespace PLANNER.ViewModels
{
    public class ViewModelLocator
    {
        public StartPageViewModel StartPageViewModel { get; } = new StartPageViewModel();
        public AddTransactionViewModel AddTransactionViewModel { get; } = new AddTransactionViewModel();
        public TransactionListViewModel TransactionListViewModel { get; } = new TransactionListViewModel();
        public ChartPageViewModel CreateChartPageViewModel(Canvas chartCanvas)
        {
            return new ChartPageViewModel(chartCanvas);
        }

    }
}
