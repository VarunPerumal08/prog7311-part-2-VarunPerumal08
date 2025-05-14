using Microsoft.EntityFrameworkCore;
using AgriEnergyConnect.Models;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;

namespace AgriEnergyConnect.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Farmer" },
                new Role { RoleId = 2, RoleName = "Employee" }
            );

            // Seed categories
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { CategoryId = 1, CategoryName = "Vegetables", Description = "Fresh farm vegetables" },
                new ProductCategory { CategoryId = 2, CategoryName = "Fruits", Description = "Seasonal fruits" },
                new ProductCategory { CategoryId = 3, CategoryName = "Grains", Description = "Cereals and grains" },
                new ProductCategory { CategoryId = 4, CategoryName = "Dairy", Description = "Milk and milk products" },
                new ProductCategory { CategoryId = 5, CategoryName = "Meat", Description = "Farm meat products" }
            );
        }
    }
}