import React from 'react';

const FeedbackTitle = () => {
    return (
        <div className="your-feedback">
            <div className="your-feedback-title">Feedback</div>
            <style jsx>{`
                .your-feedback {
                    width: 100%;
                    text-align: center;
                    margin-top: 30px; /* Add margin to create space */
                }
                .your-feedback-title {
                    color: #bd3326; /* Set color to BD3326 (100%) */
                    font: bold 32px Inter, sans-serif; /* Use Inter Bold 32 */
                    margin: 0;
                    padding-top: 20px;
                }
            `}</style>
        </div>
    );
};

export default FeedbackTitle;
