using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using TransaktionList;

namespace PLANNER.ViewModels
{
    class MainViewModel : ViewModelBase

    {
        private Page Page1;
        private Page Page2;
        private Page Page3;
        private Page Page4;

        private Page _currentPage;
        public Page CurrentPage 
        { 
            get { return _currentPage; } 
            set 
            { 
                _currentPage = value; 
                RaisePropertyChanged(() => CurrentPage); 
            } 
        }
        private double _frameOpasity;
        public double FrameOpasity
        {
            get { return _frameOpasity;}
            set
            {
                _frameOpasity = value;
                RaisePropertyChanged(() => FrameOpasity);
            }
        }
        public MainViewModel()
        {
            Page1 = new Page1();
            Page2 = new Page2();
            Page3 = new ChartPage();
            Page4 = new FilterPage();
            FrameOpasity = 1;
            CurrentPage = Page1;
        }
        private async void SlowOpasity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    FrameOpasity = i;
                    Thread.Sleep(50);

                }
                CurrentPage = page;
                for (double i = 0.0; i < 1.1; i += 0.1)
                {
                    FrameOpasity = i;
                    Thread.Sleep(50);

                }
            });
        }

        public ICommand page1Button_Click
        {
            get
            {
                return new RelayCommand(()=>SlowOpasity(Page1));
            }
        }
        public ICommand page2Button_Click
        {
            get
            {
                return new RelayCommand(() => SlowOpasity(Page2));
            }
        }
        public ICommand page3Button_Click
        {
            get
            {
                return new RelayCommand(() => SlowOpasity(Page3));
            }
        }
        public ICommand page4Button_Click
        {
            get
            {
                return new RelayCommand(() => SlowOpasity(Page4));
            }
        }
    }
}
