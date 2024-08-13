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
    //public class ServiceCurrency
    //{
    //    private readonly AppDbContext _context;
    //    public ServiceCurrency(AppDbContext context)
    //    {
    //        _context = context;
    //    }
    //    public void CreateCurrency(Currency currency)
    //    {
    //        _context.Currencies.Add(currency);
    //        _context.SaveChanges();

    //    }
    //    public void UpdateCurrency(Currency currency)
    //    {
    //        _context.Currencies.Update(currency);
    //        _context.SaveChanges();
    //    }
    //    public void DeleteCurrency(int id)
    //    {
    //        var currency = _context.Currencies.Find(id);
    //        if (currency != null)
    //        {
    //            _context.Currencies.Remove(currency);
    //            _context.SaveChanges();
    //        }
    //    }
    //    public List<Currency> GetCurrencies()
    //    {
    //        return _context.Currencies.ToList();
    //    }
    //    public Currency GetCurrency(int id)
    //    {
    //        var currency = _context.Currencies.Find(id);
    //        return currency;
    //    }
    //}
}
