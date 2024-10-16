import React from 'react';

const FeedbackComponent = () => {
    return (
        <section className="feedback-section">
            <p className="feedback-description">
                Send us feedback that can help us improve our shop!
            </p>
            <style jsx>{`
                .feedback-section {
                    display: flex; /* Use flexbox */
                    justify-content: center; /* Center horizontally */
                    align-items: center; /* Center vertically (if needed) */
                    height: 100%; /* Ensure it takes full height of the parent */
                }
                .feedback-description {
                    width: 100%;
                    max-width: 193px;
                    color: #000;
                    text-align: center;
                    font: 400 13px Inter, sans-serif;
                }
            `}</style>
        </section>
    );
};

export default FeedbackComponent;
