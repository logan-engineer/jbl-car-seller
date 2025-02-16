using jbl_car_seller.data;
using Microsoft.AspNetCore.Mvc;

namespace jbl_car_seller.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]

        public IActionResult Index()
        {
            var user = _context.Users.ToList();
            return View(user);
        }
    }
}
