using System.Diagnostics;
using jbl_car_seller.data;
using jbl_car_seller.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jbl_car_seller.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string search, decimal? minPrice, decimal? maxPrice)
        {
            // Start with the base query to get all members
            IQueryable<Car> cars = _context.Cars.Include(c=>c.CarImage);
                

            // Apply search filter if search term is provided
            if (!string.IsNullOrEmpty(search))
            {
                cars = cars.Where(m => m.Make.Contains(search));

            }


            // Apply price range filter if minPrice and maxPrice are provided
            if (minPrice.HasValue)
            {
                // Assuming Price is a property of BookLoan or Book
                cars = cars.Where(bl => bl.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                cars = cars.Where(bl => bl.Price <= maxPrice.Value);
            }

            // Execute the query and return the results
            var car = await cars.ToListAsync();
            
            return View(car);
        }
        public async Task<IActionResult> Details(int id)
        {

            var car = await _context.Cars
                .Include(c => c.CarImage)  // Include CarImage if necessary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }
           
            return View(car);
            }
            [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
