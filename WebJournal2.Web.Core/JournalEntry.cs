using System;

namespace WebJournal2.Web.Core
{
	public class JournalEntry
	{
		public uint Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime Date { get; set; }

		public JournalEntry() { }

		public JournalEntry(uint id, string title, string content, DateTime date)
		{
			Id = id;
			Title = title;
			Content = content;
			Date = date;
		}

		public string[] GetParagraphs()
		{
			return Content.Split("<==>");
		}
	}
}
