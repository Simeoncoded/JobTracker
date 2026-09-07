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
        public async Task<IActionResult> Create(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".pdf")
            {
                return BadRequest("Only PDF files are allowed.");
            }

            string extractedText = "";

            using (var stream = file.OpenReadStream())
            using (var document = UglyToad.PdfPig.PdfDocument.Open(stream))
            {
                foreach (var page in document.GetPages())
                {
                    extractedText += page.Text + "\n";
                }
            }

            var resume = new Resume
            {
                FileName = file.FileName,
                ExtractedText = extractedText,
                UploadedDate = DateTime.UtcNow
            };

            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();

            return Ok(resume);
        }
    }
}
