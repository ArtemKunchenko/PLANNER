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
    public static class ServiceCategory
    {
        private static readonly DbContextOptions<AppDbContext> _options;
        private static readonly IConfiguration _config;

        static ServiceCategory()
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

        public static void CreateCategory(Category category)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public static void UpdateCategory(Category category)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Categories.Update(category);
                context.SaveChanges();
            }
        }

        public static void DeleteCategory(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                var category = context.Categories.Find(id);
                if (category != null)
                {
                    context.Categories.Remove(category);
                    context.SaveChanges();
                }
            }
        }

        public static List<Category> GetCategories()
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Categories.ToList();
            }
        }

        public static Category GetCategory(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Categories.Find(id);
            }
        }
    }
}
