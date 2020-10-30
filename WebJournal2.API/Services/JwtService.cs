using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebJournal2.API.Services
{
	public class JwtService
	{
		public readonly string Issuer;
		public readonly string Key;
		public readonly TimeSpan defaultTokenLifespan;
		private readonly JwtSecurityTokenHandler handler;

		public JwtService(IConfiguration config)
		{
			Issuer = config["Jwt:Issuer"];
			Key = config["Jwt:Key"];
			defaultTokenLifespan = new TimeSpan(hours: 0, minutes: 20, seconds: 0);
			handler = new JwtSecurityTokenHandler();
		}

		public string GenerateJSONWebToken(TimeSpan tokenLifeSpan)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				issuer: Issuer,
				audience: Issuer,
				claims: null,
				expires: DateTime.Now.Add(tokenLifeSpan),
				signingCredentials: credentials);
			return handler.WriteToken(token);
		}

		public string GenerateJSONWebToken()
		{
			return GenerateJSONWebToken(defaultTokenLifespan);
		}
	}
}
