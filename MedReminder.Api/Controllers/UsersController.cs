﻿using MedReminder.Api.DTOs;
using MedReminder.Api.Services.Interfaces;
using MedReminder.Api.Tools;
using Microsoft.AspNetCore.Mvc;

namespace MedReminder.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDTO = user.ToDTO();
            return Ok(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserDTO userDTO)
        {
            var user = userDTO.ToDomain();
            var id = await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetByIdAsync), new { id }, user);
        }
    }
}