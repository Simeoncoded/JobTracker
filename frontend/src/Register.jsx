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
    <div className="auth-page">
      <div className="auth-card">
        <h1>JobTracker</h1>

        <p className="auth-subtitle">
          Start tracking your job search.
        </p>

        <h2>Create Account</h2>

        <form onSubmit={handleSubmit}>
          <div className="auth-field">
            <label>Email</label>

            <input
              type="email"
              value={email}
              onChange={(event) => setEmail(event.target.value)}
              placeholder="you@example.com"
              required
            />
          </div>

          <div className="auth-field">
            <label>Password</label>

            <input
              type="password"
              value={password}
              onChange={(event) => setPassword(event.target.value)}
              placeholder="Create a password"
              required
            />
          </div>

          {error && (
            <p className="auth-error">
              {error}
            </p>
          )}

          <button
            type="submit"
            className="auth-button"
          >
            Create Account
          </button>
        </form>

        <p className="auth-switch">
          Already have an account?
        </p>

        <button
          onClick={onRegister}
          className="auth-link-button"
        >
          Back to Login
        </button>
      </div>
    </div>
  )
}

export default Register