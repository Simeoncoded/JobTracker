using JobTracker.Data;
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
        public IActionResult GetAll()
        {
            var applications = _context.JobApplications.ToList();

            return Ok(applications);
        }
        [HttpPost]
        public IActionResult Create(JobApplication application)
        {
            _context.JobApplications.Add(application);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = application.Id }, application);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var application = _context.JobApplications.Find(id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }
    }
}
