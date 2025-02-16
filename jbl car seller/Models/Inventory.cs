using System.ComponentModel.DataAnnotations;

namespace jbl_car_seller.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        public Car? Car { get; set; }
     


        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }

}
