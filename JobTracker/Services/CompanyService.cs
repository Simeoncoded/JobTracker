using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;

        public CompanyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<Company> CreateAsync(CreateCompanyDTO dto)
        {
            var company = new Company
            {
                Name = dto.Name,
                Website = dto.Website
            };

            _context.Companies.Add(company);

            await _context.SaveChangesAsync();

            return company;
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateCompanyDTO dto)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return false;
            }

            company.Name = dto.Name;
            company.Website = dto.Website;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return false;
            }

            _context.Companies.Remove(company);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
