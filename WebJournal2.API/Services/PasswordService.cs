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
			return await db.Passwords.ToArrayAsync().ConfigureAwait(false);
		}

		public async Task<uint> GetPasswordCountAsync()
		{
			return (uint)(await GetPasswordsAsync().ConfigureAwait(false)).Length;
		}

		public async Task<JournalPassword> GetPasswordAsync(uint id)
		{
			return await db.Passwords.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
		}

		public async Task<JournalPassword> GetPasswordAsync(string password)
		{
			return await db.Passwords.FirstOrDefaultAsync(x => x.Password == password).ConfigureAwait(false);
		}

		public async Task<JournalPassword> AddPasswordAsync(JournalPassword password)
		{
			var addedPassword = (await db.Passwords.AddAsync(password).ConfigureAwait(false)).Entity;
			await db.SaveChangesAsync().ConfigureAwait(false);
			return addedPassword;
		}

		public async Task<JournalPassword> AddPasswordAsync(string password)
		{
			var journalPassword = new JournalPassword { Password = password };
			return await AddPasswordAsync(journalPassword).ConfigureAwait(false);
		}
	}
}
