using BoxingGearReview.Models;

namespace BoxingGearReview.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewsByEquipment(int equipmentId);
        ICollection<Review> GetReviewsByUser(int userId);
        bool ReviewExists(int id);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool Save();


        




    }
}
