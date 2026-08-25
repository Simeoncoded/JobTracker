import { useState } from "react"

function ApplicationForm({ companies }) {
  const [jobTitle, setJobTitle] = useState("")
  const [companyId, setCompanyId] = useState("")

  const [formData, setFormData] = useState({
    companyId: "",
    jobTitle: "",
    status: "",
    appliedDate: "",
    jobUrl: "",
    location: "",
    salary: "",
    notes: ""
  })

  const handleChange = (event) => {
    const { name, value } = event.target

    setFormData({
      ...formData,
      [name]: value
    })
  }
  const handleSubmit = (event) => {
    event.preventDefault()

    console.log(formData)
  }

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Company ID</label>
        <input
          type="number"
          name="companyId"
          value={formData.companyId}
          onChange={handleChange}
        />
      </div>

      <div>
        <label>Company</label>

        <select
          value={companyId}
          onChange={(event) => setCompanyId(event.target.value)}>
          <option value="">Select a company</option>

          {companies.map((company) => (
            <option key={company.id} value={company.id}>
              {company.name}
            </option>
          ))}
        </select>
      </div>

      <div>
        <label>Job Title</label>

        <input
          type="text"
          value={jobTitle}
          onChange={(event) => setJobTitle(event.target.value)}
          placeholder="e.g. Software Developer"
        />
      </div>
      <div>
        <label>Status</label>
        <input
          type="text"
          name="status"
          value={formData.status}
          onChange={handleChange}
          placeholder="e.g. Applied"
        />
      </div>

      <div>
        <label>Applied Date</label>
        <input
          type="date"
          name="appliedDate"
          value={formData.appliedDate}
          onChange={handleChange}
        />
      </div>

      <div>
        <label>Job URL</label>
        <input
          type="url"
          name="jobUrl"
          value={formData.jobUrl}
          onChange={handleChange}
          placeholder="https://..."
        />
      </div>

      <div>
        <label>Location</label>
        <input
          type="text"
          name="location"
          value={formData.location}
          onChange={handleChange}
          placeholder="e.g. Toronto, ON"
        />
      </div>

      <div>
        <label>Salary</label>
        <input
          type="number"
          name="salary"
          value={formData.salary}
          onChange={handleChange}
          placeholder="e.g. 70000"
        />
      </div>

      <div>
        <label>Notes</label>
        <textarea
          name="notes"
          value={formData.notes}
          onChange={handleChange}
          placeholder="Add notes..."
        />
      </div>

      <button type="submit">
        Add Application
      </button>
    </form>

  )
}

export default ApplicationForm