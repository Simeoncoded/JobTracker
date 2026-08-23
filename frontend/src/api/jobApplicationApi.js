const API_URL = import.meta.env.VITE_API_URL

export async function getJobApplications() {
  const response = await fetch(`${API_URL}/JobApplications`)

  if (!response.ok) {
    throw new Error("Failed to fetch job applications")
  }

  return await response.json()
}