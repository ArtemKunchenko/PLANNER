using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANNER.Models
{
    public class ServiceTransaktion
    {
        private readonly AppDbContext _context;
        public ServiceTransaktion(AppDbContext context)
        {
            _context = context;
        }
        public void CreateTransaktion(Transaktion transaktion)
        {
            _context.Transaktions.Add(transaktion);
            _context.SaveChanges();

        }
        public void UpdateTransaktion(Transaktion transaktion)
        {
            _context.Transaktions.Update(transaktion);
            _context.SaveChanges();
        }
        public void DeleteTransaktion(int id)
        {
            var transaktion = _context.Transaktions.Find(id);
            if (transaktion != null)
            {
                _context.Transaktions.Remove(transaktion);
                _context.SaveChanges();
            }
        }
        public List<Transaktion> GetTransaktions()
        {
            return _context.Transaktions.ToList();
        }
        public Transaktion GetTransaktion(int id)
        {
            var transaktion = _context.Transaktions.Find(id);
            return transaktion;
        }
    }
}
