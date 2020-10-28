using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebJournal2.API.Models
{
	public class UserCredentials
	{
		[JsonIgnore] [Key]
		public uint Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
