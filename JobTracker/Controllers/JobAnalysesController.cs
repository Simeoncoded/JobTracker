using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobAnalysesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public JobAnalysesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobAnalysisDTO dto)
        {
            var resume = _context.Resumes.FindAsync(dto.ResumeId);

            if(resume == null)
            {
                return NotFound("Resume not found");
            }

            var analysis = new JobAnalysis
            {
                ResumeId = dto.ResumeId,
                JobTitle = dto.JobTitle,
                JobDescription = dto.JobDescription,
                MatchScore = 0,
                Analysis = "Analysis pending.",
                CreatedAt = DateTime.UtcNow
            };

            _context.JobAnalyses.Add(analysis);
            await _context.SaveChangesAsync();

            return Ok(analysis);
        }
    }
}
