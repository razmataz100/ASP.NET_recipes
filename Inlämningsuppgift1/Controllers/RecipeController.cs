using Inlämningsuppgift1.Models.DTOs;
using Inlämningsuppgift1.Models.Entities;
using Inlämningsuppgift1.Services.Interfaces;
using Inlämningsuppgift1.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inlämningsuppgift1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;


        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddRecipe(RecipeAddDTO recipeDto)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            bool categoryExists =_recipeService.AddRecipe(recipeDto, currentUserId);

            if (!categoryExists)
            {
                return BadRequest("Category is not valid. Try again.");
            }

            return Ok("Recipe Added.");
        }

        [HttpDelete("{recipeId}")]
        [Authorize]
        public IActionResult RemoveRecipe(int recipeId)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            bool deletionResult = _recipeService.RemoveRecipe(recipeId, currentUserId);

            if (!deletionResult)
            {
                return Unauthorized("Can only delete your own Recipes.");
            }

            return Ok("Recipe removed.");
        }

        [HttpPut("{recipeId}")]
        [Authorize]
        public IActionResult UpdateRecipe(int recipeId, RecipeAddDTO recipeDto)
        {

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            bool updateResult = _recipeService.UpdateRecipe(recipeId, recipeDto, currentUserId);

            if (!updateResult)
            {
                return Unauthorized("Can only update your own recipes.");
            }

            return Ok("Recipe updated.");
        }

        [HttpGet("search/{searchTerm}")]
        [AllowAnonymous]
        public IActionResult SearchRecipeByTitle(string searchTerm)
        {
            List<RecipeDisplayDTO> recipes = _recipeService.SearchRecipesByTitle(searchTerm);

            if (recipes == null || recipes.Count == 0)
            {
                return NotFound("No Recipes found. Try again.");
            }

            return Ok(recipes);
        }

        [HttpGet("getAll")]
        public IActionResult GetAllRecipes()
        {
            List<RecipeDisplayDTO> recipes = _recipeService.GetAllRecipes();

            if (recipes == null || recipes.Count == 0)
            {
                return NotFound("No recipes found.");
            }

            return Ok(recipes);
        }
    }
}
