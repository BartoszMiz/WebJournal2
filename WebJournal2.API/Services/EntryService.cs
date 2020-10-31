﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebJournal2.Core.Contexts;
using WebJournal2.Core.Models;

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
			return await db.Entries.OrderBy(x => x.Id).ToArrayAsync().ConfigureAwait(false);
		}

		public async Task<JournalEntry> GetEntryAsync(uint id)
		{
			return await db.Entries.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
		}

		public async Task<JournalEntry> AddEntryAsync(JournalEntry entry)
		{
			var addedEntry = (await db.Entries.AddAsync(entry).ConfigureAwait(false)).Entity;
			await db.SaveChangesAsync().ConfigureAwait(false);
			return addedEntry;
		}

		public async Task<JournalEntry> UpdateEntryAsync(JournalEntry entry)
		{
			var updatedEntry = db.Entries.Update(entry).Entity;
			if (updatedEntry == null)
				return null;
			await db.SaveChangesAsync().ConfigureAwait(false);
			return updatedEntry;
		}

		public async Task<JournalEntry> DeleteEntryAsync(uint id)
		{
			var deletedEntry = await GetEntryAsync(id).ConfigureAwait(false);
			if (deletedEntry == null)
				return null;
			db.Entries.Remove(deletedEntry);
			await db.SaveChangesAsync().ConfigureAwait(false);
			return deletedEntry;
		}
	}
}
