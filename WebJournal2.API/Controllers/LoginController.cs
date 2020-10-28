using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebJournal2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private IConfiguration config;

		public LoginController(IConfiguration config)
		{
			this.config = config;
		}

		[HttpPost]
		public IActionResult Login(string login, string password)
		{
			if (login == "test" && password == "test")
				return Ok("You are now authenticated!");
			else
				return Unauthorized("nope!");
		}
	}
}
