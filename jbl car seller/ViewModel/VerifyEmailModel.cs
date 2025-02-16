using System.ComponentModel.DataAnnotations;

namespace jbl_car_seller.ViewModel
{
    public class VerifyEmailModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
