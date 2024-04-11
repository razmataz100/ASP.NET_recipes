using Inlämningsuppgift1.Data.Contexts;
using Inlämningsuppgift1.Models.Entities;
using Inlämningsuppgift1.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Inlämningsuppgift1.Repository.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();     
        }

        public void RemoveUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User AuthenticateUser(string username, string password) 
        { 
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }

        public void UpdateUser(User user)
        {
           _context.Entry(user).State = EntityState.Modified;
           _context.SaveChanges();
        }

        public User GetUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            return user;
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}
