import { apiFetch } from "./apiFetch"

const API_URL = import.meta.env.VITE_API_URL

export async function getCompanies() {
  console.log("Calling API...")

  const response = await apiFetch(`${API_URL}/companies`)

  console.log("Response received:", response)

  if (!response.ok) {
    throw new Error("Failed to fetch companies")
  }

  const data = await response.json()

  console.log("Companies:", data)

  return data
}