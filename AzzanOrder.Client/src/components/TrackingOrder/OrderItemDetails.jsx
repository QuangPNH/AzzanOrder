import React from 'react';
import OrderStatus from './OrderStatus'; // Adjust the import path as needed

const OrderItemDetails = ({ imageSrc, title, details, status }) => {
    return (
        <div className="order-extras">
            <img loading="lazy" src={imageSrc} alt={title} className="order-image" />
            <div className="order-info">
                <h3 className="order-name">{title}</h3>
                {details.length > 0 ? (
                    details.map((detail, index) => (
                        detail !== null && detail !== "" && ( // Check if detail is not null or empty
                            <p key={index} className="order-extra">{detail}</p>
                        )
                    ))
                ) : (
                    <p className="order-extra empty-detail">No details available</p> // Placeholder for spacing
                )}
                <OrderStatus status={status} />
            </div>
            <style jsx>{`
                .order-extras {
                    align-self: start;
                    display: flex;
                    margin-left: 15px;
                    align-items: center; /* Align items vertically centered */
                    gap: 23px;
                    color: rgba(30, 30, 30, 0.6);
                    font: 400 12px/1.4 Inter, sans-serif;
                }
                .order-image {
                    aspect-ratio: 1;
                    object-fit: contain;
                    object-position: center;
                    width: 88px;
                    border-radius: 100px;
                }
                .order-info {
                    display: flex;
                    flex-direction: column;
                }
                .order-name {
                    color: var(--sds-color-text-default-default);
                    align-self: start;
                    font: 700 16px/1.4 Inter, sans-serif; /* Updated font size for title */
                }
                .order-extra {
                    margin-top: 2px; /* Reduced margin for details */
                    font: 400 12px/1.4 Inter, sans-serif; /* Font size for details */
                }
                .empty-detail {
                    height: 20px; /* Placeholder height to maintain spacing */
                    margin: 4px 0; /* Maintain margin for empty detail */
                }
            `}</style>
        </div>
    );
};

export default OrderItemDetails;
