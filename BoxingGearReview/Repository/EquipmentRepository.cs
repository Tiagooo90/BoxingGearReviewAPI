using BoxingGearReview.Data;
using BoxingGearReview.Interfaces;
using BoxingGearReview.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingGearReview.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<EquipmentRepository> _logger;
        private DataContext _dbContext;

        public EquipmentRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public EquipmentRepository(DataContext context, ILogger<EquipmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        // Métodos Síncronos
        public bool CreateEquipment(Equipment equipment)
        {
            _context.Equipments.Add(equipment);
            return Save();
        }

        public bool DeleteEquipment(Equipment equipment)
        {
            _context.Equipments.Remove(equipment);
            return Save();
        }

        public bool EquipmentExists(int id)
        {
            return _context.Equipments.Any(e => e.Id == id);
        }

        public Equipment GetEquipment(int id)
        {
            return _context.Equipments.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Equipment> GetEquipments()
        {
            return _context.Equipments.OrderBy(e => e.Id).ToList();
        }

        public ICollection<Equipment> GetEquipmentsByBrand(int brandId)
        {
            return _context.Equipments
                           .Where(e => e.BrandId == brandId)
                           .OrderBy(e => e.Name)
                           .ToList();
        }

        public ICollection<Equipment> GetEquipmentsByCategory(int categoryId)
        {
            return _context.Equipments
                           .Where(e => e.CategoryId == categoryId)
                           .OrderBy(e => e.Name)
                           .ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateEquipment(Equipment equipment)
        {
            _context.Equipments.Update(equipment);
            return Save();
        }



    }
   
}
