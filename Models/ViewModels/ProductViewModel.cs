using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [Display(Name = "Product Name")]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Display(Name = "Description")]
        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Production Date")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;

        [Display(Name = "Price")]
        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        [Display(Name = "Organic")]
        public bool IsOrganic { get; set; }
    }
}