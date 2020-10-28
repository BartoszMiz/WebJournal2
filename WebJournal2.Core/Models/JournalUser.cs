using System.Collections.Generic;
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

		public string Username { get; set; }
		public string Password { get; set; }

		public JournalUser StripLoginData()
		{
			return new JournalUser { Id = Id, FirstName = FirstName, LastName = LastName };
		}

		public static JournalUser[] StripLoginData(JournalUser[] users)
		{
			for (int i = 0; i < users.Length; i++)
			{
				users[i] = users[i].StripLoginData();
			}

			return users;
		}
	}
}
