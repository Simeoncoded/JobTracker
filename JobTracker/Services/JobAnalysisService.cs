using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models;

namespace JobTracker.Services
{
    public class JobAnalysisService : IJobAnalysisService
    {
        private readonly ApplicationDbContext _context;

        public JobAnalysisService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JobAnalysis> CreateAsync(CreateJobAnalysisDTO dto)
        {
            var resume = await _context.Resumes.FindAsync(dto.ResumeId);

            if (resume == null)
            {
                throw new Exception("Resume not found.");
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

            return analysis;
        }
    }
}
