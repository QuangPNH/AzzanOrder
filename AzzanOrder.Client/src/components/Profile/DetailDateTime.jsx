import React, { useState, useEffect } from "react";
import ProfileItem from "./ProfileItem"; // Assuming ProfileItem is in the same folder

const DetailDateTime = ({ title, memberDetail, onChange }) => {
    const [value, setValue] = useState(memberDetail);

    useEffect(() => {
        setValue(memberDetail);
    }, [memberDetail]);

    const handleChange = (e) => {
        setValue(e.target.value);
        onChange(e.target.value);
    };

    return (
        <>
            <section className="detail-info">
                <ProfileItem title={title} />
                <input
                    className="member-detail"
                    type="date"
                    value={value}
                    onChange={handleChange}
                />
            </section>
            <style jsx>{`
                .detail-info {
                    display: flex;
                    width: 100%; /* Takes up full width of the parent */
                    gap: 9px;
                    font-family: Inter, sans-serif;
                    color: #000;
                    border-radius: 6px;
                    align-items: flex-start; /* Aligns items vertically at the start */
                    margin-bottom: 5px; /* Added margin for spacing between DetailInfo components */
                }
                .member-detail {
                    font-size: 14px;
                    font-weight: 400;
                    white-space: nowrap;
                    padding: 8px 12px; /* Increase padding to make input bigger */
                    border: 1px solid #000;
                    border-radius: 6px;
                    margin: 0;
                    flex-grow: 1; /* Takes up remaining space */
                    width: 100%; /* Ensures it stretches to full width */
                    min-width: 200px; /* Increase minimum width */
                }
            `}</style>
        </>
    );
};

export default DetailDateTime;
