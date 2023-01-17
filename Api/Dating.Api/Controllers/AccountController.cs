using Dating.Models.Data;
using Dating.Models.Entities;
using Dating.Models.ViewModels;
using Dating.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Dating.Api.Controllers
{
	public class AccountController : BaseController
	{
		private readonly AppDbContext context;
		private readonly ITokenService tokenService;

		public AccountController(AppDbContext _context, ITokenService _tokenService)
		{
			context = _context;
			tokenService = _tokenService;
		}

		[HttpPost("register")] // POST : api/account/register
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{
			if (await UserExists(registerDto.UserName)) return BadRequest("UserName is Taken");

			using var hmac = new HMACSHA512();

			var user = new AppUser
			{
				Name = registerDto.UserName.ToLower(),
				Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
			};

			context.Users.Add(user);
			await context.SaveChangesAsync();
			return new UserDto
			{
				Username = user.Name,
				Token = tokenService.CreateToken(user)
			};

		}
		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await context.Users.SingleOrDefaultAsync(x => x.Name == loginDto.UserName);

			if (user == null) return Unauthorized("Invalid User!");

			return new UserDto
			{
				Username = user.Name,
				Token = tokenService.CreateToken(user)
			};
		}
		private async Task<bool> UserExists(string userName)
		{
			return await context.Users.AnyAsync(x => x.Name == userName.ToLower());
		}
	}
}
