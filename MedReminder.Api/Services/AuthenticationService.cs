using MedReminder.Api.Services.Interfaces;
using MedReminder.Api.Tools;
using MedReminder.Dal.Interfaces;
using MedReminder.DAL.Models;
using MedReminder.Shared.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedReminder.Api.Services;

public class AuthenticationService : IAuthenticationService
{
	private readonly IUserDao _userDao;
	private readonly JwtSettings _jwtSettings;

	public AuthenticationService(IUserDao userDao, IOptions<JwtSettings> jwtSettings)
	{
		_userDao = userDao;
		_jwtSettings = jwtSettings.Value;
	}


	public async Task<User> RegisterAsync(RegisterDTO registerDTO)
	{
		var existingUser = await _userDao.GetUserByEmailAsync(registerDTO.Email);
		if (existingUser != null)
		{
			throw new Exception("User with this email already exists");
		}

		var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);

		var user = new User
		{
			FirstName = registerDTO.FirstName,
			LastName = registerDTO.LastName,
			Email = registerDTO.Email,
			PasswordHash = hashedPassword,
			CreatedAt = DateTime.UtcNow
		};

		var id = await _userDao.CreateUserAsync(user);
		user.Id = id;

		return user;
	}

	public async Task<string> LoginAsync(LoginDTO loginDTO)
	{
		var user = await _userDao.GetUserByEmailAsync(loginDTO.Email);
		if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
		{
			throw new UnauthorizedAccessException("Invalid crendetials.");
		}

		return GenerateJwtToken(user);
	}

	private string GenerateJwtToken(User user)
	{
		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			_jwtSettings.Issuer,
			_jwtSettings.Audience,
			claims,
			expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
