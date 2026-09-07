const API_URL = import.meta.env.VITE_API_URL

export const getResumes = async () => {
  const response = await fetch(`${API_URL}/resumes`)

  if (!response.ok) {
    throw new Error("Failed to fetch resumes")
  }

  return response.json()
}

export const createResume = async (file) => {
    const formData = new FormData()

    formData.append("file", file)
  
    const response = await fetch(`${API_URL}/resumes`, {
      method: "POST",
      body: formData
    })
  
    if (!response.ok) {
      throw new Error("Failed to upload resume")
    }
  
    return response.json()
}