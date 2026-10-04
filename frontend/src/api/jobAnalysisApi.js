import { apiFetch } from "./apiFetch"

const API_URL = import.meta.env.VITE_API_URL

export const createJobAnalysis = async (analysis) => {
  const response = await apiFetch(
    `${API_URL}/JobAnalyses`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(analysis)
    }
  )

  if (!response.ok) {
    throw new Error("Failed to analyze resume")
  }

  return response.json()
}

export const getJobAnalyses = async () => {
  const response = await apiFetch(
    `${API_URL}/JobAnalyses`
  )

  if (!response.ok) {
    throw new Error("Failed to fetch analyses")
  }

  return response.json()
}