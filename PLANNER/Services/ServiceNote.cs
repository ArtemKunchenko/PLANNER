using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PLANNER.Models
{
    public static class ServiceNote
    {
        private static readonly DbContextOptions<AppDbContext> _options;
        private static readonly IConfiguration _config;

        static ServiceNote()
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

        public static void CreateNote(Note note)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Notes.Add(note);
                context.SaveChanges();
            }
        }

        public static void UpdateNote(Note note)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                context.Notes.Update(note);
                context.SaveChanges();
            }
        }

        public static void DeleteNote(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                var note = context.Notes.Find(id);
                if (note != null)
                {
                    context.Notes.Remove(note);
                    context.SaveChanges();
                }
            }
        }

        public static List<Note> GetNotes()
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Notes.ToList();
            }
        }

        public static Note GetNote(int id)
        {
            using (var context = new AppDbContext(_options, _config))
            {
                return context.Notes.Find(id);
            }
        }
    }
}
