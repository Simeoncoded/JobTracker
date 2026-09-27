function AnalysisHistory({ analyses }) {
    if (analyses.length === 0) {
      return <p>No analyses yet.</p>
    }
  
    return (
      <div>
        <h2>Analysis History</h2>
  
        {analyses.map((analysis) => (
          <div key={analysis.id}>
            <h3>{analysis.jobTitle}</h3>
  
            <p>
              Match Score: {analysis.matchScore}%
            </p>
  
            <p>
              Created:{" "}
              {new Date(analysis.createdAt).toLocaleDateString()}
            </p>
          </div>
        ))}
      </div>
    )
  }
  
  export default AnalysisHistory