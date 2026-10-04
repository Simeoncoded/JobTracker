import { useState } from "react"
import { register } from "./api/authApi"

function Register({ onRegister }) {
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [error, setError] = useState("")

  const handleSubmit = async (event) => {
    event.preventDefault()
    setError("")

    try {
      await register(email, password)

      onRegister()
    } catch (error) {
      setError(error.message)
    }
  }

  return (
    <div>
      <h1>Create Account</h1>

      <form onSubmit={handleSubmit}>
        <div>
          <label>Email</label>

          <input
            type="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
            required
          />
        </div>

        <div>
          <label>Password</label>

          <input
            type="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
            required
          />
        </div>

        <button type="submit">
          Create Account
        </button>
      </form>

      {error && (
        <p>{error}</p>
      )}

      <button onClick={onRegister}>
        Already have an account? Login
      </button>
    </div>
  )
}

export default Register