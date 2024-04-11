using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inlämningsuppgift1.Models.Entities
{
    public class Rating
    {
        [Key]
        public int RatingID { get; set; }

        [Range(1, 5)]
        public int RatingValue { get; set; }

        public virtual Recipe Recipe { get; set; }

        public virtual User User { get; set; }
    }
}
