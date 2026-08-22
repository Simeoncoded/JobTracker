using JobTracker.DTOs;
using JobTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.Services
{
    public interface IJobApplicationService 
    {
        Task<List<JobApplicationResponseDTO>> GetAllAsync();
        Task<JobApplicationResponseDTO?> GetByIdAsync(int id);
        Task<JobApplication> CreateAsync(CreateJobApplicationDTO dto);
        Task<bool> UpdateAsync(int id, UpdateJobApplicationDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
