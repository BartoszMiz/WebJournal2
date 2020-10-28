using System.Linq;
using WebJournal2.Core.Models;

namespace WebJournal2.API.Services
{
	public class AuthenticationService
	{
		private readonly UserService userService;

		public AuthenticationService(UserService userService)
		{
			this.userService = userService;
		}

		public JournalUser Authenticate(UserCredentials credentials)
		{
			return userService.GetUsers()
				.FirstOrDefault(x => x.Username == credentials.Username && x.Password == credentials.Password);
		}
	}
}
