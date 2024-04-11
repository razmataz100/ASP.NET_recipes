using Inlämningsuppgift1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inlämningsuppgift1.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<RecipeCategory> RecipeCategories { get; set; }

    }
}

