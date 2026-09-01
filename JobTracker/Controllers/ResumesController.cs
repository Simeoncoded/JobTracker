using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ResumesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resumes = await _context.Resumes.ToListAsync();

            return Ok(resumes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resume = await _context.Resumes.FindAsync(id);

            if (resume == null)
            {
                return NotFound();
            }

            return Ok(resume);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateResumeDTO dto)
        {
            var resume = new Resume
            {
                FileName = dto.FileName,
                ExtractedText = dto.ExtractedText,
                UploadedDate = DateTime.UtcNow
            };

            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();

            return Ok(resume);
        }
    }
}
