import React from 'react';

const AddToCartButton = ({ onClick  }) => {
    return (
        <div className="button-container">
            <button className="add-to-cart-button" onClick={onClick}>
                <img
                    loading="lazy"
                    src="https://cdn.builder.io/api/v1/image/assets/TEMP/2ec0000c0c17b1641a0155e40b8e96b2e7f5b2f471eb8d27f08cc4a1b3f1b8af?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                    alt=""
                    className="cart-icon"
                />
                <span className="button-text">Add to cart</span>
            </button>
            <style jsx>{`
                .button-container {
                    display: flex;
                    justify-content: center; /* Center horizontally */
                }
                .add-to-cart-button {
                    display: flex;
                    align-items: center; /* Center vertically within button */
                    justify-content: center; /* Center horizontally within button */
                    padding: 11px 16px; /* Padding for top/bottom and left/right */
                    border-radius: 8px; /* 8px curve border */
                    background-color: var(--primary-color); /* Button color */
                    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.25); /* Shadow drop */
                    color: white; /* Text color */
                    font: 400 16px 'Inter', sans-serif; /* Font */
                    border: none;
                    cursor: pointer;
                    width: 100%; /* Full width */
                    max-width: 300px; /* Optional: Limit max width of button */
                    margin: 0 auto; /* Center button within container */
                }
                .cart-icon {
                    cursor: pointer;
                    width: 24px;
                    height: 24px;
                    object-fit: contain;
                    margin-right: 10px; /* Space between icon and text */
                }
                .button-text {
                    text-align: center; /* Center text */
                }
            `}</style>
        </div>
    );
};

export default AddToCartButton;
