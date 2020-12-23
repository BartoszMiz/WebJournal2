using System;
using System.Threading.Tasks;

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

		public async Task<JournalEntry> GetEntry(uint id)
		{
			if(entries == null || entries.Length == 0)
				await FetchEntries();
			return Array.Find(entries, e => e.Id == id);
		}

		public async Task<JournalEntry[]> GetEntries()
		{
			if(entries == null || entries.Length == 0)
				await FetchEntries();
			return entries;
		}
	}
}
