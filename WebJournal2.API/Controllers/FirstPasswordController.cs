using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebJournal2.API.Services;

namespace WebJournal2.API.Controllers
{
	[Route("api/passwords/first")]
	[ApiController]
	public class FirstPasswordController : ControllerBase
	{
		private readonly PasswordService passwordService;
		private readonly PasswordHashingService passwordHashingService;

		public FirstPasswordController(PasswordService passwordService, PasswordHashingService passwordHashingService)
		{
			this.passwordService = passwordService;
			this.passwordHashingService = passwordHashingService;
		}

		// A route to add the first password without authorization
		[HttpPost]
		public async Task<IActionResult> AddFirstPassword([FromBody] string password)
		{
			uint passwordCount = await passwordService.GetPasswordCountAsync().ConfigureAwait(false);
			if (passwordCount > 0)
				return Unauthorized("A password has already been registered!");

			var hashedPassword = passwordHashingService.HashPassword(password);
			await passwordService.AddPasswordAsync(hashedPassword).ConfigureAwait(false);
			return Ok();
		}
	}
}
