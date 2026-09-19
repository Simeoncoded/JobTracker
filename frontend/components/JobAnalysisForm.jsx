import { useState } from "react"
import { createJobAnalysis } from "../src/api/jobAnalysisApi"

function JobAnalysisForm({ resumes, onAnalysisCreated }) {
  const [resumeId, setResumeId] = useState("")
  const [jobTitle, setJobTitle] = useState("")
  const [jobDescription, setJobDescription] = useState("")

  const handleSubmit = async (event) => {
    event.preventDefault()

    if (!resumeId || !jobTitle || !jobDescription) {
      alert("Please fill in all fields")
      return
    }

    try {
      const result = await createJobAnalysis({
        resumeId: parseInt(resumeId),
        jobTitle: jobTitle,
        jobDescription: jobDescription
      })

      console.log("Analysis created:", result)

      alert("Resume analyzed successfully!")

      setResumeId("")
      setJobTitle("")
      setJobDescription("")

      onAnalysisCreated(result)
    } catch (error) {
      console.error(error)
      alert("Failed to analyze resume")
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Resume</label>

        <select
          value={resumeId}
          onChange={(event) => setResumeId(event.target.value)}
        >
          <option value="">Select a resume</option>

          {resumes.map((resume) => (
            <option key={resume.id} value={resume.id}>
              {resume.fileName}
            </option>
          ))}
        </select>
      </div>

      <div>
        <label>Job Title</label>

        <input
          type="text"
          value={jobTitle}
          onChange={(event) => setJobTitle(event.target.value)}
          placeholder="e.g. Junior Software Developer"
        />
      </div>

      <div>
        <label>Job Description</label>

        <textarea
          value={jobDescription}
          onChange={(event) => setJobDescription(event.target.value)}
          placeholder="Paste the job description here..."
          rows="10"
        />
      </div>

      <button type="submit">
        Analyze Resume
      </button>
    </form>
  )
}

export default JobAnalysisForm