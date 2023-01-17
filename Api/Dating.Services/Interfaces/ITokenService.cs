using Dating.Models.Entities;

namespace Dating.Services.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(AppUser user);
	}
}
