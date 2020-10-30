using System.Security.Cryptography;
using System.Text;

namespace WebJournal2.API.Services
{
	public class PasswordHashingService
	{
		private readonly SHA512CryptoServiceProvider hasher;

		public PasswordHashingService()
		{
			hasher = new SHA512CryptoServiceProvider();
		}

		public string HashPassword(string password)
		{
			var passwordBytes = Encoding.UTF8.GetBytes(password);
			var hashedPasswordBytes = hasher.ComputeHash(passwordBytes);
			return Encoding.UTF8.GetString(hashedPasswordBytes);
		}
	}
}
