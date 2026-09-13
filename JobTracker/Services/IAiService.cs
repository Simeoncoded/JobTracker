using JobTracker.DTOs;

namespace JobTracker.Services
{
    public interface IAiService
    {
        Task<AiAnalysisResultDTO> AnalyzeResumeAsync(string resumeText,string jobDescription);

    }
}
