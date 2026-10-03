using JobTracker.DTOs;
using JobTracker.Models;

namespace JobTracker.Services
{
    public interface IJobApplicationService
    {
        Task<List<JobApplicationResponseDTO>> GetAllAsync(string userId);
        Task<JobApplicationResponseDTO?> GetByIdAsync(int id,string userId);
        Task<JobApplication?> CreateAsync(CreateJobApplicationDTO dto,string userId);
        Task<bool> UpdateAsync(int id,UpdateJobApplicationDTO dto,string userId);
        Task<bool> DeleteAsync(int id, string userId);
    }
}