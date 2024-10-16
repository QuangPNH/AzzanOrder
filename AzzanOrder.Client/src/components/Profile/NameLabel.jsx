import React from 'react';

function NameLabel({ name, setName }) {
    return (
        <div className="profile-label">
            <input
                type="text"
                className="profile-input"
                aria-label="Profile name"
                value={name}
                onChange={(e) => setName(e.target.value)} // Update state on change
            />
            <style jsx>{`
                .profile-label {
                    position: relative;
                    display: flex;
                    min-height: 35px;
                    align-items: center;
                    gap: 10px;
                    justify-content: start;
                    padding: 0 22px;
                    margin-bottom: 15px; /* Added bottom margin for spacing */
                }
                .profile-input {
                    border-radius: 6px;
                    min-height: 35px;
                    width: 100%;
                    border: 2px solid #000;
                    font-size: 24px;
                    font-weight: 600;
                    line-height: 1.4;
                    color: #1e1e1e;
                    padding: 5px 10px;
                    font-family: Inter, sans-serif;
                    text-align: center; /* Center the text */
                }
            `}</style>
        </div>
    );
}

export default NameLabel;
