using System.ComponentModel.DataAnnotations;

namespace Inlämningsuppgift1.Models.Entities
{
    public class RecipeCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

    }
}
