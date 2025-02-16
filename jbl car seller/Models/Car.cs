using System.ComponentModel.DataAnnotations;

namespace jbl_car_seller.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Make is required.")]
        public string Make { get; set; }
        public CarImage? CarImage { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; }

        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
        public int Year { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        public string Description { get; set; }

    }
}
