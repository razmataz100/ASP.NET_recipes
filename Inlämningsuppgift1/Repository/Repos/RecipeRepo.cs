using Inlämningsuppgift1.Data.Contexts;
using Inlämningsuppgift1.Models.Entities;
using Inlämningsuppgift1.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inlämningsuppgift1.Repository.Repos
{
    public class RecipeRepo : IRecipeRepo
    {
        private readonly ApplicationDbContext _context;

        public RecipeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
        }

        public void RemoveRecipe(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _context.Entry(recipe).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Recipe GetRecipeById(int recipeId)
        {
            return _context.Recipes
                .Include(u => u.User)
                .Include(r => r.Ratings)
                .FirstOrDefault(r => r.RecipeId == recipeId);
        }

        public List<Recipe> SearchRecipesByTitle(string searchTerm)
        {
            return _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Category)
                .Include(r  => r.Ratings)
                .Where(r => r.Title.Contains(searchTerm))
                .ToList();
        }

        public List<Recipe> GetAllRecipes()
        {
            return _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Category)
                .Include(r => r.Ratings)
                .ToList();
        }
    }
}
