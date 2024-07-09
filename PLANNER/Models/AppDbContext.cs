using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;


namespace PLANNER.Models
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Bankaccount> Bankaccounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaktion> Transaktions { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Note> Notes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>()
           .Property(c => c.updated_at)
           .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Exchange>()
                .HasOne(e => e.CurrencyFrom)
                .WithMany()
                .HasForeignKey(e => e.currency_from)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exchange>()
                .HasOne(e => e.CurrencyTo)
                .WithMany()
                .HasForeignKey(e => e.currency_to)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.username)
                .IsUnique();

            modelBuilder.Entity<Bankaccount>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.user_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaktion>()
                .HasOne(t => t.Currency)
                .WithMany()
                .HasForeignKey(t => t.currency_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaktion>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.category_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaktion>()
                .HasOne(t => t.Bankaccount)
                .WithMany()
                .HasForeignKey(t => t.bankaccount_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Balance>()
                .HasOne(b => b.Bankaccount)
                .WithOne()
                .HasForeignKey<Balance>(b => b.bankaccount_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Transaktion)
                .WithOne()
                .HasForeignKey<Note>(n => n.transaktion_id)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}


