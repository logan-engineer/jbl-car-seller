using jbl_car_seller.data;
using jbl_car_seller.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace jbl_car_seller.Controllers
{
    public class CarImageController : Controller
    {
       
            private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CarImageController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
            {
                _context = context;
            _webHostEnvironment = webHostEnvironment;
            }

        // Admin views all categories
        public async Task<IActionResult> Index()
        {
            var carimage = await _context.CarImage
                .Include(b => b.Car)
                .ToListAsync();
            return View(carimage);
            }

            // Admin creates a new category
            [HttpGet]
            public IActionResult Create()
            {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Make");
            return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(CarImage carImage)
            {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                string newfilename = Guid.NewGuid().ToString();
                var upload = Path.Combine(webRootPath, @"images\car");
                var extension = Path.GetExtension(file[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(upload, newfilename + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }
                carImage.ImageUrl = @"\images\car\" + newfilename + extension;
            }
            if (ModelState.IsValid)
                {
                    _context.CarImage.Add(carImage);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
               
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Make", carImage.CarId);
            return View(carImage);
        }

            // Admin edits an existing category
            [HttpGet]
            public IActionResult Edit(int id)
            {
                var category = _context.CarImage.Find(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, CarImage carImage)
            {
                if (id != carImage.CarImageId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _context.Update(carImage);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(carImage);
            }

            // Admin deletes a category
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Delete(int id)
            {
                var carimage = await _context.CarImage.FindAsync(id);
                if (carimage != null)
                {
                    _context.CarImage.Remove(carimage);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
        
    }
}
