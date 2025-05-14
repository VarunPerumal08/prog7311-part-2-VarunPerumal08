using Microsoft.AspNetCore.Mvc;


/**Code Attribute
    /* https://stackoverflow.com/questions/1015813/what-goes-into-the-controller-in-mvc
    /*Author: victor hugo*/

namespace AgriEnergyConnect.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}