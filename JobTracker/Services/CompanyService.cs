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

        public async Task<List<Company>> GetAllAsync(string userId)
        {
            return await _context.Companies
                .Where(company => company.UserId == userId)
                .ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(
            int id,
            string userId)
        {
            return await _context.Companies
                .FirstOrDefaultAsync(
                    company =>
                        company.Id == id &&
                        company.UserId == userId);
        }

        public async Task<Company> CreateAsync(
            CreateCompanyDTO dto,
            string userId)
        {
            Company company = new Company
            {
                Name = dto.Name,
                Website = dto.Website,
                UserId = userId
            };

            _context.Companies.Add(company);

            await _context.SaveChangesAsync();

            return company;
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateCompanyDTO dto,
            string userId)
        {
            Company? company = await _context.Companies
                .FirstOrDefaultAsync(
                    company =>
                        company.Id == id &&
                        company.UserId == userId);

            if (company == null)
            {
                return false;
            }

            company.Name = dto.Name;
            company.Website = dto.Website;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(
            int id,
            string userId)
        {
            Company? company = await _context.Companies
                .FirstOrDefaultAsync(
                    company =>
                        company.Id == id &&
                        company.UserId == userId);

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