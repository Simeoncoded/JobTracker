using JobTracker.DTOs;
using JobTracker.Models;

namespace JobTracker.Services
{
    public interface ICompanyService
    {
        Task<List<Company>> GetAllAsync(string userId);
        Task<Company?> GetByIdAsync(int id, string userId);
        Task<Company> CreateAsync(CreateCompanyDTO dto, string userId);
        Task<bool> UpdateAsync(int id, UpdateCompanyDTO dto,string userId);
        Task<bool> DeleteAsync(int id, string userId);
    }
}