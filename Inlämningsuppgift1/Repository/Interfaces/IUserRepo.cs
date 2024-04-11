using Inlämningsuppgift1.Models.Entities;

namespace Inlämningsuppgift1.Repository.Interfaces
{
    public interface IUserRepo
    {
        public void RegisterUser(User user);
        public void RemoveUser(User userToRemove);
        public void UpdateUser(User userToUpdate);
        User AuthenticateUser(string username, string password);
        User GetUserById(int userID);
        User GetUserByUsername(string username);
    }
}
