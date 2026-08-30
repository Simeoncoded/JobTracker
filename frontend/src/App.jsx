import { useState } from "react"
import { useEffect } from "react";
import Navbar from "../components/Navbar"
import ApplicationForm from "../components/ApplicationForm"
import { getCompanies } from "./api/companyApi";
import { getJobApplications } from "./api/jobApplicationApi";
import ApplicationList from "../components/ApplicationList";
import "./App.css"

function App() {
  const [companies, setCompanies] = useState([]);
  const [applications, setApplications] = useState([]);
  const [editingApplication, setEditingApplication] = useState(null)
  const [statusFilter, setStatusFilter] = useState("All")

  useEffect(() => {
    getCompanies()
      .then((data) => {
        setCompanies(data)
      })
      .catch((error) => {
        console.error(error)
      })
    getJobApplications()
      .then((data) => {
        setApplications(data)
      })
      .catch((error) => {
        console.error(error)
      })
  }, [])

  const loadApplications = () => {
    getJobApplications()
      .then((data) => {
        setApplications(data)
      })
      .catch((error) => {
        console.error(error)
      })
  }

  const filteredApplications =
    statusFilter === "All"
      ? applications
      : applications.filter(
        (application) => application.status === statusFilter
      )

  const totalApplications = applications.length

  const appliedApplications = applications.filter(
    (application) => application.status === "Applied").length

  const interviewedApplications = applications.filter(
    (application) => application.status === "Interview").length

  const offerApplications = applications.filter(
    (application) => application.status === "Offer").length

  const rejectedApplications = applications.filter(
    (application) => application.status === "Rejected").length

  const handleEdit = (application) => {
    setEditingApplication(application)
  }
  const handleCancelEdit = () => {
    setEditingApplication(null)
  }


  return (
    <div>
      <Navbar title="JobTracker" />

      <ApplicationForm companies={companies}
        onApplicationCreated={loadApplications}
        editingApplication={editingApplication}
        onCancelEdit={handleCancelEdit}
      />


      <h2>Companies</h2>

      <ul>
        {companies.map((company) => (
          <li key={company.id}>
            {company.name}
          </li>
        ))}
      </ul>

      <div className="stats">
        <div className="stat-card">
          <h3>Total Applications</h3>
          <p>{totalApplications}</p>
        </div>

        <div className="stat-card">
          <h3>Applied</h3>
          <p>{appliedApplications}</p>
        </div>

        <div className="stat-card">
          <h3>Interview</h3>
          <p>{interviewedApplications}</p>
        </div>

        <div className="stat-card">
          <h3>Offer</h3>
          <p>{offerApplications}</p>
        </div>

        <div className="stat-card">
          <h3>Rejected</h3>
          <p>{rejectedApplications}</p>
        </div>
      </div>


      <div>
        <button onClick={() => setStatusFilter("All")}>
          All
        </button>

        <button onClick={() => setStatusFilter("Applied")}>
          Applied
        </button>

        <button onClick={() => setStatusFilter("Interview")}>
          Interview
        </button>

        <button onClick={() => setStatusFilter("Rejected")}>
          Rejected
        </button>

        <button onClick={() => setStatusFilter("Offer")}>
          Offer
        </button>
      </div>

      <ApplicationList applications={filteredApplications} onApplicationDeleted={loadApplications} onApplicationEdit={handleEdit} />

    </div>
  )
}

export default App
