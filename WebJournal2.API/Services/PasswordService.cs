using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebJournal2.API.Core.Contexts;
using WebJournal2.API.Core.Models;

namespace WebJournal2.API.Services
{
	public class PasswordService
	{
		private readonly AppDbContext db;

		public PasswordService(AppDbContext db)
		{
			this.db = db;
		}

		public async Task<JournalPassword[]> GetPasswordsAsync()
		{
			return await db.Passwords.ToArrayAsync();
		}

		public async Task<uint> GetPasswordCountAsync()
		{
			return (uint)(await GetPasswordsAsync()).Length;
		}

		public async Task<JournalPassword> GetPasswordAsync(uint id)
		{
			return await db.Passwords.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<JournalPassword> GetPasswordAsync(string password)
		{
			return await db.Passwords.FirstOrDefaultAsync(x => x.Password == password);
		}

		public async Task<JournalPassword> AddPasswordAsync(JournalPassword password)
		{
			var addedPassword = (await db.Passwords.AddAsync(password)).Entity;
			await db.SaveChangesAsync();
			return addedPassword;
		}

		public async Task<JournalPassword> AddPasswordAsync(string password)
		{
			var journalPassword = new JournalPassword { Password = password };
			return await AddPasswordAsync(journalPassword);
		}
	}
}
