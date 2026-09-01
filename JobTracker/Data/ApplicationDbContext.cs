using JobTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Resume> Resumes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobApplication>()
                .HasOne(j => j.Company)
                .WithMany(c => c.JobApplications)
                .HasForeignKey(j => j.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}