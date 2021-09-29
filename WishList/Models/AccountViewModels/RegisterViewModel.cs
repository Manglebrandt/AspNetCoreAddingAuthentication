using System.ComponentModel.DataAnnotations;

namespace WishList.Models.AccountViewModels
{
	public class RegisterViewModel
	{
		public string Email { get; set; }

		[Required,MinLength(100)]
		public string Password { get; set; }

		[Required, DataType(DataType.Password), Compare("Password")]
		public string ConfirmPassword { get; set; }
	}
}
