using System;
using System.Linq;

namespace WebJournal2.Web.Core.Services
{
	public class JournalEntryService
	{
		private JournalEntry[] entries;

		public JournalEntryService()
		{
			entries = new JournalEntry[1]
			{
				new JournalEntry
				{
					Id = 1,
					Title = "Lorem ipsum dolor sit amet",
					Date = DateTime.Now,
					Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Magna fringilla urna porttitor rhoncus. Quam nulla porttitor massa id. Ante metus dictum at tempor commodo ullamcorper a lacus vestibulum. Sagittis purus sit amet volutpat. Etiam non quam lacus suspendisse faucibus interdum posuere lorem. Eget sit amet tellus cras adipiscing. Proin sed libero enim sed. Id aliquet lectus proin nibh nisl condimentum id. In mollis nunc sed id semper risus. Elit ullamcorper dignissim cras tincidunt."
				}
			};
		}

		public JournalEntry GetEntry(uint id)
		{
			return entries.FirstOrDefault(e => e.Id == id);
		}

		public JournalEntry[] GetEntries()
		{
			return entries;
		}
	}
}
