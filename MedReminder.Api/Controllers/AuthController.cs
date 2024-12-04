using MedReminder.Api.Services.Interfaces;
using MedReminder.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedReminder.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IAuthenticationService _authenticationService;

	public AuthController(IAuthenticationService authenticationService)
	{
		_authenticationService = authenticationService;
	}

	[HttpPost("Register")]
	public async Task<IActionResult> Register(RegisterDTO registerDTO)
	{
		try
		{
			var user = await _authenticationService.RegisterAsync(registerDTO);
			return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("Login")]
	public async Task<IActionResult> Login(LoginDTO loginDTO)
	{
		try
		{
			var token = await _authenticationService.LoginAsync(loginDTO);
			return Ok(new TokenDTO { Token = token });
		}
		catch (UnauthorizedAccessException ex)
		{
			return Unauthorized(ex.Message);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

}
