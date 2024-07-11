using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANNER.Models
{
    public class ServiceBankaccount
    {
        private readonly AppDbContext _context;
        public ServiceBankaccount(AppDbContext context)
        {
            _context = context;
        }
        public void CreateBankaccount(Bankaccount bankaccount)
        {
            _context.Bankaccounts.Add(bankaccount);
            _context.SaveChanges();

        }
        public void UpdateBankaccount(Bankaccount bankaccount)
        {
            _context.Bankaccounts.Update(bankaccount);
            _context.SaveChanges();
        }
        public void DeleteBankaccount(int id)
        {
            var bankaccount = _context.Bankaccounts.Find(id);
            if (bankaccount != null)
            {
                _context.Bankaccounts.Remove(bankaccount);
                _context.SaveChanges();
            }
        }
        public List<Bankaccount> GetBankaccounts()
        {
            return _context.Bankaccounts.ToList();
        }
        public Bankaccount GetBankaccount(int id)
        {
            var bankaccount = _context.Bankaccounts.Find(id);
            return bankaccount;
        }
    }
}
