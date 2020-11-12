using Microsoft.EntityFrameworkCore;
using WebJournal2.Core.Models;

namespace WebJournal2.Core.Contexts
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<JournalPassword> Passwords { get; set; }
		public DbSet<JournalEntry> Entries { get; set; }
	}
}
