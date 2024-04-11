using Inlämningsuppgift1.Models.DTOs;
using Inlämningsuppgift1.Models.Entities;
using Inlämningsuppgift1.Repository.Interfaces;
using Inlämningsuppgift1.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inlämningsuppgift1.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public bool RegisterUser(User user)
        {
            if (_userRepo.GetUserByUsername(user.Username) != null)
            {
                return false;
            }

            _userRepo.RegisterUser(user);
            return true;      
        }

        public string Login(UserLoginDTO user)
        {
            var authenticatedUser = _userRepo.AuthenticateUser(user.Username, user.Password);

            if (authenticatedUser == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, authenticatedUser.UserId.ToString()),
                new Claim(ClaimTypes.Role, "LoggedInUser")
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#123456789101112"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
            issuer: "http://localhost:5203/",
            audience: "http://localhost:5203/",
            claims: claims,
            expires: DateTime.Now.AddHours(1), 
            signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        public bool RemoveUser(int userId)
        {
            var userToRemove = _userRepo.GetUserById(userId);
            if (userToRemove == null) 
            {
                return false;
            }

            _userRepo.RemoveUser(userToRemove);
            return true;
        }

        public bool UpdateUser(int userId, User user)
        {
            var currentUser = _userRepo.GetUserById(userId);

            if(currentUser == null)
            {
                return false;
            }

            currentUser.Email = user.Email;
            currentUser.Username = user.Username;
            currentUser.Password = user.Password;

            _userRepo.UpdateUser(currentUser);
            return true;
        }
    }
}
