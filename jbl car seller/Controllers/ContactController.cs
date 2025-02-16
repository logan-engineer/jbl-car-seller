using jbl_car_seller.data;
using jbl_car_seller.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jbl_car_seller.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Contact model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                // Logic to process the form (e.g., send an email)
                TempData["Success"] = "Thank you for contacting us!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
