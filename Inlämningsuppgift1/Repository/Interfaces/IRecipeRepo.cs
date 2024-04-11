using Inlämningsuppgift1.Models.Entities;

namespace Inlämningsuppgift1.Repository.Interfaces
{
    public interface IRecipeRepo
    {
        void AddRecipe(Recipe recipe);
        Recipe GetRecipeById(int recipeId);
        void RemoveRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        List<Recipe> SearchRecipesByTitle(string searchTerm);

        List<Recipe> GetAllRecipes();
    }
}
