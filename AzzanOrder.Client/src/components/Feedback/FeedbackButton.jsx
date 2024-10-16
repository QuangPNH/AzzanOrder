﻿import React from "react";

function FeedbackButton({ text }) {
    // Calculate the width based on the text length
    const buttonWidth = `${30 + text.length * 10}px`;

    return (
        <>
            <div className="button-container"> {/* Added a container for the button */}
                <button className="submit-button">{text}</button>
            </div>
            <style jsx>{`
                .button-container {
                    display: flex;
                    justify-content: center; /* Center the button */
                    width: 100%; /* Full width to center properly */
                    margin-top: 15px; /* Add spacing above */
                }
                .submit-button {
                    border-radius: 15px;
                    border: none;
                    background: #bd3326;
                    min-height: 35px;
                    width: ${buttonWidth}; /* Dynamic width based on text */
                    padding: 5px;
                    color: #fff;
                    font-size: 16px;
                    font-weight: 20;
                    text-align: center;
                    cursor: pointer;
                    transition: background-color 0.3s ease-in-out;
                }
            `}</style>
        </>
    );
}

export default FeedbackButton;
