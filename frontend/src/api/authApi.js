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

export async function register(email, password) {
  const response = await fetch(`${API_URL}/auth/register`, {
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
    const data = await response.json()

    if (Array.isArray(data)) {
      const messages = data.map((error) => error.description)

      throw new Error(messages.join(" "))
    }

    throw new Error("Failed to create account")
  }

  return await response.json()
}