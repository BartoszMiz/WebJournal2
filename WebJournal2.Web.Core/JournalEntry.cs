using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace WebJournal2.Web.Core
{
	public class JournalEntry
	{
		[JsonPropertyName("id")]
		public uint? Id { get; set; }

		[JsonPropertyName("title")]
		[Required(ErrorMessage = "Title is required")]
		[MaxLength(60, ErrorMessage = "Title cannot be longer than 50 chracters!")]
		public string Title { get; set; }

		[JsonPropertyName("content")]
		[Required(ErrorMessage = "Entry must contain text!")]
		public string Content { get; set; }

		[JsonPropertyName("submitDate")]
		public DateTime SubmitDate { get; set; }

		public JournalEntry()
		{
			Id = null;
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
			return Content.Split("<>");
		}

		public string ToFormattedForm()
		{
			string processedContent = Content;
			processedContent = processedContent.Replace("\n\n", "<>");
			return processedContent;
		}

		public string ToHumanReadableForm()
		{
			string processedContent = Content;
			processedContent = processedContent.Replace("<>", "\n\n");
			return processedContent;
		}
	}
}
