using JobTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var applications = new List<JobApplication>
            {new JobApplication
            {
                Id = 1,
                CompanyName = "Microsoft",
                JobTitle = "Software Developer",
                Status = "Applied"
            },new JobApplication
            {
                Id = 2,
                CompanyName = "Microsoft",
                JobTitle = "Software Developer",
                Status = "Applied"
            }
        };

            return Ok(applications);
        }
    }
}
