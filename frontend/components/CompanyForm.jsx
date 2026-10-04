import { useState } from "react"

const API_URL = import.meta.env.VITE_API_URL

function CompanyForm({
    companies,
    onCompanyCreated
  }) {
    const [name, setName] = useState("")
    const [website, setWebsite] = useState("")
    const [error, setError] = useState("")
  
    const handleSubmit = async (event) => {
      event.preventDefault()
      setError("")
  
      try {
        const token = sessionStorage.getItem("token")
  
        const response = await fetch(`${API_URL}/companies`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
          },
          body: JSON.stringify({
            name: name,
            website: website
          })
        })
  
        if (!response.ok) {
          throw new Error("Failed to create company")
        }
  
        const company = await response.json()
  
        onCompanyCreated(company)
  
        setName("")
        setWebsite("")
      } catch (error) {
        setError(error.message)
      }
    }
  
    return (
      <div className="company-form">
  
        <form onSubmit={handleSubmit}>
          <div>
            <label>Company Name</label>
  
            <input
              type="text"
              value={name}
              onChange={(event) => setName(event.target.value)}
              placeholder="e.g. Microsoft"
              required
            />
          </div>
  
          <div>
            <label>Website</label>
  
            <input
              type="url"
              value={website}
              onChange={(event) => setWebsite(event.target.value)}
              placeholder="https://example.com"
            />
          </div>
  
          {error && (
            <p className="company-form-error">
              {error}
            </p>
          )}
  
          <button type="submit">
            Add Company
          </button>
        </form>
  
        <div className="company-form-divider"></div>
  
        <div className="company-form-list-header">
          <h3>Your Companies</h3>
  
          <span className="count-badge">
            {companies.length}
          </span>
        </div>
  
        {companies.length === 0 ? (
          <p className="no-companies">
            No companies added yet.
          </p>
        ) : (
          <div className="company-form-list">
            {companies.map((company) => (
              <div
                className="company-form-item"
                key={company.id}
              >
                <div className="company-avatar">
                  {company.name.charAt(0).toUpperCase()}
                </div>
  
                <div>
                  <strong>{company.name}</strong>
  
                  {company.website && (
                    <span>
                      {company.website}
                    </span>
                  )}
                </div>
              </div>
            ))}
          </div>
        )}
  
      </div>
    )
  }
  
  export default CompanyForm