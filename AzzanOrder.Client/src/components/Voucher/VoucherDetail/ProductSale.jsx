import React, { useState, useEffect } from 'react';
import SaleHeader from './SaleHeader';
import SaleContent from './SaleContent';
import Category from '../Category';

// Utility function to format date to MM/DD
const formatDate = (dateString) => {
    const date = new Date(dateString);
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Get month and pad with 0
    const day = String(date.getDate()).padStart(2, '0');        // Get day and pad with 0
    return `${month}/${day}`;
};

const ProductSale = ({ saleAmount, endDate, price, infiniteUses, useCount , bought, voucherDetailId}) => {
    const formattedEndDate = formatDate(endDate); 
    return (
        <div className="product-sale">
            <SaleHeader endDate={formattedEndDate} voucherDetailId={voucherDetailId}/>
            <SaleContent saleAmount={saleAmount} price={price} infiniteUses={infiniteUses} useCount={useCount}  bought={bought} voucherDetailId={voucherDetailId} />
            <style jsx>{`
                .product-sale {
                    display: flex; /* Make it a flex container */
                    align-items: stretch; /* Ensure items stretch to the same height */
                    border-radius: 10px;
                    overflow: hidden;
                    width: 100%; /* Full width of the container */
                    max-width: 600px; /* Optional max-width to control size */
                    margin: 10px 0; /* Space between ProductSale components */
                    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1); /* Optional shadow for better visual */
                }
            `}</style>
        </div>
    );
};

export default ProductSale;
