using Microsoft.AspNetCore.Identity;

namespace jbl_car_seller.Models
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }
    }
}
