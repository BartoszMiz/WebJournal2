using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebJournal2.Core.Models
{
	public class JournalUser
	{
		[Key]
		public uint Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[JsonIgnore]
		public UserCredentials Credentials { get; set; }
	}
}
