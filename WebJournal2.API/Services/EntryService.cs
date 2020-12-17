using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebJournal2.API.Core.Contexts;
using WebJournal2.API.Core.Models;

namespace WebJournal2.API.Services
{
	public class EntryService
	{
		private readonly AppDbContext db;

		public EntryService(AppDbContext db)
		{
			this.db = db;
		}

		public async Task<JournalEntry[]> GetEntriesAsync()
		{
			return await db.Entries.OrderBy(x => x.Id).ToArrayAsync();
		}

		public async Task<JournalEntry> GetEntryAsync(uint id)
		{
			return await db.Entries.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<JournalEntry> AddEntryAsync(JournalEntry entry)
		{
			var addedEntry = (await db.Entries.AddAsync(entry)).Entity;
			await db.SaveChangesAsync();
			return addedEntry;
		}

		public async Task<JournalEntry> UpdateEntryAsync(JournalEntry entry)
		{
			var updatedEntry = await GetEntryAsync(entry.Id);
			if (updatedEntry == null)
				return null;

			updatedEntry.Title = entry.Title;
			updatedEntry.Content = entry.Content;
			updatedEntry.SubmitDate = entry.SubmitDate;
			updatedEntry = db.Entries.Update(updatedEntry).Entity;
			await db.SaveChangesAsync();
			return updatedEntry;
		}

		public async Task<JournalEntry> DeleteEntryAsync(uint id)
		{
			var deletedEntry = await GetEntryAsync(id);
			if (deletedEntry == null)
				return null;
			db.Entries.Remove(deletedEntry);
			await db.SaveChangesAsync();
			return deletedEntry;
		}
	}
}
