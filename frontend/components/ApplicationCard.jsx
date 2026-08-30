import { updateJobApplications, deleteJobApplications } from "../src/api/jobApplicationApi"

function ApplicationCard({ application, onApplicationDeleted, onApplicationEdit }) {

    const handleDelete = async () => {
        try {
            await deleteJobApplications(application.id);

            console.log("Application deleted");

            onApplicationDeleted();
        } catch (error) {
            console.log(error)
            alert("Failed to delete application")
        }
    }

    return (
        <div className="application-card">
            <h3>{application.jobTitle}</h3>

            <p>Company: {application.companyName}</p>

            <p>
                Status: <span className={`status ${application.status.toLowerCase()}`}>
                    {application.status}
                </span>
            </p>

            <p>Applied: {application.appliedDate}</p>

            <p>Location: {application.location || "Not specified"}</p>

            {application.salary && (
                <p>Salary: ${application.salary}</p>
            )}

            {application.jobUrl && (
                <p>
                    <a
                        href={application.jobUrl}
                        target="_blank"
                        rel="noopener noreferrer"
                    >
                        View Job Posting
                    </a>
                </p>
            )}
            <button onClick={() => onApplicationEdit(application)}>
                Edit
            </button>
            <button onClick={handleDelete}>
                Delete
            </button>
        </div>
    )
}

export default ApplicationCard