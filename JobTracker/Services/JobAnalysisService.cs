using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JobTracker.Services
{
    public class JobAnalysisService : IJobAnalysisService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAiService _aiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobAnalysisService(ApplicationDbContext context, IAiService aiService, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _aiService = aiService;
            _httpContextAccessor = contextAccessor;
        }

        public async Task<List<JobAnalysis>> GetAllAsync()
        {
            string? userId =
                _httpContextAccessor.HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new Exception("User not authenticated.");
            }

            List<JobAnalysis> analyses =
                await _context.JobAnalyses
                    .Where(analysis => analysis.Resume.UserId == userId)
                    .OrderByDescending(analysis => analysis.CreatedAt)
                    .ToListAsync();

            return analyses;
        }
        public async Task<JobAnalysis> CreateAsync(CreateJobAnalysisDTO dto)
        {
            string? userId =
                _httpContextAccessor.HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new Exception("User not authenticated.");
            }

            Resume? resume =
                await _context.Resumes
                    .FirstOrDefaultAsync(
                        resume =>
                            resume.Id == dto.ResumeId &&
                            resume.UserId == userId
                    );

            if (resume == null)
            {
                throw new Exception("Resume not found.");
            }

            AiAnalysisResultDTO aiResult =
                await _aiService.AnalyzeResumeAsync(
                    resume.ExtractedText,
                    dto.JobDescription
                );

            JobAnalysis analysis = new JobAnalysis
            {
                ResumeId = dto.ResumeId,
                JobTitle = dto.JobTitle,
                JobDescription = dto.JobDescription,
                MatchScore = aiResult.MatchScore,
                MatchingSkills = aiResult.MatchingSkills,
                MissingSkills = aiResult.MissingSkills,
                Recommendation = aiResult.Recommendation,
                CreatedAt = DateTime.UtcNow
            };

            _context.JobAnalyses.Add(analysis);

            await _context.SaveChangesAsync();

            return analysis;
        }
    }
}
