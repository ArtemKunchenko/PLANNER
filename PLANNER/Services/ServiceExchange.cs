using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PLANNER.Models
{
    public class ServiceExchange
    {
        private readonly AppDbContext _context;
        public ServiceExchange(AppDbContext context)
        {
            _context = context;
        }
        public void CreateExchange(Exchange exchange)
        {
            _context.Exchanges.Add(exchange);
            _context.SaveChanges();

        }
        public void UpdateExchange(Exchange exchange)
        {
            _context.Exchanges.Update(exchange);
            _context.SaveChanges();
        }
        public void DeleteExchange(int id)
        {
            var exchange = _context.Exchanges.Find(id);
            if (exchange != null)
            {
                _context.Exchanges.Remove(exchange);
                _context.SaveChanges();
            }
        }
        public List<Exchange> GetExchanges()
        {
            return _context.Exchanges.ToList();
        }
        public Exchange GetExchange(int id)
        {
            var exchange = _context.Exchanges.Find(id);
            return exchange;
        }
    }
}
