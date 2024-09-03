using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PLANNER.ViewModels
{
    public class ViewModelLocator
    {
        public StartPageViewModel StartPageViewModel { get; } = new StartPageViewModel();
        public AddTransactionViewModel AddTransactionViewModel { get; }= new AddTransactionViewModel();

    }
}
