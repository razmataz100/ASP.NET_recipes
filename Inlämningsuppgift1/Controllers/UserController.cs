using Inlämningsuppgift1.Models.DTOs;
using Inlämningsuppgift1.Models.Entities;
using Inlämningsuppgift1.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace Inlämningsuppgift1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult RegisterUser(User user)
        {

            bool registrationResult = _userService.RegisterUser(user);

            if (!registrationResult)
            {
                return BadRequest("Username already exists. Try again.");
            }

            return Ok("User registered successfully.");
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(UserLoginDTO user)
        {

            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            var token = _userService.Login(user);
            if (token == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { Token = token });
        }


        [HttpDelete("{userId}")]
        [Authorize]
        public IActionResult RemoveUser(int userId)
        {

            //Måste vara i controllern då det är via http requestet som current user hittas.
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            if (currentUserId != userId)
            {
                return Unauthorized("Can only delete your own account.");
            }

            bool removalResult = _userService.RemoveUser(userId);

            if (!removalResult)
            {
                return NotFound("User not found.");
            }

            return Ok("User removed.");
        }

        [HttpPut("{userId}")]
        [Authorize]
        public IActionResult UpdateUser(int userId, User user)
        {

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (currentUserId != userId)
            {
                return Unauthorized("Can only update your own account.");
            }

            bool updateResult = _userService.UpdateUser(userId, user);

            if (!updateResult)
            {
                return NotFound("User not found.");
            }

            return Ok("User updated.");
        }
    }
}
