using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RootRPGAPI.Models
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Equipment>(b =>
			{
				b.Property(e => e.Id).UseIdentityColumn();
			});
		}

		public DbSet<Equipment> Equipment { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<WeaponSkill> WeaponSkills { get; set; }
	}
}
