using BirdCageShopDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BirdCageShopUtils.UtilMethod
{
	//public static class JwtUtils
	//{
	//	public static string GenerateToken(this ApplicationUser applicationUser, int id,
	//		IConfiguration configuration, DateTime createdAt, int minuteValidFor, string role, string? secretKey = null)
	//	{

	//		if (user == null)
	//		{
	//			throw new ArgumentNullException(nameof(applicationUser), "User cannot be null.");
	//		}

	//		if (configuration == null)
	//		{
	//			throw new ArgumentNullException(nameof(configuration), "Configuration cannot be null.");
	//		}

	//		// Ensure that secretKey is not null and provide a default value if it is
	//		secretKey ??= configuration["Jwt:Key"];

	//		if (string.IsNullOrEmpty(secretKey))
	//		{
	//			throw new ArgumentException("Jwt secret key is null or empty.");
	//		}
	//		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? configuration["Jwt:Key"]));
	//		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
	//		var claims = new[]
	//		{
	//			new Claim("Id", applicationUser.Id.ToString()),
	//			new Claim("RoleId", id.ToString()),
	//		 new Claim(ClaimTypes.Role, role.ToString())
	//		};
	//		var token = new JwtSecurityToken(
	//			issuer: configuration["Jwt:Issuer"],
	//			audience: configuration["Jwt:Audience"],
	//			claims: claims,
	//			notBefore: createdAt,
	//			expires: createdAt.AddMinutes(minuteValidFor),
	//			signingCredentials: credentials);
	//		return new JwtSecurityTokenHandler().WriteToken(token);
	//	}
	//}
}
