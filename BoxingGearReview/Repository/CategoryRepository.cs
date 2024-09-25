using BoxingGearReview.Data;
using BoxingGearReview.Interfaces;
using BoxingGearReview.Models;
using Microsoft.EntityFrameworkCore;

namespace BoxingGearReview.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {

            _context = context;

        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            return Save();
        }



        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Name).ToList();
        }

      

        public Category GetCategory(int id)
        {
            return _context.Categories
               .Include(c => c.Equipments)  // Inclui equipamentos relacionados
               .FirstOrDefault(c => c.Id == id);
        }



        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

    

        public bool UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            return Save();
        }


 
    }
}
