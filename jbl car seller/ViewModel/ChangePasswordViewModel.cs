using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace jbl_car_seller.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
       
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [Display(Name = "New Password")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} AspNetCoreGeneratedDocument at max {1} characterlong ")]
        [DataType(DataType.Password)]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password does not Match")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
