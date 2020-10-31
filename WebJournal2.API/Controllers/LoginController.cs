using Microsoft.AspNetCore.Mvc;
using WebJournal2.Core.Models;
using WebJournal2.API.Services;
using System.Threading.Tasks;

namespace WebJournal2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly JwtService jwt;
		private readonly AuthenticationService auth;

		public LoginController(JwtService jwt, AuthenticationService auth)
		{
			this.jwt = jwt;
			this.auth = auth;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] string password)
		{
			var foundPassword = await auth.AuthenticateAsync(password).ConfigureAwait(false);
			if (foundPassword == null)
				return Unauthorized();

			string token = jwt.GenerateJSONWebToken();
			return Ok(new { token });
		}
	}
}
