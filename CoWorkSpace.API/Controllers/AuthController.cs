using CoWorkSpace.Business.Interfaces;
using CoWorkSpace.Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Invalid login request");
            }
            var response = await _authService.Login(loginRequest);
            if (response.IsAuthenticated)
            {
                return Ok(response);
            }
            else
            {
                return Unauthorized(response.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO createUser)
        {
            if (createUser == null)
            {
                return BadRequest("Invalid registration request.");
            }
            var response = await _authService.Register(createUser);
            if (response != null)
            {
                return CreatedAtAction(nameof(Register), new { id = response.Id }, response);
            }
            else
            {
                return BadRequest("Registration failed.");
            }
        }
    }
}