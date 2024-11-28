import React from "react";

function ProfileButton({ text, onClick }) {
    // Calculate the width based on the text length
    const buttonWidth = `${30 + text.length * 10}px`;

    return (
        <>
            <div className="button-container"> {/* Added a container for the button */}
                <button className="submit-button" onClick={onClick}>{text}</button>
            </div>
            <style jsx>{`
                .button-container {
                    display: flex;
                    justify-content: center; /* Center the button */
                    width: 100%; /* Full width to center properly */
                    margin-top: 15px; /* Add spacing above */
                    margin-bottom: 15px; /* Add spacing below */
                }
                .submit-button {
                    border-radius: 15px;
                    border: none;
                    background: var(--primary-color);
                    min-height: 35px;
                    width: ${buttonWidth}; /* Dynamic width based on text */
                    padding: 5px;
                    color: #fff;
                    font-size: 16px;
                    font-weight: 600; /* Changed font-weight to a valid value */
                    text-align: center;
                    cursor: pointer;
                    transition: background-color 0.3s ease-in-out;
                    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25); /* Added shadow */
                }
            `}</style>
        </>
    );
}

export default ProfileButton;
