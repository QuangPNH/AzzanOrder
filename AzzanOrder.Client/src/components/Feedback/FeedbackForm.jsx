import React, { useState, useEffect } from 'react';

const FeedbackTextbox = ({ data, onChange }) => {
    const [value, setValue] = useState(data);

    useEffect(() => {
        setValue(data);
    }, [data]);

    const handleChange = (e) => {
        setValue(e.target.value);
        onChange(e.target.value);
    };

    return (
        <>
            <section className="feedback-container">
                <textarea
                    className="feedback-input"
                    value={value} // Set the textarea value to the feedback state
                    onChange={handleChange} // Handle changes to the textarea
                    placeholder="Write your feedback here..."
                    aria-label="Feedback input"
                />
            </section>
            <style jsx>{`
                .feedback-container {
                    width: calc(100% - 20px); /* Full width with spacing */
                    max-width: 328px; /* Optional max-width */
                    padding: 0 10px; /* Add spacing on the sides */
                    margin: 0 auto; /* Center the container */
                }
                .feedback-input {
                    width: 100%;
                    height: 328px;
                    padding: 5px 9px;
                    border: 2px solid rgba(85, 52, 52, 0.35);
                    border-radius: 2px;
                    resize: none;
                    font: 400 12px 'Inter', sans-serif; /* Set font to Inter with size 12 */
                }
                .feedback-input::placeholder {
                    color: rgba(0, 0, 0, 0.67);
                }
            `}</style>
        </>
    );
};

export default FeedbackTextbox;
