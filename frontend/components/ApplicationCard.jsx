import { deleteJobApplications } from "../src/api/jobApplicationApi"

function ApplicationCard({
  application,
  onApplicationDeleted,
  onApplicationEdit
}) {
  const handleDelete = async () => {
    try {
      await deleteJobApplications(application.id)

      console.log("Application deleted")

      onApplicationDeleted()
    } catch (error) {
      console.log(error)
      alert("Failed to delete application")
    }
  }

  return (
    <div className="application-card">

      <div className="application-card-top">
        <div>
          <h3>{application.jobTitle}</h3>

          <p className="application-company">
            {application.companyName}
          </p>
        </div>

        <span
          className={`status ${application.status.toLowerCase()}`}
        >
          {application.status}
        </span>
      </div>

      <div className="application-details">

        <div className="application-detail">
          <span className="detail-label">Applied</span>
          <span>{application.appliedDate}</span>
        </div>

        <div className="application-detail">
          <span className="detail-label">Location</span>
          <span>
            {application.location || "Not specified"}
          </span>
        </div>

        {application.salary && (
          <div className="application-detail">
            <span className="detail-label">Salary</span>
            <span>${application.salary}</span>
          </div>
        )}

      </div>

      <div className="application-card-bottom">

        {application.jobUrl ? (
          <a
            href={application.jobUrl}
            target="_blank"
            rel="noopener noreferrer"
            className="job-link"
          >
            View Job Posting →
          </a>
        ) : (
          <span></span>
        )}

        <div className="application-actions">

          <button
            className="edit-button"
            onClick={() => onApplicationEdit(application)}
          >
            Edit
          </button>

          <button
            className="delete-button"
            onClick={handleDelete}
          >
            Delete
          </button>

        </div>

      </div>

    </div>
  )
}

export default ApplicationCard