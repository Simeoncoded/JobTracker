namespace JobTracker.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ExtractedText {  get; set; } = string.Empty;
        public DateTime UploadedDate { get; set; }
    }
}
