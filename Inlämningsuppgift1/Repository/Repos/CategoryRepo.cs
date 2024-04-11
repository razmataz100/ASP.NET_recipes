using Inlämningsuppgift1.Data.Contexts;
using Inlämningsuppgift1.Models.Entities;
using Inlämningsuppgift1.Repository.Interfaces;

namespace Inlämningsuppgift1.Repository.Repos
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext _DBcontext;

        public CategoryRepo(ApplicationDbContext dbContext)
        {
            _DBcontext = dbContext;
        }
        public RecipeCategory GetCategory(string categoryName)
        {
            return _DBcontext.RecipeCategories.FirstOrDefault(c => c.CategoryName == categoryName);
        }
    }
}
