using System;

namespace WebJournal2.Core.Models
{
	public class JournalEntry : DbEntity
	{
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime SubmitDate { get; set; }
	}
}
