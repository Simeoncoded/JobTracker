using JobTracker.DTOs;
using JobTracker.Models;

namespace JobTracker.Services
{
    public interface ICompanyService
    {
        Task<List<Company>> GetAllAsync();

        Task<Company?> GetByIdAsync(int id);

        Task<Company> CreateAsync(CreateCompanyDTO dto);

        Task<bool> UpdateAsync(int id, UpdateCompanyDTO dto);

        Task<bool> DeleteAsync(int id);
    }
}
