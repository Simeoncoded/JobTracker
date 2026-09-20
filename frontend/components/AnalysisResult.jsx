function AnalysisResult({ analysis }) {
    if (!analysis) {
      return null
    }
  
    return (
      <div className="analysis-card">
  
        <h2>{analysis.jobTitle}</h2>
  
        <div className="score-section">
          <h3>Match Score</h3>
          <div className="score-circle">
            {analysis.matchScore}%
          </div>
        </div>
  
        <div className="skills-section">
          <h3>Matching Skills</h3>
  
          <div className="skills-container">
            {analysis.matchingSkills?.map((skill, index) => (
              <span key={index} className="skill-badge matching">
                {skill}
              </span>
            ))}
          </div>
        </div>
  
        <div className="skills-section">
          <h3>Missing / Weak Skills</h3>
  
          <div className="skills-container">
            {analysis.missingSkills?.map((skill, index) => (
              <span key={index} className="skill-badge missing">
                {skill}
              </span>
            ))}
          </div>
        </div>
  
        <div className="recommendation-section">
          <h3>Recommendation</h3>
  
          <p>
            {analysis.recommendation}
          </p>
        </div>
  
      </div>
    )
  }
  
  export default AnalysisResult