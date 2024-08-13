using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PLANNER.Models
{
    public static class ServiceExchange
    {
        private static readonly DbContextOptions<AppDbContext> _options;
        private static readonly IConfiguration _config;

        static ServiceExchange()
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

        public static void CreateExchange(Exchange exchange)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Exchanges.Add(exchange);
                context.SaveChanges();
            }
        }

        public static void UpdateExchange(Exchange exchange)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Exchanges.Update(exchange);
                context.SaveChanges();
            }
        }

        public static void DeleteExchange(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                var exchange = context.Exchanges.Find(id);
                if (exchange != null)
                {
                    context.Exchanges.Remove(exchange);
                    context.SaveChanges();
                }
            }
        }

        public static List<Exchange> GetExchanges()
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Exchanges.ToList();
            }
        }

        public static Exchange GetExchange(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Exchanges.Find(id);
            }
        }
    }
}
