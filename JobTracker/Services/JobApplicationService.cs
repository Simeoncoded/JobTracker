using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly ApplicationDbContext _context;

        public JobApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobApplicationResponseDTO>> GetAllAsync(string userId)
        {
            List<JobApplication> applications =
                await _context.JobApplications
                    .Include(application => application.Company)
                    .Where(application => application.UserId == userId)
                    .ToListAsync();

            return applications.Select(application => new JobApplicationResponseDTO
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
            }).ToList();
        }

        public async Task<JobApplicationResponseDTO?> GetByIdAsync(
            int id,
            string userId)
        {
            JobApplication? application =
                await _context.JobApplications
                    .Include(application => application.Company)
                    .FirstOrDefaultAsync(
                        application =>
                            application.Id == id &&
                            application.UserId == userId
                    );

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

        public async Task<JobApplication?> CreateAsync(
            CreateJobApplicationDTO dto,
            string userId)
        {
            bool companyExists = await _context.Companies
       .AnyAsync(
             company =>
                 company.Id == dto.CompanyId &&
                 company.UserId == userId);

            if (!companyExists)
            {
                return null;
            }

            JobApplication application = new JobApplication
            {
                UserId = userId,
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

        public async Task<bool> UpdateAsync(
            int id,
            UpdateJobApplicationDTO dto,
            string userId)
        {
            JobApplication? application =
                await _context.JobApplications
                    .FirstOrDefaultAsync(
                        application =>
                            application.Id == id &&
                            application.UserId == userId
                    );

            if (application == null)
            {
                return false;
            }

            bool companyExists = await _context.Companies
            .AnyAsync(company =>company.Id == dto.CompanyId && company.UserId == userId);

            if (!companyExists)
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

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            JobApplication? application =
                await _context.JobApplications
                    .FirstOrDefaultAsync(
                        application =>
                            application.Id == id &&
                            application.UserId == userId
                    );

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