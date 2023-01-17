using Dating.Models.Data;
using Dating.Services.Implements;
using Dating.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Dating.Api.Extensions
{
	public static class AppServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlite(config.GetConnectionString("DefaultCon"));
			});

			services.AddCors();
			services.AddScoped<ITokenService, TokenService>();
			return services;
		}
	}
}
