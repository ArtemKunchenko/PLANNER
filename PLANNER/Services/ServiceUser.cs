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
    public static class ServiceUser
    {
        private static readonly DbContextOptions<AppDbContext> _options;
        private static readonly IConfiguration _config;

        static ServiceUser()
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

        public static void CreateUser(User user)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Users.Add(user);
                context.SaveChanges();
              
            }
        }

        public static void UpdateUser(User user)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Users.Update(user);
                context.SaveChanges();
               
            }
        }

        public static void DeleteUser(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                var user = context.Users.Find(id);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }
        }

        public static List<User> GetUsers()
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Users.ToList();
            }
        }

        public static User GetUser(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Users.Find(id);
            }
        }
    }
}
