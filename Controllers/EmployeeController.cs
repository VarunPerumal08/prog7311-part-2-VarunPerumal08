using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.ViewModels;
using AgriEnergyConnect.Data;
using Microsoft.EntityFrameworkCore;


/**Code Attribute
    /* https://stackoverflow.com/questions/1015813/what-goes-into-the-controller-in-mvc
    /*Author: victor hugo*/

namespace AgriEnergyConnect.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var farmers = _context.Farmers
                .Include(f => f.User)
                .Include(f => f.Products)
                .ToList();

            return View(farmers);
        }

        [HttpGet]
        public IActionResult AddFarmer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFarmer(AddFarmerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Create user first
            var user = new User
            {
                Username = model.Username,
                Password = HashPassword(model.Password), 
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                RoleId = 1 // Farmer role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Then create farmer
            var farmer = new Farmer
            {
                UserId = user.UserId,
                FarmName = model.FarmName,
                FarmLocation = model.FarmLocation,
                FarmSize = model.FarmSize,
                Specialization = model.Specialization
            };

            _context.Farmers.Add(farmer);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ViewProducts(int farmerId, string searchTerm = "", string categoryFilter = "", DateTime? fromDate = null, DateTime? toDate = null)
        {
            var farmer = _context.Farmers
                .Include(f => f.User)
                .FirstOrDefault(f => f.FarmerId == farmerId);

            if (farmer == null)
            {
                return NotFound();
            }

            ViewBag.Farmer = farmer;
            ViewBag.Categories = _context.ProductCategories.ToList();

            var productsQuery = _context.Products
                .Include(p => p.Category)
                .Where(p => p.FarmerId == farmerId);

            // Apply filters
            if (!string.IsNullOrEmpty(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.ProductName.Contains(searchTerm) ||
                    p.Description.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(categoryFilter))
            {
                productsQuery = productsQuery.Where(p => p.Category.CategoryName == categoryFilter);
            }

            if (fromDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate <= toDate.Value);
            }

            var products = productsQuery.ToList();

            return View(products);
        }

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}