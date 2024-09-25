using System.Collections.Generic;
using System.Threading.Tasks;
using BoxingGearReview.Models;

namespace BoxingGearReview.Interfaces
{
    public interface IEquipmentRepository
    {
        // Métodos síncronos existentes
        ICollection<Equipment> GetEquipments();
        Equipment GetEquipment(int id);
        ICollection<Equipment> GetEquipmentsByCategory(int categoryId);
        ICollection<Equipment> GetEquipmentsByBrand(int brandId);
        bool EquipmentExists(int id);
        bool CreateEquipment(Equipment equipment);
        bool UpdateEquipment(Equipment equipment);
        bool DeleteEquipment(Equipment equipment);
        bool Save();


    }
}
