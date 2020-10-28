using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebJournal2.API.Models;

namespace WebJournal2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private IConfiguration config;
		private readonly string key;
		private readonly string issuer;

		public LoginController(IConfiguration config)
		{
			this.config = config;
			this.key = config["Jwt:Key"];
			this.issuer = config["Jwt:Issuer"];
		}

		[HttpPost]
		public IActionResult Login([FromBody]UserCredentials userCredentials)
		{
			if (userCredentials.Username == "test" && userCredentials.Password == "test")
			{
				string token = GenerateJSONWebToken();
				return Ok(new { token = token });
			}
			else
				return Unauthorized();
		}

		private string GenerateJSONWebToken()
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(issuer: issuer, audience: issuer, claims: null, expires: DateTime.Now.AddSeconds(30), signingCredentials: credentials);
			var handler = new JwtSecurityTokenHandler();
			var tokenString = handler.WriteToken(token);
			return tokenString;
		}
	}
}
