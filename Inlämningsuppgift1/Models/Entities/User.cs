
using System.ComponentModel.DataAnnotations;

namespace Inlämningsuppgift1.Models.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

    }
}
