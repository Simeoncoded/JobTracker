const API_URL = import.meta.env.VITE_API_URL

export async function getJobApplications() {
  const response = await fetch(`${API_URL}/JobApplications`)

  if (!response.ok) {
    throw new Error("Failed to fetch job applications")
  }

  return await response.json()
}

export async function createJobApplications(application){
  const response = await fetch(`${API_URL}/JobApplications`,{
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(application),
  })

  if(!response.ok){
    throw new Error("Failed to create job application")
  }

  return await response.json()
}