namespace AgriEnergyConnect.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        // Navigation property
        public ICollection<User> Users { get; set; }
    }
}