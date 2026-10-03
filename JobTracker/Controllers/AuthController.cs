using JobTracker.DTOs;
using JobTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            IdentityResult result = await _userManager.CreateAsync(
                user,
                dto.Password
            );

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new
            {
                message = "Account created successfully."
            });
        }
    }
}
