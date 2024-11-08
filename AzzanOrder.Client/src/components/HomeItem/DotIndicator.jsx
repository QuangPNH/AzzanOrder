import React from 'react';

const DotIndicator = ({ active, onClick }) => {
    return (
        <div className={`dot ${active ? 'active' : ''}`} onClick={onClick}>
            <style jsx>{`
                .dot {
                    background-color: #f0f9ff; /* Light color for inactive dots */
                    border-radius: 50%;
                    width: 8px; /* Smaller size for inactive dots */
                    height: 8px; /* Smaller size for inactive dots */
                    transition: background-color 0.3s, width 0.3s; /* Smooth transition for active state */
                    cursor: pointer; /* Change cursor to pointer on hover */
                }
                .active {
                    background-color: #4c5a61; /* Darker color for active dot */
                    width: 19px; /* Larger size for active dot */
                }
            `}</style>
        </div>
    );
};

export default DotIndicator;