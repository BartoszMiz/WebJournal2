using System.ComponentModel.DataAnnotations;

namespace WebJournal2.API.Core.Models
{
	public class JournalPassword : DbEntity
	{
		public string Password { get; set; }
	}
}
