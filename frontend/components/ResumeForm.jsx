import { useState } from "react"
import { createResume } from "../src/api/resumeApi"

function ResumeForm({ onResumeCreated }) {
  const [fileName, setFileName] = useState("")
  const [extractedText, setExtractedText] = useState("")

  const handleSubmit = async (event) => {
    event.preventDefault()

    const resume = {
      fileName,
      extractedText
    }

    try {
      const result = await createResume(resume)

      console.log("Resume created:", result)

      alert("Resume saved successfully!")

      onResumeCreated()
    } catch (error) {
      console.error(error)
      alert("Failed to save resume")
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>File Name</label>

        <input
          type="text"
          value={fileName}
          onChange={(event) => setFileName(event.target.value)}
          placeholder="e.g. Simeon-Resume.pdf"
        />
      </div>

      <div>
        <label>Resume Text</label>

        <textarea
          value={extractedText}
          onChange={(event) => setExtractedText(event.target.value)}
          placeholder="Paste your resume text here..."
        />
      </div>

      <button type="submit">
        Save Resume
      </button>
    </form>
  )
}

export default ResumeForm