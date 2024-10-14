import React from 'react';

const PriceTag = ({ price, onclick }) => {
    return (
        <div className="price-tag">
            <span className="price">{price}</span>
            <span className="currency">đ</span>
            <button className="add-to-cart" onClick={onclick} aria-label="Add to cart">
                <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8" alt="" className="cart-icon" />
            </button>
            <style jsx>{`
        .price-tag {
          display: flex;
          margin-top: 9px;
          gap: 20px;
          font-size: 16px;
          color: rgba(0, 0, 0, 0.6);
          justify-content: space-between;
          align-items: center;
        }
        .price {
          font-weight: 400;
          font-size: 12px;
          color: #000;
        }
        .currency {
          font-weight: 400;
          text-decoration: underline;
          font-size: 12px;
          text-transform: lowercase;
          color: #000;
        }
        .add-to-cart {
          background: none;
          border: none;
          cursor: pointer;
          padding: 0;
        }
        .cart-icon {
          aspect-ratio: 0.96;
          object-fit: contain;
          object-position: center;
          width: 23px;
        }
      `}</style>
        </div>
    );
};

export default PriceTag;