using HealthcareAppointment.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareAppointment.Business.Services.GenerateToken
{
	public static class TokenService
	{
		public static JwtSecurityToken GenerateToken(this List<Claim> claims, IConfiguration configuration)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			var token = new JwtSecurityToken(
				//issuer: configuration["Jwt:Issuer"],
				//audience: configuration["Jwt:Audience"],
				expires: DateTime.Now.AddMinutes(30),
				claims: claims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
			);
			return token;
		}
	}
}
