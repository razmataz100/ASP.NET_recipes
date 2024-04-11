using Inlämningsuppgift1.Models.DTOs;
using Inlämningsuppgift1.Models.Entities;
using Inlämningsuppgift1.Repository.Interfaces;
using Inlämningsuppgift1.Services.Interfaces;

namespace Inlämningsuppgift1.Services.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepo _recipeRepo;
        private readonly IUserRepo _userRepo;
        private readonly ICategoryRepo _categoryRepo;

        public RecipeService(IRecipeRepo recipeRepo, IUserRepo userRepo, ICategoryRepo categoryRepo)
        {
            _recipeRepo = recipeRepo;
            _userRepo = userRepo;
            _categoryRepo = categoryRepo;

        }

        public bool AddRecipe(RecipeAddDTO recipeDto, int userId)
        {
            var currentUser = _userRepo.GetUserById(userId);
            var category = _categoryRepo.GetCategory(recipeDto.CategoryName);

            if (category == null)
            {
                return false;
            }

            var recipe = new Recipe
            {
                Title = recipeDto.Title,
                Ingredients = recipeDto.Ingredients,
                Description = recipeDto.Description,
                User = currentUser,
                Category = category
            };

            _recipeRepo.AddRecipe(recipe);
            return true;
        }

        public bool RemoveRecipe(int recipeId, int userId)
        {
            var existingRecipe = _recipeRepo.GetRecipeById(recipeId);

            if (existingRecipe == null || existingRecipe.User.UserId != userId) 
            {
                return false;
            }
            _recipeRepo.RemoveRecipe(existingRecipe);
            return true;
        }

        public bool UpdateRecipe(int recipeId, RecipeAddDTO recipeDto, int currentUserId)
        {
            var existingRecipe = _recipeRepo.GetRecipeById(recipeId);

            if (existingRecipe == null || existingRecipe.User.UserId != currentUserId)
            {
                return false;
            }
            existingRecipe.Title = recipeDto.Title;
            existingRecipe.Ingredients = recipeDto.Ingredients;
            existingRecipe.Description = recipeDto.Description;

            _recipeRepo.UpdateRecipe(existingRecipe);
            return true;
        }

        public List<RecipeDisplayDTO> SearchRecipesByTitle(string searchTerm)
        {
            List<Recipe> recipes = _recipeRepo.SearchRecipesByTitle(searchTerm);
            List<RecipeDisplayDTO> recipeDTOs = MapRecipesToDTOs(recipes);

            return recipeDTOs;
        }

        public List<RecipeDisplayDTO> GetAllRecipes()
        {
            List<Recipe> recipes = _recipeRepo.GetAllRecipes();
            List<RecipeDisplayDTO> recipeDTOs = MapRecipesToDTOs(recipes);

            return recipeDTOs;
        }

        public List<RecipeDisplayDTO> MapRecipesToDTOs(List<Recipe> recipes)
        {
            return recipes.Select(r => new RecipeDisplayDTO
            {
                Title = r.Title,
                AverageRating = CalculateAverageRating(r.Ratings?.ToList()),
                Ingredients = r.Ingredients,
                Description = r.Description,
                CategoryName = r.Category.CategoryName,
                Username = r.User.Username
            }).ToList();
        }

        public double CalculateAverageRating(List<Rating> ratings)
        {
            if(ratings == null || ratings.Count == 0)
            {
                return 0.0;
            }

            double totalRating = ratings.Sum(r => r.RatingValue);
            return totalRating / ratings.Count;
        }
    }
}
