using System;
using WebJournal2.Core.Models;

namespace WebJournal2.API.Services
{
	public class AuthenticationService
	{
		private readonly UserService userService;
		private readonly PasswordHashingService passwordHashingService;

		public AuthenticationService(UserService userService, PasswordHashingService passwordHashingService)
		{
			this.userService = userService;
			this.passwordHashingService = passwordHashingService;
		}

		public JournalUser Authenticate(Credentials credentials)
		{
			var hashedPassword = passwordHashingService.HashPassword(credentials.Password);
			return Array.Find(userService.GetUsers(), x => x.Username == credentials.Username && x.Password == hashedPassword);
		}
	}
}
