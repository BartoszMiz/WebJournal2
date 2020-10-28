using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebJournal2.Core.Models
{
	public class UserCredentials
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
