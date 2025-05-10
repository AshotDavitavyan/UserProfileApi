using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using UserProfileApi.Models;

namespace UserProfileApi.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public AppDbContext() { }
		public DbSet<UserProfile> UserProfiles { get; set; }
		public DbSet<ProfileField> ProfileFields { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserProfile>()
				.Property(up => up.ExpertiseAreas)
				.HasConversion(
					v => string.Join(',', v),
					v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
				);
			modelBuilder.Entity<UserProfile>()
				.Property(up => up.ExtraFields)
				.HasConversion(
					v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
					v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions?)null) ?? new Dictionary<string, string>()
				);
		}
	}
}
