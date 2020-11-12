using System.ComponentModel.DataAnnotations;

namespace WebJournal2.Core.Models
{
	public class JournalPassword : DbEntity
	{
		public string Password { get; set; }
	}
}
