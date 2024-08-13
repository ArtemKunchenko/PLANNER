using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PLANNER.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLANNER
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {



            #region PERFORMANCE TESTS
            var config = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory() + @"\")
                           .AddJsonFile("appsettings.json")
                           .Build();


            string connectionString = config.GetConnectionString("DefaultConnection");
            //var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //optionsBuilder.UseSqlServer(connectionString);

            //using (var connection = new AppDbContext(optionsBuilder.Options, config))
            //{
            //    var serviceUser = new ServiceUser(connection);
            //    serviceUser.CreateUser(new User { username = "Bob", password = "123" });
            //}
            //using (var connection = new AppDbContext(optionsBuilder.Options, config))
            //{
            //    var serviceUser = new ServiceUser(connection);
            //    serviceUser.UpdateUser(new User { user_id = 1, username = "Anton", password = "321" });
            //}
            //using (var connection = new AppDbContext(optionsBuilder.Options, config))
            //{
            //    var serviceUser = new ServiceUser(connection);
            //    serviceUser.GetUser(2);

            //}
            //using (var connection = new AppDbContext(optionsBuilder.Options, config))
            //{
            //    var serviceUser = new ServiceUser(connection);
            //    serviceUser.GetUsers();

            //}
            //using (var connection = new AppDbContext(optionsBuilder.Options, config))
            //{
            //    var serviceUser = new ServiceUser(connection);
            //    serviceUser.DeleteUser(1);

            //} 
            #endregion



            InitializeComponent();
            InitializeDefaultData();
            DataContext = new ViewModels.MainViewModel();
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
            //var users = ServiceUser.GetUsers();
            //if (users.Count==0) ServiceUser.CreateUser(new User { username = "Admin", password = "Admin" });
        }

        private void DropDownButton_Click(object sender, RoutedEventArgs e)
        {

            DropDownPopup.IsOpen = !DropDownPopup.IsOpen;
        }

    }
}
