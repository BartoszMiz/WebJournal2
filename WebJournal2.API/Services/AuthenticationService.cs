using WebJournal2.Core.Models;

namespace WebJournal2.API.Services
{
	public class AuthenticationService
	{
		public JournalUser Authenticate(UserCredentials credentials)
		{
			if (credentials.Username == "admin" && credentials.Password == "admin")
				return new JournalUser { Id = 0, FirstName = "admin", LastName = string.Empty, Credentials = credentials };
			if (credentials.Username == "test" && credentials.Password == "test")
				return new JournalUser { Id = 1, FirstName = "Test", LastName = "User", Credentials = credentials };

			return null;
		}
	}
}
