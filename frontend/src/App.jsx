import { useState } from "react"
import { useEffect } from "react";
import Navbar from "../components/Navbar"
import ApplicationForm from "../components/ApplicationForm"
import { getCompanies } from "./api/companyApi";

function App() {
  const [companies, setCompanies] = useState([]);

  useEffect(() => {
    getCompanies()
      .then((data) => {
        setCompanies(data)
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


    </div>
  )
}

export default App
