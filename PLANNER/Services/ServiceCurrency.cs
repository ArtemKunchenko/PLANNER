using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANNER.Models
{
    public static class ServiceCurrency
    {
        private static readonly DbContextOptions<AppDbContext> _options;
        private static readonly IConfiguration _config;

        static ServiceCurrency()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + @"\")
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = _config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            _options = optionsBuilder.Options;
        }

        public static void CreateCurrency(Currency currency)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Currencies.Add(currency);
                context.SaveChanges();
            }
        }

        public static void UpdateCurrency(Currency currency)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Currencies.Update(currency);
                context.SaveChanges();
            }
        }

        public static void DeleteCurrency(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                var currency = context.Currencies.Find(id);
                if (currency != null)
                {
                    context.Currencies.Remove(currency);
                    context.SaveChanges();
                }
            }
        }

        public static List<Currency> GetCurrencies()
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Currencies.ToList();
            }
        }

        public static Currency GetCurrency(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Currencies.Find(id);
            }
        }
    }
   
}
