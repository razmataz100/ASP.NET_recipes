using Inlämningsuppgift1.Models.DTOs;
using Inlämningsuppgift1.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inlämningsuppgift1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("{recipeId}")]
        public IActionResult AddRating(int recipeId, RatingDTO ratingDto)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            bool ratingResult = _ratingService.AddRating(recipeId, ratingDto, currentUserId);
            if (!ratingResult)
            {
                return BadRequest("Can only rate other users' recipes, and only once.");
            }
            else
            {
                return Ok("Rating added.");
            }
        }
    }
}
