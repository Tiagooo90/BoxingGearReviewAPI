using BoxingGearReview.Data;
using BoxingGearReview.Interfaces;
using BoxingGearReview.Models;
using Microsoft.EntityFrameworkCore;

namespace BoxingGearReview.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DataContext _context;

        public BrandRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Brand> GetBrands()
        {
            return _context.Brands.OrderBy(b => b.Name).ToList();
        }

        public Brand GetBrand(int id)
        {
            return _context.Brands
                           .Include(b => b.Equipments)  // Inclui equipamentos relacionados
                           .FirstOrDefault(b => b.Id == id);
        }

        public bool BrandExists(int id)
        {
            return _context.Brands.Any(b => b.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool CreateBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            return Save();
        }

        public bool UpdateBrand(Brand brand)
        {
            _context.Brands.Update(brand);
            return Save();
        }

        public bool DeleteBrand(Brand brand)
        {
            _context.Brands.Remove(brand);
            return Save();

        }

    }

}
 






