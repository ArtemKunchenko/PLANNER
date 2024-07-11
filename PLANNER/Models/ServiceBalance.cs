using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANNER.Models
{
    public class ServiceBalance
    {
        private readonly AppDbContext _context;
        public ServiceBalance(AppDbContext context)
        {
            _context = context;
        }
        public void CreateBalance(Balance balance)
        {
            _context.Balances.Add(balance);
            _context.SaveChanges();

        }
        public void UpdateBalance(Balance balance)
        {
            _context.Balances.Update(balance);
            _context.SaveChanges();
        }
        public void DeleteBalance(int id)
        {
            var balance = _context.Balances.Find(id);
            if (balance != null)
            {
                _context.Balances.Remove(balance);
                _context.SaveChanges();
            }
        }
        public List<Balance> GetBankaccounts()
        {
            return _context.Balances.ToList();
        }
        public Balance GetBankaccount(int id)
        {
            var balance = _context.Balances.Find(id);
            return balance;
        }
    }
}
