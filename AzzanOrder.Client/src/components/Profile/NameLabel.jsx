import React, { useState, useEffect } from "react";
import ProfileItem from './ProfileItem';

function NameLabel({ name, onChange }) {
    const [value, setValue] = useState(name);

    useEffect(() => {
        setValue(name);
    }, [name]);

    const handleChange = (e) => {
        setValue(e.target.value);
        onChange(e.target.value);
    };

    return (
        <div className="profile-label">
            <ProfileItem title='Full Name' />
            <input
                type="text"
                className="profile-input"
                aria-label="Profile name"
                value={value}
                onChange={handleChange}
            />
            <style jsx>{`
                .profile-label {
                    display: flex;
                    width: calc(100% - 30px); /* Takes up full width of the parent minus padding */
                    gap: 9px;
                    font-family: Inter, sans-serif;
                    color: #000;
                    border-radius: 6px;
                    align-items: center; /* Aligns items vertically at the center */
                    margin-bottom: 5px; /* Added margin for spacing between DetailInfo components */
                }
                .profile-label > :first-child {
                    flex: 0 0 30%; /* ProfileItem takes up 30% of the width */
                }
                .profile-label > :last-child {
                    flex: 0 0 70%; /* Input takes up 70% of the width */
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