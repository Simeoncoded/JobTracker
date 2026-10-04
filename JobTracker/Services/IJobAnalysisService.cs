using JobTracker.DTOs;
using JobTracker.Models;

namespace JobTracker.Services
{
    public interface IJobAnalysisService
    {
        Task<List<JobAnalysis>> GetAllAsync();
        Task<JobAnalysisDTO> CreateAsync(CreateJobAnalysisDTO dto);
    }
}
