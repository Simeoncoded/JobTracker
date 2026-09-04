const API_URL = import.meta.env.VITE_API_URL

export const getResumes = async () => {
  const response = await fetch(`${API_URL}/resumes`)

  if (!response.ok) {
    throw new Error("Failed to fetch resumes")
  }

  return response.json()
}

export const createResume = async (resume) => {
  const response = await fetch(`${API_URL}/resumes`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(resume)
  })

  if (!response.ok) {
    throw new Error("Failed to create resume")
  }

  return response.json()
}