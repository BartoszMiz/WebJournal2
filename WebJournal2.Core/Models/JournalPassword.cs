using System.ComponentModel.DataAnnotations;

namespace WebJournal2.Core.Models
{
	public class JournalPassword
	{
		[Key]
		public uint Id { get; set; }
		public string Password { get; set; }
	}
}
