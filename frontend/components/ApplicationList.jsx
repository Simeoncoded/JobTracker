import ApplicationCard from "./ApplicationCard"

function ApplicationList({
  applications,
  onApplicationDeleted,
  onApplicationEdit
}) {
  return (
    <div className="application-list">
      <div className="application-list-header">
        <h2>Applications</h2>

        <span className="count-badge">
          {applications.length}
        </span>
      </div>

      <div className="application-list-items">
        {applications.map((application) => (
          <ApplicationCard
            key={application.id}
            application={application}
            onApplicationDeleted={onApplicationDeleted}
            onApplicationEdit={onApplicationEdit}
          />
        ))}
      </div>
    </div>
  )
}

export default ApplicationList