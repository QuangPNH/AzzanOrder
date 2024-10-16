import React from 'react';
import OrderItemDetails from './OrderItemDetails';
import Separator from './Separator';

const OrderItem = ({ imageSrc, title, details, status }) => {
    return (
        <div className="order-item">
            <OrderItemDetails
                imageSrc={imageSrc}
                title={title}
                details={details}
                status={status}
            />
            <Separator />
            <style jsx>{`
                .order-item {
                    border-radius: 0;
                    display: flex;
                    max-width: 360px;
                    flex-direction: column;
                }
            `}</style>
        </div>
    );
};

export default OrderItem;
