import React, { useState } from 'react';

function MenuItem({ imageSrc, name, description, price }) {

    return (
        <>
            <article className="menu-item">
                <img loading="lazy" src={imageSrc} alt={name} className="item-image"/>
                <div className="item-details">
                    <h2 className="item-name">{name}</h2>
                    <p className="item-description">{description}</p>
                    <button className="add-item" aria-label="Add item">+</button>
                    <div className="price-container">
                        <span className="item-price">{price}</span>
                        <div className="price-indicator" aria-hidden="true" />
                    </div>
                </div>
            </article>
            <hr className="item-separator" />
            
            <style jsx>{`
        .menu-item {
          z-index: 10;
          display: flex;
          gap: 20px;
        }
        .item-image {
          aspect-ratio: 1.04;
          object-fit: contain;
          object-position: center;
          width: 88px;
          border-radius: 50%;
        }
        .item-details {
          align-self: start;
          display: flex;
          flex-direction: column;
          margin-left: auto;
        }
        .item-name {
          font-size: 20px;
          align-self: start;
        }
        .item-description {
          align-self: start;
          margin-top: 13px;
        }
        .add-item {
          color: #fff;
          font-size: 32px;
          font-weight: 700;
          align-self: end;
          z-index: 10;
          margin-top: 8px;
          background: none;
          border: none;
          cursor: pointer;
        }
        .price-container {
          display: flex;
          gap: 20px;
          color: #d50000;
          justify-content: space-between;
        }
        .item-price {
          align-self: start;
          margin-top: 14px;
        }
        .price-indicator {
          background-color: var(--primary-color);
          border-radius: 50%;
          width: 25px;
          height: 25px;
        }
        .item-separator {
          align-self: center;
          margin-top: 11px;
          width: 292px;
          max-width: 100%;
          height: 1px;
          border: 1px solid rgba(167, 84, 84, 0.5);
        }
      `}</style>
        </>
    );
}

export default MenuItem;