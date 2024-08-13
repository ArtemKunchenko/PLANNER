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
    public static class ServiceBalance
    {
        private static readonly DbContextOptions<AppDbContext> _options;
        private static readonly IConfiguration _config;

        static ServiceBalance()
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

        public static void CreateBalance(Balance balance)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Balances.Add(balance);
                context.SaveChanges();
            }
        }

        public static void UpdateBalance(Balance balance)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Balances.Update(balance);
                context.SaveChanges();
            }
        }

        public static void DeleteBalance(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                var balance = context.Balances.Find(id);
                if (balance != null)
                {
                    context.Balances.Remove(balance);
                    context.SaveChanges();
                }
            }
        }

        public static List<Balance> GetBalances()
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Balances.ToList();
            }
        }

        public static Balance GetBalance(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Balances.Find(id);
            }
        }
    }
}
