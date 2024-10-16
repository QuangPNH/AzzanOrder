import React from "react";

function PointsDisplay({ points }) {
    return (
        <>
            <div className="points-container">
                <div className="points-label">Points:</div>
                <div className="points-value">{points}</div>
            </div>
            <style jsx>{`
                .points-container {
                    display: flex;
                    justify-content: space-between; /* Space between label and value */
                    align-items: center; /* Center items vertically */
                    max-width: 328px;
                    font: 600 24px Inter, sans-serif;
                    padding: 10px; /* Add padding to avoid overlap */
                    margin-top: 20px; /* Add margin to ensure spacing from elements above */
                }
                .points-label {
                    color: #000;
                    flex-grow: 1; /* Allow the label to take available space */
                    text-align: left; /* Align label to the left */
                }
                .points-value {
                    color: #1e1e1e;
                    text-align: right; /* Align value to the right */
                    min-width: 100px; /* Optional: set a minimum width for consistent spacing */
                }
            `}</style>
        </>
    );
}

export default PointsDisplay;
