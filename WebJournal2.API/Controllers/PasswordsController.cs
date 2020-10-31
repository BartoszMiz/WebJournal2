using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebJournal2.API.Services;

namespace WebJournal2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PasswordsController : ControllerBase
	{
		private readonly PasswordService passwordService;
		private readonly PasswordHashingService passwordHashingService;

		public PasswordsController(PasswordService passwordService, PasswordHashingService passwordHashingService)
		{
			this.passwordService = passwordService;
			this.passwordHashingService = passwordHashingService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			uint passwordCount = await passwordService.GetPasswordCountAsync().ConfigureAwait(false);
			return Ok(passwordCount);
		}

		[Authorize] [HttpPost]
		public async Task<IActionResult> Post([FromBody] string password)
		{
			var hashedPassword = passwordHashingService.HashPassword(password);
			if (await passwordService.GetPasswordAsync(hashedPassword).ConfigureAwait(false) != null)
				return BadRequest("This password has been already registered!");

			await passwordService.AddPasswordAsync(hashedPassword).ConfigureAwait(false);
			return Ok();
		}
	}
}
