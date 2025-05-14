using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriEnergyConnect.Models
{
    public class ProductCategory
    {
        [Key] // Add this attribute
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public string? Description { get; set; }

        // Navigation property
        public ICollection<Product> Products { get; set; }
    }
}