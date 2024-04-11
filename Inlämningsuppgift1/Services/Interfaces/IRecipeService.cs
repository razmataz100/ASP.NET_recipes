using Inlämningsuppgift1.Models.DTOs;
using Inlämningsuppgift1.Models.Entities;

namespace Inlämningsuppgift1.Services.Interfaces
{
    public interface IRecipeService
    {
        List<RecipeDisplayDTO> SearchRecipesByTitle(string searchTerm);
        bool AddRecipe(RecipeAddDTO recipe, int userId);
        bool RemoveRecipe(int recipeId, int currentUserId);
        bool UpdateRecipe(int recipeId, RecipeAddDTO recipeDto, int currentUserId);
        List<RecipeDisplayDTO> GetAllRecipes();   
        List<RecipeDisplayDTO> MapRecipesToDTOs(List<Recipe> recipes);
        double CalculateAverageRating(List<Rating> ratings);
    }
}
