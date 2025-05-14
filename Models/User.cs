using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AgriEnergyConnect.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        // Navigation property
        public Farmer? Farmer { get; set; }
    }
}