const API_URL = import.meta.env.VITE_API_URL

export async function login(email, password) {
  const response = await fetch(`${API_URL}/auth/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      email: email,
      password: password
    })
  })

  if (!response.ok) {
    throw new Error("Invalid email or password")
  }

  const data = await response.json()

  sessionStorage.setItem("token", data.token)

  return data
}