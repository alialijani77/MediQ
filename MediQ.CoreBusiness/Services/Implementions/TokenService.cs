using MediQ.CoreBusiness.Services.Interfaces;
using MediQ.CoreBusiness.Settings;
using MediQ.Domain.Entities.UserManagement;
using MediQ.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediQ.CoreBusiness.Services.Implementions
{
	public class TokenService : ITokenService
	{
		private readonly IUserRepository _userRepository;
		private readonly SymmetricSecurityKey _key;
		private readonly string? _issuer;
		private readonly string? _audience;
		private readonly double _expires;

		public TokenService(IUserRepository userRepository, IConfiguration configuration)
		{
			_userRepository = userRepository;
			var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
			if (jwtSettings is null || string.IsNullOrEmpty(jwtSettings.Key))
			{
				throw new InvalidOperationException("JWT security key is not configured.");
			}

			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
			_issuer = jwtSettings.Issuer;
			_audience = jwtSettings.Audience;
			_expires = jwtSettings.ExpirationInMinutes;
		}

		public async Task<string> GenerateToken(IdentityUser user)
		{
			var signingCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
			var claims = await GetClaimsAsync(user);
			var jwtSecurityToken = GenerateTokenOptions(claims, signingCredentials);
			return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
		}

		private async Task<List<Claim>> GetClaimsAsync(IdentityUser user)
		{
			var userClaims = await _userRepository.GetClaimsAsync(user);
			var userRoles = await _userRepository.GetRolesAsync(user);

			var roleClaims = new List<Claim>();
			foreach (var role in userRoles)
			{
				roleClaims.Add(new Claim(ClaimTypes.Role, role));
			}

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Email, user.Email)
			}
			.Union(userClaims)
			.Union(roleClaims);

			return claims.ToList();
		}

		private JwtSecurityToken GenerateTokenOptions(List<Claim> claims, SigningCredentials signingCredentials)
		{
			return new JwtSecurityToken(
				_issuer,
				_audience,
				claims,
				expires: DateTime.Now.AddMinutes(_expires),
				signingCredentials: signingCredentials
			);
		}
	}
}
