using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models;
using JobTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var applications = await _service.GetAllAsync();

            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var application = await _service.GetByIdAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobApplicationDTO dto)
        {
            var application = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = application.Id }, application);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateJobApplicationDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if(!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if(!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }       
    }
}
