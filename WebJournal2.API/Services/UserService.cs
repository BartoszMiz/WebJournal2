using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
			return await db.Users.FirstOrDefaultAsync(x => x.Id == id);
		}

		public JournalUser[] GetUsers()
		{
			return db.Users.OrderBy(x => x.Id).ToArray();
		}

		public async Task<JournalUser> AddUserAsync(JournalUser user)
		{
			JournalUser addedUser = (await db.Users.AddAsync(user)).Entity;
			await db.SaveChangesAsync();
			return addedUser;
		}

		public async Task<JournalUser> UpdateUserAsync(uint id, JournalUser newUser)
		{
			var user = await GetUserAsync(id);
			if (user == null)
				return null;

			db.Users.Update(newUser);
			await db.SaveChangesAsync();
			return await GetUserAsync(id);
		}

		public async Task<JournalUser> DeleteUserAsync(uint id)
		{
			var deletedUser = await GetUserAsync(id);

			if (deletedUser == null)
				return null;

			db.Users.Remove(deletedUser);
			await db.SaveChangesAsync();
			return deletedUser;
		}
	}
}
