using jbl_car_seller.data;
using jbl_car_seller.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace jbl_car_seller.Controllers
{
    public class WishListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishListController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<WishList> Item = _context.WishLists.Include(c => c.Car);
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Item = Item.Where(b => b.UserId == id);
            var wish = await Item.ToListAsync();

            


            return View(wish); // Pass the current cart to the view
        }
        public async Task<IActionResult> AddToWishList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var existingWish = await _context.Cars
                           .FirstOrDefaultAsync(w => w.Id == id);
            if (existingWish == null)
            {
                return NotFound();
            }

            return View(existingWish);
        }

      

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWishList(int id,WishList wishList)
        {
            // Getting the logged-in user's ID dynamically
            string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var CarId = await _context.Cars.FirstOrDefaultAsync(b => b.Id == id);
            var existingWish = await _context.WishLists
                .FirstOrDefaultAsync(w => w.CarId == CarId.Id && w.UserId == userid);

            if (existingWish == null)
            {
                var wishList1 = new WishList
                {
                    CarId = CarId.Id,
                    UserId = userid
                };
                _context.Add(wishList1);
               
                await _context.SaveChangesAsync();
                
               
            }

            // Optionally, you can add a success message in TempData
            TempData["SuccessMessage"] = "Car added to wishlist successfully.";

            return RedirectToAction("Index", "Home");
        }
        
      


      

    
        public async Task<IActionResult> RemoveFromWishList(int id)
        {
            var wishListItem = await _context.WishLists.FindAsync(id);
            if (wishListItem != null)
            {
                _context.WishLists.Remove(wishListItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
