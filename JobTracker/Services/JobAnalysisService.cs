using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models;

namespace JobTracker.Services
{
    public class JobAnalysisService : IJobAnalysisService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAiService _aiService;

        public JobAnalysisService(ApplicationDbContext context, IAiService aiService)
        {
            _context = context;
            _aiService = aiService;
        }

        public async Task<JobAnalysis> CreateAsync(CreateJobAnalysisDTO dto)
        {
            var resume = await _context.Resumes.FindAsync(dto.ResumeId);

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
                Analysis = aiResult.Recommendation,
                CreatedAt = DateTime.UtcNow
            };

            _context.JobAnalyses.Add(analysis);

            await _context.SaveChangesAsync();

            return analysis;
        }
    }
}
