using BoxingGearReview.Data;
using BoxingGearReview.Interfaces;
using BoxingGearReview.Models;
using Microsoft.EntityFrameworkCore;

public class ReviewRepository : IReviewRepository
{
    private readonly DataContext _context;

    public ReviewRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<Review> GetReviews()
    {
        return _context.Reviews.ToList();
    }

    public Review GetReview(int id)
    {
        return _context.Reviews.FirstOrDefault(r => r.Id == id);
    }

    public ICollection<Review> GetReviewsByEquipment(int equipmentId)
    {
        return _context.Reviews.Where(r => r.EquipmentId == equipmentId).ToList();
    }

    public ICollection<Review> GetReviewsByUser(int userId)
    {
        return _context.Reviews.Where(r => r.UserId == userId).ToList();
    }

    public bool ReviewExists(int id)
    {
        return _context.Reviews.Any(r => r.Id == id);
    }



 

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }

    public bool CreateReview(Review review)
    {
        _context.Reviews.Add(review);
        return Save();
    }

    public bool UpdateReview(Review review)
    {
        _context.Reviews.Update(review);
        return Save();
    }

    public bool DeleteReview(Review review)
    {
        _context.Reviews.Remove(review);
        return Save();
    }

  
  


}
