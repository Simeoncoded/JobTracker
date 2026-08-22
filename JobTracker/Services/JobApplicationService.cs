using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        public readonly ApplicationDbContext _context;
        public JobApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobApplicationResponseDTO>> GetAllAsync()
        {
            var application = await _context.JobApplications
                .Include(c => c.Company)
                .ToListAsync();

            return application.Select(x => new JobApplicationResponseDTO
            {
                Id = x.Id,
                CompanyId = x.CompanyId,
                CompanyName = x.Company.Name,
                JobTitle = x.JobTitle,
                Status = x.Status,
                AppliedDate = x.AppliedDate,
                JobUrl = x.JobUrl,
                Location = x.Location,
                Salary = x.Salary,
                Notes = x.Notes
            }).ToList();
        }



        public async Task<JobApplicationResponseDTO?> GetByIdAsync(int id)
        {
            var application = await _context.JobApplications
                .Include(c => c.Company)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (application == null)
            {
                return null;
            }


            return new JobApplicationResponseDTO
            {
                Id = application.Id,
                CompanyId = application.CompanyId,
                CompanyName = application.Company.Name,
                JobTitle = application.JobTitle,
                Status = application.Status,
                AppliedDate = application.AppliedDate,
                JobUrl = application.JobUrl,
                Location = application.Location,
                Salary = application.Salary,
                Notes = application.Notes
            };

        }

        public async Task<JobApplication> CreateAsync(CreateJobApplicationDTO dto)
        {
            var companyExists = await _context.Companies
         .AnyAsync(c => c.Id == dto.CompanyId);

            if (!companyExists)
            {
                return null;
                     
            }
            var application = new JobApplication
            {
                CompanyId = dto.CompanyId,
                JobTitle = dto.JobTitle,
                Status = dto.Status,
                AppliedDate = dto.AppliedDate,
                JobUrl = dto.JobUrl,
                Location = dto.Location,
                Salary = dto.Salary,
                Notes = dto.Notes
            };

            _context.JobApplications.Add(application);

            await _context.SaveChangesAsync();

            return application;
        }

        public async Task<bool> UpdateAsync(int id, UpdateJobApplicationDTO dto)
        {
            var application = await _context.JobApplications.FindAsync(id);

            var companyExists = await _context.Companies
            .AnyAsync(c => c.Id == dto.CompanyId);

            if (!companyExists)
            {
                return false;
            }

            if (application == null)
            {
                return false;
            }

            application.CompanyId = dto.CompanyId;
            application.JobTitle = dto.JobTitle;
            application.Status = dto.Status;
            application.AppliedDate = dto.AppliedDate;
            application.JobUrl = dto.JobUrl;
            application.Location = dto.Location;
            application.Salary = dto.Salary;
            application.Notes = dto.Notes;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var application = await _context.JobApplications.FindAsync(id);

            if (application == null)
            {
                return false;
            }

            _context.JobApplications.Remove(application);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
