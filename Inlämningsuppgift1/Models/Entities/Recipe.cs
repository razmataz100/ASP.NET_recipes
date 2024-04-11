using System.ComponentModel.DataAnnotations;

namespace Inlämningsuppgift1.Models.Entities
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Ingredients { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual User User { get; set; }
        public virtual RecipeCategory Category { get; set; }
        public virtual ICollection<Rating>? Ratings { get; set; }

        
    }
}