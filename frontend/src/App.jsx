import { useState } from "react"
import { useEffect } from "react";
import Navbar from "../components/Navbar"
import ApplicationForm from "../components/ApplicationForm"
import { getCompanies } from "./api/companyApi";
import { getJobApplications } from "./api/jobApplicationApi";
import ApplicationList from "../components/ApplicationList";

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

      <ApplicationList applications={applications} />

    </div>
  )
}

export default App
