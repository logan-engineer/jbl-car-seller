using System.ComponentModel.DataAnnotations;

namespace jbl_car_seller.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }
        public Car Car { get; set; }
        [Required]
        public string UserId { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        [StringLength(500, ErrorMessage = "Comment can't exceed 500 characters.")]
        public string Comment { get; set; }
    }

}
