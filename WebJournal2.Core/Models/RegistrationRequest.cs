namespace WebJournal2.Core.Models
{
	public class RegistrationRequest
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public JournalUser ToJournalUser()
		{
			return new JournalUser { Username = Username, Password = Password, FirstName = FirstName, LastName = LastName };
		}
	}
}
