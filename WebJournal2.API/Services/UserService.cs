using Microsoft.EntityFrameworkCore;
using System.Collections;
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
			return await db.Users.FirstAsync(x => x.Id == id);
		}

		public IEnumerable<JournalUser> GetUsers()
		{
			return db.Users.OrderBy(x => x.Id);
		}

		public async Task<JournalUser> AddUserAsync(JournalUser user)
		{
			JournalUser addedUser = (await db.Users.AddAsync(user)).Entity;
			await db.SaveChangesAsync();
			return addedUser;
		}

		public async Task<JournalUser> UpdateUser(JournalUser user)
		{
			JournalUser updatedUser = db.Update(user).Entity;
			await db.SaveChangesAsync();
			return updatedUser;
		}

		public async Task<JournalUser> DeleteUserAsync(JournalUser user)
		{
			JournalUser deletedUser = db.Users.Remove(user).Entity;
			await db.SaveChangesAsync();
			return deletedUser;
		}
	}
}
