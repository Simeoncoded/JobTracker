using JobTracker.Data;
using JobTracker.DTOs;
using JobTracker.Models;
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

        public async Task<List<JobApplication>> GetAllAsync()
        {
            return await _context.JobApplications.ToListAsync();
        }

        public async Task<JobApplication?> GetByIdAsync(int id)
        {
            return await _context.JobApplications.FindAsync(id);
        }

        public async Task<JobApplication> CreateAsync(CreateJobApplicationDTO dto)
        {
            var application = new JobApplication
            {
                CompanyName = dto.CompanyName,
                JobTitle = dto.JobTitle,
                Status = dto.Status
            };

            _context.JobApplications.Add(application);

            await _context.SaveChangesAsync();

            return application;
        }

        public async Task<bool> UpdateAsync(int id, UpdateJobApplicationDTO dto)
        {
            var application = await _context.JobApplications.FindAsync(id);

            if (application == null)
            {
                return false;
            }

            application.CompanyName = dto.CompanyName;
            application.JobTitle = dto.JobTitle;
            application.Status = dto.Status;

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
