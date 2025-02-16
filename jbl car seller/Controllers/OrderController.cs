using jbl_car_seller.data;
using jbl_car_seller.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace jbl_car_seller.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            // Populate the Car and User dropdowns for selection
            var carList = _context.Cars
                                    .Select(car => new SelectListItem
                                    {
                                        Value = car.Id.ToString(),  // The Id of the Car will be the value in the dropdown
                                        Text = car.Make + "(" + car.Model + ")"             // The Name of the Car will be the display text
                                    }).ToList();


            // Inject the list into ViewBag
            ViewBag.CarList = carList;
            var carPrices = _context.Cars.ToDictionary(c => c.Id, c => c.Price);
            ViewData["CarPrices"] = carPrices;
            var UserList = _context.Users
                                 .Select(User => new SelectListItem
                                 {
                                     Value = User.Id.ToString(),  // The Id of the Car will be the value in the dropdown
                                     Text = User.FullName            // The Name of the Car will be the display text
                                 }).ToList();

            // Inject the list into ViewBag
            ViewBag.UserList = UserList;
            var payment = new List<string>();

            payment.Add("card");
            payment.Add("online");
            
            ViewBag.List = payment;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("OrderSummary", new { id = order.Id });
            }
            var carList = _context.Cars
                                 .Select(car => new SelectListItem
                                 {
                                     Value = car.Id.ToString(),
                                     Text = car.Make + "(" + car.Model + ")"
                                 }).ToList();

            ViewBag.CarList = carList;
           
            var UserList = _context.Users
                                 .Select(User => new SelectListItem
                                 {
                                     Value = User.Id.ToString(),
                                     Text = User.FullName
                                 }).ToList();

            ViewBag.UserList = UserList;
           
            return View(order);
        }

        public IActionResult OrderSummary(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }

}
