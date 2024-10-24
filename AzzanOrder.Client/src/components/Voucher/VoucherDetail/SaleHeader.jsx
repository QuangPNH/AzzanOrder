﻿import React from 'react';

const SaleHeader = ({category, endDate }) => {
    return (
        <div className="sale-header">
            <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/46e20667afa9d08fbd38cb9448d5e29e3e5cd42b24c7ba3407f78bffb5babf1d?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533" alt="Sale banner" className="sale-banner" />
            <div className="sale-title">{category}</div>
            <div className="sale-end-date">
                <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/c567125b1e63313ad4d2fcb6e291b8566bafd451b0848750569e09708aeea750?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533" alt="Calendar icon" className="calendar-icon" />
                <p>End: {endDate}</p> {/* Dynamic End Date */}
            </div>
            <style jsx>{`
                .sale-header {
                    border-radius: 10px 0 0 10px;
                    background-color: rgba(191, 50, 39, 0.75);
                    box-shadow: 0 4px 4px rgba(0, 0, 0, 0.25);
                    display: flex;
                    flex-direction: column;
                    overflow: hidden;
                    font-family: Inter, sans-serif;
                    color: #fff;
                    font-weight: 600;
                    text-align: center;
                    padding: 9px 8px;
                }
                .sale-banner {
                    aspect-ratio: 4.44;
                    object-fit: contain;
                    width: 71px;
                }
                .sale-title {
                    font-size: 14px;
                    align-self: flex-start;
                    margin-top: 6px;
                }
                .sale-end-date {
                    display: flex;
                    margin-top: 8px;
                    font-size: 10px;
                }
                .calendar-icon {
                    aspect-ratio: 1;
                    object-fit: contain;
                    width: 12px;
                    margin-right: 4px;
                }
            `}</style>
        </div>
    );
};

export default SaleHeader;
