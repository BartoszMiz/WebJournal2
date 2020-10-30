using Microsoft.AspNetCore.Mvc;
using WebJournal2.Core.Models;
using WebJournal2.API.Services;

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
		public IActionResult Login([FromBody] Credentials credentials)
		{
			JournalUser user = auth.Authenticate(credentials);
			if (user == null)
				return Unauthorized();

			string token = jwt.GenerateJSONWebToken();
			return Ok(new { token , userId = user.Id});
		}
	}
}
