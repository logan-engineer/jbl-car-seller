using jbl_car_seller.data;
using jbl_car_seller.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace jbl_car_seller.Controllerss
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // View all inventory
        public IActionResult Index()
        {
            var inventory = _context.Inventories.Include(i => i.Car).ToList();
            return View(inventory);
        }
        // Admin creates a new category
        [HttpGet]
        public IActionResult Create()
        {
            // Create a list of SelectListItems for Car dropdown
            var carList = _context.Cars
                                  .Select(car => new SelectListItem
                                  {
                                      Value = car.Id.ToString(),  // The Id of the Car will be the value in the dropdown
                                      Text = car.Make+"("+car.Model+")"             // The Name of the Car will be the display text
                                  }).ToList();

            // Inject the list into ViewBag
            ViewBag.CarList = carList;

            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect back to Index after successful creation
            }

            // Repopulate the list in case of validation error
            var carList = _context.Cars
                                  .Select(car => new SelectListItem
                                  {
                                      Value = car.Id.ToString(),
                                      Text = car.Make + "(" + car.Model + ")"
                                  }).ToList();

            ViewBag.CarList = carList;
            return View(inventory);
        }

        // Admin updates the stock
        [HttpGet]
        public IActionResult UpdateStock(int id)
        {
            var inventory = _context.Inventories.Include(i => i.Car).FirstOrDefault(i => i.CarId == id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStock(int id,Inventory inventory)
        {

            
            if (ModelState.IsValid)
            {
                _context.Update(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }
    }

}
