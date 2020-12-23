using System;
using System.Text.Json.Serialization;
namespace WebJournal2.Web.Core
{
	public class JournalEntry
	{
		[JsonPropertyName("id")]
		public uint Id { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("content")]
		public string Content { get; set; }

		[JsonPropertyName("submitDate")]
		public DateTime SubmitDate { get; set; }

		public JournalEntry()
		{
			Title = string.Empty;
			Content = string.Empty;
			SubmitDate = DateTime.Now;
		 }

		public JournalEntry(uint id, string title, string content, DateTime date)
		{
			Id = id;
			Title = title;
			Content = content;
			SubmitDate = date;
		}

		public string[] GetParagraphs()
		{
			return Content.Split("<==>");
		}
	}
}
