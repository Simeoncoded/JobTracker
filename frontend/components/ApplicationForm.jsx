import { useState } from "react"

function ApplicationForm({ companies }) {
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
  
    console.log("Form Data:", formData)
  }

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Company</label>

        <select
        name="companyId"
          value={formData.companyId}
          onChange={handleChange}>
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
          name = "jobTitle"
          value={formData.jobTitle}
          onChange={handleChange}
          placeholder="e.g. Software Developer"
        />
      </div>
      <div>
        <label>Status</label>

        <select
        name="status"
          value={formData.status}
          onChange={handleChange}
        >
          <option value="Applied">Applied</option>
          <option value="Interview">Interview</option>
          <option value="Rejected">Rejected</option>
          <option value="Offer">Offer</option>
        </select>
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
          placeholder="https://example.com/job"
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