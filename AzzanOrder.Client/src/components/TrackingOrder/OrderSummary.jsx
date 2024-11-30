import React from 'react';

const OrderSummary = () => {
    return (
        <div className="your-order">
            <div className="your-order-title">Your Order</div>
            <style jsx>{`
                .your-order {
                    width: 100%;
                    text-align: center;
                    margin-top: 20px; /* Add margin to create space */
                }
                .your-order-title {
                    color: var(--primary-color); /* Set color to BD3326 (100%) */
                    font: bold 32px Inter, sans-serif; /* Use Inter Bold 32 */
                    margin: 0;
                    padding-top: 20px;
                }
            `}</style>
        </div>
    );
};

export default OrderSummary;
