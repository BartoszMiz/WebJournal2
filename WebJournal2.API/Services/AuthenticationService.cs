using System;
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

		public JournalUser Authenticate(Credentials credentials)
		{
			return Array.Find(userService.GetUsers(), x => x.Username == credentials.Username && x.Password == credentials.Password);
		}
	}
}
