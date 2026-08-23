import { useState } from "react"
import { useEffect } from "react";
import Navbar from "../components/Navbar"
import ApplicationForm from "../components/ApplicationForm"
import { getCompanies } from "./api/companyApi";
import { getJobApplications } from "./api/jobApplicationApi";

function App() {
  const [companies, setCompanies] = useState([]);
  const [applications, setApplications] = useState([]);

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

  return (
    <div>
      <Navbar title="JobTracker" />

      <ApplicationForm />

      <h2>Companies</h2>

      <ul>
        {companies.map((company) => (
          <li key={company.id}>
            {company.name}
          </li>
        ))}
      </ul>

      <h2>Applications</h2>

      <ul>
        {applications.map((application) => (
          <li key={application.id}>
            {application.jobTitle} - {application.companyName}
          </li>
        ))}
      </ul>

    </div>
  )
}

export default App
