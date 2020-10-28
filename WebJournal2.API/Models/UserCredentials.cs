using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebJournal2.API.Models
{
	public class UserCredentials
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
