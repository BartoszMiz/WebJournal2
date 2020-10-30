using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebJournal2.Core.Contexts;
using WebJournal2.Core.Models;

namespace WebJournal2.API.Services
{
	public class UserService
	{
		private readonly AppDbContext db;

		public UserService(AppDbContext db)
		{
			this.db = db;
		}

		public async Task<JournalUser> GetUserAsync(uint id)
		{
			return await db.Users.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
		}

		public JournalUser[] GetUsers()
		{
			return db.Users.OrderBy(x => x.Id).ToArray();
		}

		public async Task<JournalUser> AddUserAsync(JournalUser user)
		{
			JournalUser addedUser = (await db.Users.AddAsync(user).ConfigureAwait(false)).Entity;
			await db.SaveChangesAsync().ConfigureAwait(false);
			return addedUser;
		}

		public async Task<JournalUser> UpdateUserAsync(uint id, JournalUser newUser)
		{
			var user = await GetUserAsync(id).ConfigureAwait(false);
			if (user == null)
				return null;

			db.Users.Update(newUser);
			await db.SaveChangesAsync().ConfigureAwait(false);
			return await GetUserAsync(id).ConfigureAwait(false);
		}

		public async Task<JournalUser> DeleteUserAsync(uint id)
		{
			var deletedUser = await GetUserAsync(id).ConfigureAwait(false);

			if (deletedUser == null)
				return null;

			db.Users.Remove(deletedUser);
			await db.SaveChangesAsync().ConfigureAwait(false);
			return deletedUser;
		}
	}
}
