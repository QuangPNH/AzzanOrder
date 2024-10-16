import React from "react";

const statusMapping = {
    1: { label: "Not Processed", color: "rgba(255, 0, 0, 0.8)" }, // Red
    2: { label: "Processing", color: "rgba(255, 255, 0, 0.8)" }, // Yellow
    3: { label: "Done", color: "rgba(0, 255, 0, 0.8)" } // Green
};

const OrderStatus = ({ status }) => {
    const statusInfo = statusMapping[status] || { label: "Unknown", color: "rgba(255, 255, 255, 0.8)" }; // Fallback color

    return (
        <>
            <div className="status-container">
                <div className="status-indicator" style={{ backgroundColor: statusInfo.color }}>
                    {statusInfo.label}
                </div>
            </div>
            <style jsx>{`
                .status-container {
                    border-radius: 100px;
                    display: flex;
                    max-width: 98px; /* Keep max-width for the container */
                    flex-direction: column;
                    color: #fff;
                    white-space: nowrap;
                    font: 400 10px/1.4 Inter, sans-serif;
                    align-items: center; /* Center items vertically */
                }
                .status-indicator {
                    text-shadow: 0 2px 2px rgba(0, 0, 0, 0.25);
                    border-radius: 100px;
                    padding: 1px;
                    width: 98px; /* Set a fixed width for the box */
                    height: 16px; /* Set the height of the box to 16px */   
                    display: flex; /* Enable flexbox for the indicator */
                    align-items: center; /* Center text vertically within the indicator */
                    justify-content: center; /* Center text horizontally within the indicator */
                }
            `}</style>
        </>
    );
};

export default OrderStatus;
