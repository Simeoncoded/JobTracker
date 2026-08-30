import ApplicationCard from "./ApplicationCard"

function ApplicationList({ applications, onApplicationDeleted, onApplicationEdit }){
    return (
        <div>
      <h2>Applications</h2>

      <ul>
        {applications.map((application) => (
          <ApplicationCard key={application.id}
          application={application}
          onApplicationDeleted={onApplicationDeleted}
          onApplicationEdit={onApplicationEdit}/>
        ))}
      </ul>
    </div>
    )
}

export default ApplicationList