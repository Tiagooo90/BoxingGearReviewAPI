using BoxingGearReview.Models;

namespace BoxingGearReview.Interfaces
{
    public interface IBrandRepository
    {
        ICollection<Brand> GetBrands();

        Brand GetBrand(int id);

        bool BrandExists(int id);

        bool Save();

        bool CreateBrand(Brand brand);

        bool UpdateBrand(Brand brand);

        bool DeleteBrand(Brand brand);

  

    }
}
