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
        
        public JobAnalysesController(ApplicationDbContext context, IJobAnalysisService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<JobAnalysis> analyses =
                await _service.GetAllAsync();

            return Ok(analyses);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobAnalysisDTO dto)
        {
            var analysis = await _service.CreateAsync(dto);

            return Ok(analysis);
        }
    }
}
