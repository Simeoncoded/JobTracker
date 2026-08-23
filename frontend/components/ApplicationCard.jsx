function ApplicationCard({ application }) {
    return (
        <div>
            <h3>{application.jobTitle}</h3>

            <p>Company: {application.companyName}</p>

            <p>Status: {application.status}</p>

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
        </div>
    )
}

export default ApplicationCard