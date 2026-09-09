namespace JobTracker.Services
{
    public interface IAiService
    {
        Task<string> AnalyzeResumeAsync(string resumeText, string jobDescription);

    }
}
