using System.ComponentModel.DataAnnotations;

namespace jbl_car_seller.Models
{
    public class Contact
    {
             public int Id { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
            public string Email { get; set; }

            [Required]
            [StringLength(500, ErrorMessage = "Message cannot be longer than 500 characters.")]
            public string Message { get; set; }
       
     }
}
