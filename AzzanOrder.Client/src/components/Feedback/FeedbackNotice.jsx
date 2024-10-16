import React from 'react';

const FeedbackNotice = () => {
    return (
        <section className="feedback-notice">
            <p className="notice-text">
                <span className="italic-text">* Each</span> user can only send one feedback.
                <br />
                <span className="italic-text">If</span> you have new feedback, please edit the form.
            </p>
            <style jsx>{`
                .feedback-notice {
                    max-width: 270px;
                }
                .notice-text {
                    color: rgba(189, 51, 38, 1);
                    font: 400 12px Inter, sans-serif;
                    font-style: italic; /* Make the whole text italic */
                }
                .italic-text {
                    display: inline-block; /* Maintain alignment */
                }
            `}</style>
        </section>
    );
};

export default FeedbackNotice;
