using System.ComponentModel.DataAnnotations;

namespace jbl_car_seller.Models
{
    public class CarImage
    {
       
            public int CarImageId { get; set; }
            public int CarId { get; set; }
            public string ImageUrl { get; set; }
            public Car? Car { get; set; }

       
    }
}
