using System.ComponentModel.DataAnnotations;

namespace Dating.Models.ViewModels
{
	public class RegisterDto
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
