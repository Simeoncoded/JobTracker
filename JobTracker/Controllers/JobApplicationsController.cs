using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JobApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applications = await _context.JobApplications.ToListAsync();

            return Ok(applications);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateJobApplicationDTO dto)
        {
            var application = new JobApplication
            {
                CompanyName = dto.CompanyName,
                JobTitle = dto.JobTitle,
                Status = dto.Status
            };
            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = application.Id }, application);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var application = await _context.JobApplications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateJobApplicationDTO dto)
        {
            var application = await _context.JobApplications.FindAsync(id);

            if(application == null)
            {
                return NotFound();
            }

            application.CompanyName = dto.CompanyName;
            application.JobTitle = dto.JobTitle;
            application.Status = dto.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var application = await _context.JobApplications.FindAsync(id);

            if(application == null)
            {
                return NotFound();
            }

            _context.JobApplications.Remove(application);
            await _context.SaveChangesAsync();
            return NoContent();
        }

       
    }
}
