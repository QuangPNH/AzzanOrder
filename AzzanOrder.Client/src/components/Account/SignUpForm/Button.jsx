import React from "react";

function Button({ text }) {
    // Calculate the width based on the text length
    const buttonWidth = `${30 + text.length * 10}px`;

    return (
        <>
            <button className="submit-button">{text}</button>
            <style jsx>{`
        .submit-button {
          align-self: center;
          border-radius: 15px;
          border: none;
          background: #bd3326;
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
      `}</style>
        </>
    );
}

export default Button;
