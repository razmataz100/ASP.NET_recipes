using Inlämningsuppgift1.Models.Entities;

namespace Inlämningsuppgift1.Repository.Interfaces
{
    public interface ICategoryRepo
    {
        RecipeCategory GetCategory (string categoryName);
    }
}
