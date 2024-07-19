using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANNER.Models
{
    public class ServiceCategory
    {
        private readonly AppDbContext _context;
        public ServiceCategory(AppDbContext context)
        {
            _context = context;
        }
        public void CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

        }
        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
        public Category GetCategory(int id)
        {
            var category = _context.Categories.Find(id);
            return category;
        }
    }
}
