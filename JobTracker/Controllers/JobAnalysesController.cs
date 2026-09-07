using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models;
using JobTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobAnalysesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJobAnalysisService _service;
        
        public JobAnalysesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobAnalysisDTO dto)
        {
            var analysis = await _service.CreateAsync(dto);

            return Ok(analysis);
        }
    }
}
