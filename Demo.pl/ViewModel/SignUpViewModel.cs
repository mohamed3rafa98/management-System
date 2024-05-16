using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModel
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="Email Is Required")]
		[EmailAddress(ErrorMessage ="Email Is Invalid")]
        public string Email { get; set; }

		[Required(ErrorMessage ="First Name Is Required")]
		public string FirstName { get; set; }	
		
		[Required(ErrorMessage ="Last Name Is Required")]
		public string LastName { get; set; }

        public string UserName { get; set; }

		[Required(ErrorMessage ="Password Is Required")]
		[MinLength( 5,ErrorMessage ="Minimum Length Password Is 5")]
		[DataType(DataType.Password)]
        public string Password { get; set; }	
		
		[Required(ErrorMessage ="Confirm Password Is Required")]
		[Compare(nameof(Password),ErrorMessage ="Confirm Password does Not Match Password")]
		[DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }


    }
}
