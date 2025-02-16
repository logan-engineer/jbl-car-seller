using System.ComponentModel.DataAnnotations;

namespace jbl_car_seller.Models
{
    public class WishList
    {
       
        public int Id { get; set; }

        [Required]
        
        public string UserId { get; set; }

        [Required]
        
        public int CarId { get; set; }

        public User? User { get; set; }
        public Car? Car { get; set; }
        
    }

}
