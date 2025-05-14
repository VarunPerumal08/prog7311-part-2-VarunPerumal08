using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.ViewModels;
using AgriEnergyConnect.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AgriEnergyConnect.Controllers
{
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FarmerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var farmer = _context.Farmers
                .Include(f => f.User)
                .Include(f => f.Products)
                .ThenInclude(p => p.Category)
                .FirstOrDefault(f => f.UserId == userId);

            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Categories = _context.ProductCategories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.ProductCategories.ToList();
                return View(model);
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId);
            if (farmer == null)
            {
                return NotFound();
            }

            var product = new Product
            {
                FarmerId = farmer.FarmerId,
                CategoryId = model.CategoryId,
                ProductName = model.ProductName,
                Description = model.Description,
                ProductionDate = model.ProductionDate,
                Quantity = model.Quantity,
                Price = model.Price,
                IsOrganic = model.IsOrganic,
                IsAvailable = true
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}