using System.ComponentModel.DataAnnotations;

namespace jbl_car_seller.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }
        public Car Car { get; set; }

        [Required]
        public string UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public decimal Amount { get; set; }

        public string PaymentStatus { get; set; }  // e.g., Pending, Completed
    }

}
