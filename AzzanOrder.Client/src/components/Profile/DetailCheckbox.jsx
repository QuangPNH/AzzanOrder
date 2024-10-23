import React from "react";
import ProfileItem from "./ProfileItem"; // Assuming ProfileItem is in the same folder

const DetailCheckbox = ({ title, memberDetail, onChange }) => {
    const handleCheckboxChange = (event) => {
        const { value } = event.target;
        onChange(value === "true" ? true : value === "false" ? false : null);
    };

    return (
        <>
            <section className="detail-info">
                <ProfileItem title={title} />
                <div className="checkbox-group">
                    <label>
                        <input
                            className="member-detail"
                            type="checkbox"
                            value="true"
                            checked={memberDetail === true}
                            onChange={handleCheckboxChange}
                        />
                        Male
                    </label>
                    <label>
                        <input
                            className="member-detail"
                            type="checkbox"
                            value="false"
                            checked={memberDetail === false}
                            onChange={handleCheckboxChange}
                        />
                        Female
                    </label>
                    <label>
                        <input
                            className="member-detail"
                            type="checkbox"
                            value="null"
                            checked={memberDetail === null}
                            onChange={handleCheckboxChange}
                        />
                        Other
                    </label>
                </div>
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
                .checkbox-group {
                    display: flex;
                    gap: 10px;
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

export default DetailCheckbox;