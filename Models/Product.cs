using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public int FarmerId { get; set; }
        public Farmer Farmer { get; set; }

        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        public bool IsOrganic { get; set; } = false;
        public bool IsAvailable { get; set; } = true;
    }
}