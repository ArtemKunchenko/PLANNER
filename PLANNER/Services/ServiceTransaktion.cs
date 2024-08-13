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
    public static class ServiceTransaktion
    {
        private static readonly DbContextOptions<AppDbContext> _options;
        private static readonly IConfiguration _config;

        static ServiceTransaktion()
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

        public static void CreateTransaktion(Transaktion transaktion)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Transaktions.Add(transaktion);
                context.SaveChanges();
            }
        }

        public static void UpdateTransaktion(Transaktion transaktion)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Transaktions.Update(transaktion);
                context.SaveChanges();
            }
        }

        public static void DeleteTransaktion(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                var transaktion = context.Transaktions.Find(id);
                if (transaktion != null)
                {
                    context.Transaktions.Remove(transaktion);
                    context.SaveChanges();
                }
            }
        }

        public static List<Transaktion> GetTransaktions()
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Transaktions.ToList();
            }
        }

        public static Transaktion GetTransaktion(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Transaktions.Find(id);
            }
        }
    }
}
