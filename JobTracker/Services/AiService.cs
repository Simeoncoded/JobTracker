#pragma warning disable OPENAI001

using JobTracker.DTOs;
using JobTracker.Services;
using Microsoft.Extensions.Configuration;
using OpenAI.Responses;
using System.Text.Json;

public class AiService : IAiService
{
    private readonly ResponsesClient _client;

    public AiService(IConfiguration configuration)
    {
        string apiKey = configuration["OpenAI:ApiKey"]
            ?? throw new Exception("OpenAI API key is missing.");

        _client = new ResponsesClient(apiKey);
    }

    public async Task<AiAnalysisResultDTO> AnalyzeResumeAsync(
        string resumeText,
        string jobDescription)
    {
        string prompt = $"""
    Compare the following resume against the job description.

    RESUME:
    {resumeText}

    JOB DESCRIPTION:
    {jobDescription}

    Return ONLY valid JSON.
    Do not include markdown or ```json.

    The JSON must contain these properties:

    - matchScore: an integer from 0 to 100
    - matchingSkills: an array of matching skills
    - missingSkills: an array of missing or weak skills
    - recommendation: a short recommendation

    Rules:
    - matchScore must be an integer from 0 to 100.
    - matchingSkills should contain skills from the job description
      that are demonstrated in the resume.
    - missingSkills should contain important job requirements that
      are missing or weakly demonstrated in the resume.
    - recommendation should be concise.
    """;

        ResponseResult response = await _client.CreateResponseAsync(
            "gpt-5.2",
            prompt
        );

        string aiResponse = response.GetOutputText();

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        AiAnalysisResultDTO result =
            JsonSerializer.Deserialize<AiAnalysisResultDTO>(
                aiResponse,
                options
            )
            ?? throw new Exception("AI returned an invalid response."); 

        return result;
    }
}