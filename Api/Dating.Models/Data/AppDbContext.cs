using Dating.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Models.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext()
		{

		}

		public DbSet<AppUser> Users { get; set; }
	}
}
