namespace JobTracker.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Website { get; set; }
        public HashSet<JobApplication> JobApplications { get; set; }= new HashSet<JobApplication>();
    }
}
