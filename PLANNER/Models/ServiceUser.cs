using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANNER.Models
{
    public class ServiceUser
    {
        private readonly AppDbContext _context;
        public ServiceUser(AppDbContext context)
        {
            _context = context;
        }
        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public User GetUser(int id)
        {
            var user = _context.Users.Find(id);
            return user;
        }

    }
}
