import { useState } from "react"
import { useEffect } from "react";
import Navbar from "../components/Navbar"
import ApplicationForm from "../components/ApplicationForm"
import { getCompanies } from "./api/companyApi";
import { getJobApplications } from "./api/jobApplicationApi";
import ApplicationList from "../components/ApplicationList";
import ResumeForm from "../components/ResumeForm";
import { getResumes } from "./api/resumeApi";
import "./App.css"

function App() {
  const [companies, setCompanies] = useState([]);
  const [applications, setApplications] = useState([]);
  const [editingApplication, setEditingApplication] = useState(null)
  const [statusFilter, setStatusFilter] = useState("All")
  const [searchTerm, setSearchTerm] = useState("")
  const [resumes, setResumes] = useState([])

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
    getResumes()
      .then((data) => {
        setResumes(data)
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

  const loadResumes = () => {
    getResumes()
      .then((data) => {
        setResumes(data)
      })
      .catch((error) => {
        console.error(error)
      })
  }

  const filteredApplications = applications.filter((application) => {
    const matchesStatus =
      statusFilter === "All" ||
      application.status === statusFilter

    const matchesSearch =
      application.jobTitle
        .toLowerCase()
        .includes(searchTerm.toLowerCase()) ||
      application.companyName
        .toLowerCase()
        .includes(searchTerm.toLowerCase())

    return matchesStatus && matchesSearch
  })
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

      <div className="search">
        <input
          type="text"
          placeholder="Search by job title or company..."
          value={searchTerm}
          onChange={(event) => setSearchTerm(event.target.value)}
        />
      </div>

      <div className="filters">
        <button
          className={statusFilter === "All" ? "active" : ""}
          onClick={() => setStatusFilter("All")}
        >
          All
        </button>

        <button
          className={statusFilter === "Applied" ? "active" : ""}
          onClick={() => setStatusFilter("Applied")}
        >
          Applied
        </button>

        <button
          className={statusFilter === "Interview" ? "active" : ""}
          onClick={() => setStatusFilter("Interview")}
        >
          Interview
        </button>

        <button
          className={statusFilter === "Offer" ? "active" : ""}
          onClick={() => setStatusFilter("Offer")}
        >
          Offer
        </button>

        <button
          className={statusFilter === "Rejected" ? "active" : ""}
          onClick={() => setStatusFilter("Rejected")}
        >
          Rejected
        </button>
      </div>

      <ApplicationList applications={filteredApplications} onApplicationDeleted={loadApplications} onApplicationEdit={handleEdit} />

      <h2>Resume</h2>

      <ResumeForm onResumeCreated={loadResumes} />

      <ul>
        {resumes.map((resume) => (
          <li key={resume.id}>
            {resume.fileName}
          </li>
        ))}
      </ul>

    </div>
  )
}

export default App
