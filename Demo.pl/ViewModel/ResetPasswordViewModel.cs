using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(5, ErrorMessage = "Minimum Length Password Is 5")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password does Not Match Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
