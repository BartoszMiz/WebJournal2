using Microsoft.EntityFrameworkCore;
using WebJournal2.API.Models;

namespace WebJournal2.API.Contexts
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<JournalUser> Users { get; set; }
	}
}
