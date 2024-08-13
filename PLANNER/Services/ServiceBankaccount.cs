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
    public static class ServiceBankaccount
    {
        private static readonly DbContextOptions<AppDbContext> _options;
        private static readonly IConfiguration _config;

        static ServiceBankaccount()
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

        public static void CreateBankaccount(Bankaccount bankaccount)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Bankaccounts.Add(bankaccount);
                context.SaveChanges();
            }
        }

        public static void UpdateBankaccount(Bankaccount bankaccount)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Bankaccounts.Update(bankaccount);
                context.SaveChanges();
            }
        }

        public static void DeleteBankaccount(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                var bankaccount = context.Bankaccounts.Find(id);
                if (bankaccount != null)
                {
                    context.Bankaccounts.Remove(bankaccount);
                    context.SaveChanges();
                }
            }
        }

        public static List<Bankaccount> GetBankaccounts()
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Bankaccounts.ToList();
            }
        }

        public static Bankaccount GetBankaccount(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Bankaccounts.Find(id);
            }
        }
    }
}
