﻿import React from 'react';

function AmountIcon({ isSelected, label, onClick }) {
    return (
        <div
            className={`amount ${isSelected ? 'amount-selected' : 'amount-unselected'}`}
            onClick={onClick}
        >
            {label}
            <style jsx>{`
                .amount {
                    display: flex; /* Use flexbox for centering */
                    align-items: center; /* Vertically center the label */
                    justify-content: center; /* Horizontally center the label */
                    padding: 7px 21px;
                    border: 1px solid rgba(0, 0, 0, 0.5);
                    cursor: pointer; /* Show a pointer cursor on hover */
                    transition: background-color 0.3s, color 0.3s; /* Smooth transition for hover effects */
                }
                .amount-unselected {
                    padding: 7px 16px;
                    background: transparent; /* Unselected background */
                    color: #000; /* Unselected color */
                }
                .amount-selected {
                    background: var(--Azzan-Color, #bd3326);
                    color: #fff;
                    font-weight: 400;
                    padding: 7px 16px;
                    border: none; /* No border for the selected step */
                }
            `}</style>
        </div>
    );
}

export default AmountIcon;
