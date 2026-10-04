export async function apiFetch(url, options = {}) {
  const token = sessionStorage.getItem("token")

  const headers = {
    ...options.headers
  }

  if (token) {
    headers.Authorization = `Bearer ${token}`
  }

  const response = await fetch(url, {
    ...options,
    headers: headers
  })

  if (response.status === 401) {
    sessionStorage.removeItem("token")

    window.dispatchEvent(
      new Event("auth:logout")
    )
  }

  return response
}