import { useState } from "react"
import { useEffect } from "react"
import ApplicationForm from "../components/ApplicationForm"
import { getCompanies } from "./api/companyApi"
import { getJobApplications } from "./api/jobApplicationApi"
import ApplicationList from "../components/ApplicationList"
import ResumeForm from "../components/ResumeForm"
import { getResumes } from "./api/resumeApi"
import JobAnalysisForm from "../components/JobAnalysisForm"
import AnalysisResult from "../components/AnalysisResult"
import { getJobAnalyses } from "./api/jobAnalysisApi"
import AnalysisHistory from "../components/AnalysisHistory"
import Login from "./Login"
import Register from "./Register"
import CompanyForm from "../components/CompanyForm"
import "./App.css"

function App() {
  const [companies, setCompanies] = useState([])
  const [applications, setApplications] = useState([])
  const [editingApplication, setEditingApplication] = useState(null)
  const [statusFilter, setStatusFilter] = useState("All")
  const [searchTerm, setSearchTerm] = useState("")
  const [resumes, setResumes] = useState([])
  const [latestAnalysis, setLatestAnalysis] = useState(null)
  const [analyses, setAnalyses] = useState([])
  const [showRegister, setShowRegister] = useState(false)
  const [isLoggedIn, setIsLoggedIn] = useState(
    Boolean(sessionStorage.getItem("token"))
  )

  useEffect(() => {
    if (!isLoggedIn) {
      return
    }
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

    getJobAnalyses()
      .then((data) => {
        setAnalyses(data)
      })
      .catch((error) => {
        console.error(error)
      })
  }, [isLoggedIn])

  useEffect(() => {
    const handleAuthLogout = () => {
      setCompanies([])
      setApplications([])
      setResumes([])
      setAnalyses([])
      setLatestAnalysis(null)
      setEditingApplication(null)
  
      setIsLoggedIn(false)
    }
  
    window.addEventListener("auth:logout", handleAuthLogout)
  
    return () => {
      window.removeEventListener("auth:logout", handleAuthLogout)
    }
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
  const handleLogout = () => {
    sessionStorage.removeItem("token")

    setCompanies([])
    setApplications([])
    setResumes([])
    setAnalyses([])
    setLatestAnalysis(null)
    setEditingApplication(null)

    setIsLoggedIn(false)
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
    (application) => application.status === "Applied"
  ).length

  const interviewedApplications = applications.filter(
    (application) => application.status === "Interview"
  ).length

  const offerApplications = applications.filter(
    (application) => application.status === "Offer"
  ).length

  const rejectedApplications = applications.filter(
    (application) => application.status === "Rejected"
  ).length

  const handleEdit = (application) => {
    setEditingApplication(application)
  }

  const handleCancelEdit = () => {
    setEditingApplication(null)
  }

  if (!isLoggedIn) {
    if (showRegister) {
      return (
        <Register
          onRegister={() => setShowRegister(false)}
        />
      )
    }

    return (
      <Login
        onLogin={() => setIsLoggedIn(true)}
        onRegister={() => setShowRegister(true)}
      />
    )
  }
  return (
    <div className="app">
      <main className="dashboard">

        <div className="dashboard-topbar">
          <span className="dashboard-brand">
            JobTracker
          </span>

          <button
            className="logout-button"
            onClick={handleLogout}
          >
            Logout
          </button>
        </div>

        <section className="hero">
          <div>
            <p className="eyebrow">JOB SEARCH DASHBOARD</p>

            <h1>
              Keep your job search
              <span> organized.</span>
            </h1>

            <p className="hero-text">
              Track applications, manage resumes, and use AI to see
              how well your resume matches a job.
            </p>
          </div>
        </section>

        <section className="form-grid">

          <div className="section-card compact-form">
            <div className="section-header">
              <div>
                <p className="section-label">COMPANIES</p>
                <h2>Add Company</h2>
                <p>
                  Add a company you're applying to.
                </p>
              </div>
            </div>

            <CompanyForm
              companies={companies}
              onCompanyCreated={(company) => {
                setCompanies((currentCompanies) => [
                  ...currentCompanies,
                  company
                ])
              }}
            />
          </div>

          <div className="section-card compact-form">
            <div className="section-header">
              <div>
                <p className="section-label">APPLICATIONS</p>
                <h2>Add Job Application</h2>
                <p>
                  Track a new job application.
                </p>
              </div>
            </div>

            <ApplicationForm
              companies={companies}
              onApplicationCreated={loadApplications}
              editingApplication={editingApplication}
              onCancelEdit={handleCancelEdit}
            />
          </div>

        </section>

        {/* Statistics */}

        <section className="stats">

          <div className="stat-card">
            <div className="stat-icon">📋</div>
            <p>Total Applications</p>
            <h3>{totalApplications}</h3>
          </div>

          <div className="stat-card">
            <div className="stat-icon">📨</div>
            <p>Applied</p>
            <h3>{appliedApplications}</h3>
          </div>

          <div className="stat-card">
            <div className="stat-icon">💬</div>
            <p>Interviews</p>
            <h3>{interviewedApplications}</h3>
          </div>

          <div className="stat-card">
            <div className="stat-icon">🎉</div>
            <p>Offers</p>
            <h3>{offerApplications}</h3>
          </div>

          <div className="stat-card">
            <div className="stat-icon">✕</div>
            <p>Rejected</p>
            <h3>{rejectedApplications}</h3>
          </div>

        </section>

        {/* Applications */}

        <section className="section-card">

          <div className="section-header">
            <div>
              <p className="section-label">JOB SEARCH</p>
              <h2>Your Applications</h2>
              <p>
                Search and filter your current applications.
              </p>
            </div>
          </div>

          <div className="search-container">
            <span className="search-icon">⌕</span>

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

          <ApplicationList
            applications={filteredApplications}
            onApplicationDeleted={loadApplications}
            onApplicationEdit={handleEdit}
          />

        </section>

        {/* Resume */}

        <section className="section-card">

          <div className="section-header">
            <div>
              <p className="section-label">RESUME</p>
              <h2>Manage Your Resume</h2>
              <p>
                Upload a PDF resume to use with the AI Resume Checker.
              </p>
            </div>
          </div>

          <ResumeForm onResumeCreated={loadResumes} />

          <div className="resume-list">

            {resumes.map((resume) => (
              <div className="resume-item" key={resume.id}>

                <div className="resume-icon">
                  📄
                </div>

                <div>
                  <strong>{resume.fileName}</strong>

                  <span>
                    Uploaded resume
                  </span>
                </div>

              </div>
            ))}

          </div>

        </section>

        {/* AI Resume Checker */}

        <section className="ai-section">

          <div className="ai-header">

            <div>
              <span className="ai-badge">
                ✨ AI POWERED
              </span>

              <h2>AI Resume Checker</h2>

              <p>
                Compare your resume against a job description
                and discover your strengths and skill gaps.
              </p>
            </div>

          </div>

          <div className="ai-form-container">

            <JobAnalysisForm
              resumes={resumes}
              onAnalysisCreated={(analysis) => {
                setLatestAnalysis(analysis)

                setAnalyses((previousAnalyses) => [
                  analysis,
                  ...previousAnalyses
                ])
              }}
            />

          </div>

        </section>

        {/* Latest Analysis */}

        {latestAnalysis && (
          <section className="section-card">

            <div className="section-header">
              <div>
                <p className="section-label">AI RESULTS</p>
                <h2>Latest Analysis</h2>
              </div>
            </div>

            <AnalysisResult
              analysis={latestAnalysis}
            />

          </section>
        )}

        {/* Analysis History */}

        <section className="section-card">

          <AnalysisHistory
            analyses={analyses}
          />

        </section>

      </main>

      <footer className="footer">
        <p>
          JobTracker · Built with React, ASP.NET Core,
          PostgreSQL & AI
        </p>
      </footer>

    </div>
  )
}

export default App