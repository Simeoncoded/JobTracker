#pragma warning disable OPENAI001
using OpenAI.Responses;

namespace JobTracker.Services
{
    public class AiService : IAiService
    {
        private readonly ResponsesClient _client;
        public AiService(IConfiguration configuration)
        {
            string apiKey = configuration["OpenAI:ApiKey"]
                ?? throw new Exception("OpenAI API key is missing.");

            _client = new ResponsesClient(apiKey);
        }

        public async Task<string> AnalyzeResumeAsync(
         string resumeText,
         string jobDescription)
        {
            string prompt = $"""
            Compare the following resume against the job description.

            RESUME:
            {resumeText}

            JOB DESCRIPTION:
            {jobDescription}

            Give a concise assessment of how well the candidate matches
            the job. Include:
            - Overall match percentage
            - Matching skills
            - Missing skills
            - Short recommendation
            """;

             ResponseResult response = await _client.CreateResponseAsync(
                "gpt-5.2",
                prompt
            );

            return response.GetOutputText();
        }
    }
}
