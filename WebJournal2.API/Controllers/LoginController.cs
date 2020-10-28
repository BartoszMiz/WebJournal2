using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WebJournal2.API.Models;
using WebJournal2.API.Services;

namespace WebJournal2.API.Controllers
{
	[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly JwtGenerator jwt;

		public LoginController(JwtGenerator jwt)
		{
			this.jwt = jwt;
		}

		[HttpPost]
		public IActionResult Login([FromBody]UserCredentials userCredentials)
		{
			if (userCredentials.Username == "test" && userCredentials.Password == "test")
			{
				string token = jwt.GenerateJSONWebToken();
				return Ok(new { token });
			}
			else
				return Unauthorized();
		}
	}
}
