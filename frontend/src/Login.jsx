import { useState } from "react"
import { login } from "./api/authApi"

function Login({ onLogin, onRegister }) {
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [error, setError] = useState("")

  const handleSubmit = async (event) => {
    event.preventDefault()
    setError("")

    try {
      await login(email, password)
      onLogin()
    } catch (error) {
      setError(error.message)
    }
  }

  return (
    <div className="auth-page">
      <div className="auth-card">
        <h1>JobTracker</h1>

        <p className="auth-subtitle">
          Track your job applications in one place.
        </p>

        <h2>Welcome Back</h2>

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
              placeholder="Enter your password"
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
            Login
          </button>
        </form>

        <p className="auth-switch">
          Don't have an account?
        </p>

        <button
          onClick={onRegister}
          className="auth-link-button"
        >
          Create an account
        </button>
      </div>
    </div>
  )
}

export default Login