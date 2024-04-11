using Inlämningsuppgift1.Models.DTOs;
using Inlämningsuppgift1.Models.Entities;
using System.Security.Claims;

namespace Inlämningsuppgift1.Services.Interfaces
{
    public interface IUserService
    {
        bool RegisterUser(User user);

        bool RemoveUser(int userId);

        string Login(UserLoginDTO user);

        bool UpdateUser(int userId, User user);

    }
}
