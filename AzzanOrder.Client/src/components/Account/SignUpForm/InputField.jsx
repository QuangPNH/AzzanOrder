import React from "react";

function InputField({ icon, placeholder, value, onChange }) {
    return (
        <>
            <div className="input-container">
                <img src={icon} alt="" className="input-icon" />
                <input
                    onChange={onChange}
                    value={value}
                    type="tel"
                    placeholder={placeholder}
                    className="input-field"
                    aria-label={placeholder}
                />
            </div>
            <style jsx>{`
        .input-container {
          border-radius: 6px;
          align-self: stretch;
          display: flex;
          margin-top: 15px;
          gap: 7px;
          font-size: 16px;
          font-weight: 400;
          padding: 8px 9px;
          border: 2px solid #000;
        }
        .input-icon {
          aspect-ratio: 1;
          object-fit: contain;
          object-position: center;
          width: 24px;
        }
        .input-field {
          align-self: start;
          flex-grow: 1;
          width: 229px;
          border: none;
          outline: none;
          font-size: 16px;
        }
      `}</style>
        </>
    );
}

export default InputField;