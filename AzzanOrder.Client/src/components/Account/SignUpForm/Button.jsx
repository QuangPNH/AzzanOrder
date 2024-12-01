import React from "react";

function Button({ text, style, disabled }) {
    // Calculate the width based on the text length
    const buttonWidth = `${30 + text.length * 10}px`;

    return (
        <>
            <button className="submit-button" style={style} disabled={disabled}>{text}</button>
            <style jsx>{`
                .submit-button {
                    align-self: center;
                    border-radius: 15px;
                    border: none;
                    background: var(--primary-color);
                    margin-top: 15px;
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
                .submit-button:disabled {
                    background: gray;
                    cursor: not-allowed;
                }
            `}</style>
        </>
    );
}

export default Button;