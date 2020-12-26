using System;
using System.Threading.Tasks;
using System.Linq;

namespace WebJournal2.Web.Core.Services
{
	public class JournalEntryService
	{
		private readonly ApiRequestService api;
		private JournalEntry[] entries;

		public JournalEntryService(ApiRequestService api)
		{
			this.api = api;
		}

		public async Task FetchEntries()
		{
			entries = await api.GetEntriesAsync();
		}

		public async Task<JournalEntry> GetEntryAsync(uint id)
		{
			if(entries == null || entries.Length == 0)
				await FetchEntries();
			return Array.Find(entries, e => e.Id == id);
		}

		public async Task<JournalEntry[]> GetEntriesAsync()
		{
			if(entries == null || entries.Length == 0)
				await FetchEntries();
			return entries.OrderByDescending(x => x.Id).ToArray();
		}

		public async Task AddEntryAsync(JournalEntry entry)
		{
			entry.Content = entry.ToFormattedForm();
			await api.PostEntryAsync(entry);
			await FetchEntries();
		}

		public async Task UpdateEntryAsync(JournalEntry entry)
		{
			entry.Content = entry.ToFormattedForm();
			await api.PutEntryAsync(entry);
			await FetchEntries();
		}

		public async Task DeleteEntryAsync(uint entryId)
		{
			await api.DeleteEntryAsync(entryId);
			await FetchEntries();
		}
	}
}
