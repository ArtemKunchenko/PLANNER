using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANNER.Models
{
    public class ServiceCurrency
    {
        private readonly AppDbContext _context;
        public ServiceCurrency(AppDbContext context)
        {
            _context = context;
        }
        public void CreateCurrency(Currency currency)
        {
            _context.Currencies.Add(currency);
            _context.SaveChanges();

        }
        public void UpdateCurrency(Currency currency)
        {
            _context.Currencies.Update(currency);
            _context.SaveChanges();
        }
        public void DeleteCurrency(int id)
        {
            var currency = _context.Currencies.Find(id);
            if (currency != null)
            {
                _context.Currencies.Remove(currency);
                _context.SaveChanges();
            }
        }
        public List<Currency> GetCurrencies()
        {
            return _context.Currencies.ToList();
        }
        public Currency GetCurrency(int id)
        {
            var currency = _context.Currencies.Find(id);
            return currency;
        }
    }
}
