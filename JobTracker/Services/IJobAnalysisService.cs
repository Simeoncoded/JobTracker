using JobTracker.DTOs;
using JobTracker.Models;

namespace JobTracker.Services
{
    public interface IJobAnalysisService
    {
        Task<JobAnalysis> CreateAsync(CreateJobAnalysisDTO dto);
    }
}
