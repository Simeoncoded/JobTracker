using JobTracker.Data;
using JobTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JobTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            string? userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            List<Resume> resumes =
                await _context.Resumes
                    .Where(resume => resume.UserId == userId)
                    .ToListAsync();

            return Ok(resumes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            string? userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            Resume? resume =
                await _context.Resumes
                    .FirstOrDefaultAsync(
                        resume =>
                            resume.Id == id &&
                            resume.UserId == userId
                    );

            if (resume == null)
            {
                return NotFound();
            }

            return Ok(resume);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file)
        {
            string? userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            if (file == null || file.Length == 0)
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

            Resume resume = new Resume
            {
                UserId = userId,
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