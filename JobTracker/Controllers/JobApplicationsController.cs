using JobTracker.DTOs;
using JobTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationService _service;

        public JobApplicationsController(IJobApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var applications = await _service.GetAllAsync(userId);

            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var application = await _service.GetByIdAsync(id, userId);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobApplicationDTO dto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var application = await _service.CreateAsync(dto, userId);

            if (application == null)
            {
                return BadRequest(new
                {
                    message = "The specified company does not exist."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = application.Id },
                application
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateJobApplicationDTO dto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            bool updated = await _service.UpdateAsync(id, dto, userId);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            bool deleted = await _service.DeleteAsync(id, userId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}