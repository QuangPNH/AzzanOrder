import React, { useState, useEffect, useRef } from "react";
import QuantityControl from "../ItemInCart/QuantityControl";

const ProductCardSingle = ({ imageSrc, name, price, onQuantityChange }) => {
    const [currentQuantity, setCurrentQuantity] = useState(1);

    const handleQuantityChange = (newQuantity) => {
        setCurrentQuantity(newQuantity);
        onQuantityChange(newQuantity); // Pass the new quantity to the parent component
    };

    return (
        <div className="product-card-container">
            <img src={imageSrc} alt={`${name} product`} className="product-image" />
            <div className="product-card">
                <div className="product-info">
                    <h2 className="product-name">{name}</h2>
                    <div className="price-quantity-container">
                        <p className="product-price">
                            <span className="price-value">{price * currentQuantity}</span>
                            <span className="price-currency">đ</span>
                        </p>
                        <QuantityControl
                            quantity={currentQuantity}
                            onQuantityChange={handleQuantityChange}
                        />
                    </div>
                </div>
            </div>
            <style jsx>{`
                .product-card-container {
                    display: flex;
                    flex-direction: column;
                    align-items: center; /* Center the card container */
                    max-width: 330px; /* Limit the max width of the product card */
                    margin: 0 auto; /* Center the card in the page */
                    font-family: Inter, sans-serif;
                }
                .product-image {
                    aspect-ratio: 0.91;
                    object-fit: contain;
                    object-position: center;
                    width: 100%;
                    border-radius: 10px;
                }
                .product-card {
                    display: flex;
                    flex-direction: column;
                    width: 100%; /* Ensure it takes full width of the container */
                    margin-top: 14px;
                }
                .product-info {
                    display: flex;
                    flex-direction: column;
                    font-weight: 700;
                    flex: 1;
                }
                .product-name {
                    color: #000;
                    font-size: 16px;
                    align-self: start; /* Align the name to the left */
                }
                .price-quantity-container {
                    display: flex;
                    justify-content: space-between; /* Aligns price and QuantityChange */
                    align-items: center; /* Vertically centers items */
                    margin-top: 10px; /* Adjusted margin for better spacing */
                }
                .product-price {
                    color: rgba(0, 0, 0, 0.6); /* Unified price and currency color */
                    font-size: 24px;
                }
                .price-currency {
                    font-weight: 400;
                    text-decoration: underline;
                    font-size: 22px;
                    text-transform: lowercase;
                    color: inherit; /* Inherit color from .product-price */
                }
            `}</style>
        </div>
    );
};

export default ProductCardSingle;