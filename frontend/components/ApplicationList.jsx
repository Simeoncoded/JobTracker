import ApplicationCard from "./ApplicationCard"

function ApplicationList({ applications }){
    return (
        <div>
      <h2>Applications</h2>

      <ul>
        {applications.map((application) => (
          <ApplicationCard key={application.id}
          application={application}/>
        ))}
      </ul>
    </div>
    )
}

export default ApplicationList