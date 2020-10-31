using System.Threading.Tasks;
using WebJournal2.Core.Models;

namespace WebJournal2.API.Services
{
	public class AuthenticationService
	{
		private readonly PasswordService passwordService;
		private readonly PasswordHashingService passwordHashingService;

		public AuthenticationService(PasswordService passwordService, PasswordHashingService passwordHashingService)
		{
			this.passwordService = passwordService;
			this.passwordHashingService = passwordHashingService;
		}

		public async Task<JournalPassword> AuthenticateAsync(string password)
		{
			var hashedPassword = passwordHashingService.HashPassword(password);
			return await passwordService.GetPasswordAsync(hashedPassword).ConfigureAwait(false);
		}
	}
}
