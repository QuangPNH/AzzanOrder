import React, { useState, useEffect } from 'react';

const ToppingAdd = ({ toppingName, toppingNameEnglish, toppingPrice, onToppingChange }) => {
    const [isChecked, setIsChecked] = useState(false);

    const handleCheckboxChange = () => {
        const updatedChecked = !isChecked;
        setIsChecked(updatedChecked);

        const topping = { name: toppingName, price: toppingPrice };
        onToppingChange(topping, updatedChecked);
    };

    return (
        <>
            <div className="item-container">
                <div className="item-details">
                    <div className="item-name">
                        {toppingName}/<span className="item-name-english">{toppingNameEnglish}</span>
                    </div>
                    <div className="item-price">
                        + <span className="price-value">{toppingPrice.toLocaleString()} </span>
                        <span className="price-currency">đ</span>
                    </div>
                </div>
                <input
                    type="checkbox"
                    className="topping-checkbox"
                    checked={isChecked}
                    onChange={handleCheckboxChange}
                />
            </div>
            <div className="item-divider" />
            <style jsx>{`
                .item-container {
                    display: flex;
                    width: 100%;
                    justify-content: space-between;
                    font-family: Inter, sans-serif;
                    max-width: 328px;
                }
                .item-details {
                    display: flex;
                    flex-direction: column;
                    font-size: 12px;
                    color: rgba(0, 0, 0, 0.8);
                    font-weight: 300;
                }
                .item-name-english {
                    font-size: 8px;
                }
                .item-price {
                    align-self: start;
                    margin-top: 4px;
                }
                .price-value, .price-currency {
                    font-weight: 500;
                }
                .price-currency {
                    text-decoration: underline;
                }
                .item-divider {
                    min-height: 1px;
                    margin-top: 12px;
                    width: 100%; /* Make sure the width is 100% of the parent */
                    border: none;
                    border-top: 1px solid rgba(0, 0, 0, 0.4);
                    box-sizing: border-box; /* Include padding and border in element's total width and height */
                }
                .topping-checkbox {
                    align-self: center;
                }
            `}</style>
        </>
    );
};

export default ToppingAdd;