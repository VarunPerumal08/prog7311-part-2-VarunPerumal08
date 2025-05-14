using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    public class Farmer
    {
        [Key]
        public int FarmerId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [StringLength(100)]
        public string FarmName { get; set; }

        [Required]
        [StringLength(100)]
        public string FarmLocation { get; set; }

        public decimal? FarmSize { get; set; }

        [StringLength(100)]
        public string? Specialization { get; set; }

        // Navigation property
        public ICollection<Product> Products { get; set; }
    }
}