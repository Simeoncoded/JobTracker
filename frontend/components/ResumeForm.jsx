import { useState } from "react"
import { createResume } from "../src/api/resumeApi"

function ResumeForm({ onResumeCreated }) {
  const [file, setFile] = useState(null)

  const handleSubmit = async (event) => {
    event.preventDefault()

    if (!file) {
      alert("Please select a PDF")
      return
    }

    try {
      const result = await createResume(file)

      console.log("Resume created:", result)

      alert("Resume uploaded successfully!")

      setFile(null)
      event.target.reset()

      onResumeCreated()
    } catch (error) {
      console.error(error)
      alert("Failed to upload resume")
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Upload Resume (PDF)</label>

        <input
          type="file"
          accept=".pdf"
          onChange={(event) => setFile(event.target.files[0])}
        />
      </div>

      <button type="submit">
        Upload Resume
      </button>
    </form>
  )
}

export default ResumeForm