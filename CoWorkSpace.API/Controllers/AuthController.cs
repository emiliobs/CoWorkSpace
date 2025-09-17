using CoWorkSpace.Business.Interfaces;
using CoWorkSpace.Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            try
            {
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
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicate"))
                {
                    return Conflict(new { Message = "Ese correo ya esta siendo utilizado por otro usuario", isSuccess = false });
                }
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException!.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}