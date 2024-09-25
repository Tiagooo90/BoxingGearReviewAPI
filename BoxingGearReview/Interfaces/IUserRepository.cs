using BoxingGearReview.Models;
using Microsoft.EntityFrameworkCore;

namespace BoxingGearReview.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User GetUser(int Id);

        User GetUser(string name);

        User GetUserByEmail(string email);

        decimal GetUserRating(int userId);

        public bool CreateUser(User user);

        public bool UpdateUser(User user);

        public bool DeleteUser(User user);


        bool UserExists(int userId);

    }
}
