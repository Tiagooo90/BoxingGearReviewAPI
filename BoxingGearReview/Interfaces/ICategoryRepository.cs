using BoxingGearReview.Models;
using Microsoft.EntityFrameworkCore;

namespace BoxingGearReview.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();

        Category GetCategory(int id);

        bool CreateCategory(Category category); 

        bool UpdateCategory(Category category);

        bool DeleteCategory(Category category);


        bool CategoryExists(int id);

        bool Save();


    }
}
