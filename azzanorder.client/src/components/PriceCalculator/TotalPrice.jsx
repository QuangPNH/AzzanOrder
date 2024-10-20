import React, { useState, useEffect } from "react";

const fetchVoucherDetails = async () => {
    try {
        const response = await fetch("https://localhost:7183/api/VoucherDetail");
        if (!response.ok) {
            throw new Error("Network response was not ok");
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error("Failed to fetch voucher details:", error);
        return [];
    }
};

const TotalPrice = ({ finalPrice, discountPrice }) => {
    const [voucherDetails, setVoucherDetails] = useState([]);
    const [isExpanded, setIsExpanded] = useState(false);

    const handleVoucherClick = async () => {
        const details = await fetchVoucherDetails();
        setVoucherDetails(details);
        setIsExpanded(details.length > 0);
    };

    const renderVoucherDetails = () => {
        if (voucherDetails.length === 0) {
            return <p>There are currently no vouchers</p>;
        }

        return (
            <div className="voucher-details">
                {voucherDetails.map((detail, index) => (
                    <p key={index}>{detail.title}</p>
                ))}
            </div>
        );
    };

    return (
        <div className="total-price-container">
            <button className="voucher-input" onClick={handleVoucherClick}>
                <img
                    src="https://cdn.builder.io/api/v1/image/assets/TEMP/170034c8ccaf3722be922eaa999b1f02f19585d5b327ad61ff50748511ec2953?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8"
                    alt=""
                    className="voucher-icon"
                />
            </button>
            {isExpanded && renderVoucherDetails()}
            <div className="price-values">
                <p className="discount-price">{discountPrice} đ</p>
                <p className="final-price">{finalPrice} đ</p>
                <img
                    src="https://cdn.builder.io/api/v1/image/assets/TEMP/3daa88c596f12764a69508098612d6d3ba5839873c031a93b1ac416fd8d5c0b9?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8"
                    alt=""
                    className="payment-icon"
                />
            </div>
            <style jsx>{`
                .total-price-container {
                    display: flex;
                    flex-direction: column;
                }
                .voucher-input {
                    border-radius: 5px;
                    background-color: #fff;
                    display: flex;
                    justify-content: center;
                    padding: 2px 32px;
                    border: 1px solid #bd3326;
                }
                .voucher-icon {
                    aspect-ratio: 1.1;
                    object-fit: contain;
                    width: 22px;
                }
                .voucher-details {
                    margin-top: 11px;
                    padding-left: 32px;
                    color: var(--Azzan-Color, #bd3326);
                    text-align: right;
                    font: 600 16px Inter, sans-serif;
                }
                .price-values {
                    display: flex;
                    margin-top: 11px;
                    padding-left: 32px;
                    flex-direction: column;
                    align-items: flex-end;
                    color: var(--Azzan-Color, #bd3326);
                    text-align: right;
                    font: 600 16px Inter, sans-serif;
                }
                .discount-price,
                .final-price {
                    text-decoration: underline;
                }
                .final-price {
                    margin-top: 16px;
                }
                .payment-icon {
                    aspect-ratio: 1;
                    object-fit: contain;
                    width: 30px;
                    margin-top: 8px;
                }
            `}</style>
        </div>
    );
};

export default TotalPrice;