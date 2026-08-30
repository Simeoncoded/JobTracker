import { useState, useEffect } from "react"
import { createJobApplications, updateJobApplications } from "../src/api/jobApplicationApi"

function ApplicationForm({ companies, onApplicationCreated, editingApplication, onCancelEdit }) {
  const [formData, setFormData] = useState({
    companyId: "",
    jobTitle: "",
    status: "Applied",
    appliedDate: "",
    jobUrl: "",
    location: "",
    salary: "",
    notes: ""
  })

  useEffect(() => {
    if (editingApplication) {
      setFormData({
        companyId: editingApplication.companyId,
        jobTitle: editingApplication.jobTitle,
        status: editingApplication.status,
        appliedDate: editingApplication.appliedDate,
        jobUrl: editingApplication.jobUrl || "",
        location: editingApplication.location || "",
        salary: editingApplication.salary || "",
        notes: editingApplication.notes || ""
      })
    }
  }, [editingApplication])

  const handleChange = (event) => {
    const { name, value } = event.target

    setFormData({
      ...formData,
      [name]: value
    })
  }

  const handleSubmit = async (event) => {
    event.preventDefault()

    const application = {
      ...formData,
      companyId: parseInt(formData.companyId),
      salary: formData.salary
        ? parseFloat(formData.salary) : null
    }
    try {
      if (editingApplication) {
        await updateJobApplications(editingApplication.id, application)
        console.log("Application updated")
        alert("Application updated successfully!")
      } else {
        await createJobApplications(application);
        alert("Application added successfully!")
      }
      onApplicationCreated();
      if (editingApplication) {
        onCancelEdit()
      }

      setFormData({
        companyId: "",
        jobTitle: "",
        status: "Applied",
        appliedDate: "",
        jobUrl: "",
        location: "",
        salary: "",
        notes: ""
      })

    } catch (error) {
      console.error(error)
      alert("Failed to create application")
    }
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
          name="jobTitle"
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
  {editingApplication ? "Update Application" : "Add Application"}
</button>
    </form>

  )
}

export default ApplicationForm