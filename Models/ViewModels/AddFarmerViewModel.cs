using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models.ViewModels
{
    public class AddFarmerViewModel
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Farm Name")]
        [StringLength(100)]
        public string FarmName { get; set; }

        [Required]
        [Display(Name = "Farm Location")]
        [StringLength(100)]
        public string FarmLocation { get; set; }

        [Display(Name = "Farm Size (hectares)")]
        public decimal? FarmSize { get; set; }

        [Display(Name = "Specialization")]
        [StringLength(100)]
        public string? Specialization { get; set; }
    }
}