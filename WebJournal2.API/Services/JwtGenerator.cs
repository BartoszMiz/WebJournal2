using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebJournal2.API.Services
{
	public class JwtGenerator
	{
		public readonly string issuer;
		public readonly string key;

		public JwtGenerator(IConfiguration config)
		{
			issuer = config["Jwt:Issuer"];
			key = config["Jwt:Key"];
		}

		public string GenerateJSONWebToken()
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
