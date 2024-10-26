import React from 'react';

const CustomItem = ({ title, compact }) => {
    return (
        <section className={`custom-item-container ${compact ? 'compact' : ''}`}>
            <div className={`custom-item-title ${compact ? 'compact' : ''}`}>{title}</div> {/* Changed to div */}
            <style jsx>{`
                .custom-item-container {
                    padding: 20px;
                    border-radius: 8px;
                }
                .custom-item-title {
                    font-size: 16px; /* Adjusted font size */
                    color: #333;
                    margin: 0;
                    font-family: 'Inter', sans-serif; /* Ensure the font is Inter */
                }
                .compact{
                    padding: 0px; !important;
                    font-size: 10px; !important;
                }
            `}</style>
        </section>
    );
};

export default CustomItem;
