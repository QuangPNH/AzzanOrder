import React, { useState } from "react";

const QuantityChange = () => {
    const [quantity, setQuantity] = useState(0);

    const handleIncrease = () => {
        setQuantity((prev) => prev + 1);
    };

    const handleDecrease = () => {
        setQuantity((prev) => (prev > 0 ? prev - 1 : 0)); // Prevent negative quantity
    };

    return (
        <div className="quantity-control">
            <button className="quantity-button" aria-label="Decrease quantity" onClick={handleDecrease}>
                <img
                    src="https://cdn.builder.io/api/v1/image/assets/TEMP/dcedeee007db2ccbb191aac8273334f72e1603b3f37a3b60ccad5ae201c40106?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8"
                    alt=""
                    className="quantity-icon"
                />
            </button>
            <span className="quantity-value">{quantity}</span>
            <button className="quantity-button" aria-label="Increase quantity" onClick={handleIncrease}>
                <img
                    src="https://cdn.builder.io/api/v1/image/assets/TEMP/eb7d7218614da6fb584fc6f918571ecf71dd611b4cf52ca9b3cf01270ab4d6a9?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8"
                    alt=""
                    className="quantity-icon"
                />
            </button>
            <style jsx>{`
        .quantity-control {
          display: flex;
          align-items: center;
          gap: 9px;
          color: rgba(0, 0, 0, 1);
          white-space: nowrap;
          text-align: center;
          justify-content: start;
          font: 400 16px Inter, sans-serif;
        }
        .quantity-button {
          background: none;
          border: none;
          cursor: pointer;
          padding: 0;
        }
        .quantity-icon {
          aspect-ratio: 1;
          object-fit: contain;
          object-position: center;
          width: 24px;
        }
        .quantity-value {
          align-self: stretch;
          min-height: 24px;
          width: 24px;
          margin: auto 0;
          padding: 3px 1px;
          border: 1px solid rgba(0, 0, 0, 0.5);
        }
      `}</style>
        </div>
    );
};

export default QuantityChange;
