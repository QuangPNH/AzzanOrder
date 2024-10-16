import React from 'react';

const VoucherList = () => {
    return (
        <div className="voucher-list">
            <div>VOUCHER LIST</div>
            <style jsx>{`
                .voucher-list {
                    color: #ff0000;
                    padding: 0;
                    font: 700 24px Inter, sans-serif;
                    display: flex; /* Enable flex display */
                    justify-content: center; /* Center the content */
                    align-items: center; /* Center vertically */
                    width: 100%; /* Ensure it takes full width */
                }
            `}</style>
        </div>
    );
};

export default VoucherList;
