using jbl_car_seller.data;
using jbl_car_seller.Models;
using Microsoft.AspNetCore.Mvc;

namespace jbl_car_seller.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Admin dashboard overview
        [HttpGet]
        public IActionResult Index()
        {
            var carCount = _context.Cars.Count();
            var orderCount = _context.Orders.Count();
            var reviewCount = _context.Reviews.Count();
            var categoryCount = _context.Categories.Count();

            var model = new DashboardViewModel
            {
                CarCount = carCount,
                OrderCount = orderCount,
                ReviewCount = reviewCount,
                CategoryCount = categoryCount
            };

            return View(model);
        }
    }

}
