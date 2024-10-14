import React, { useState } from 'react';
import Image from './Image';
import PriceTag from './PriceTag';

function generateRandomKey(length) {
  const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  let key = '';
  for (let i = 0; i < length; i++) {
    const randomIndex = Math.floor(Math.random() * characters.length);
    key += characters.charAt(randomIndex);
  }
  return key;
}

const ProductCard = ({ imageSrc, title, price }) => {
    const handleAddToCart = () => {
        console.log("Add to cart");
        const storedData = sessionStorage.getItem('cartData');
        let parsedData = [];
        if (storedData) {
            parsedData = JSON.parse(storedData);
        }
        const newItem = {
            key: generateRandomKey(5),
            name: title,
            options: ["Full sugar", "Full Ice"],
            price: price,
            quantity: 1
        };
        console.log(newItem);
        parsedData.push(newItem);
        sessionStorage.setItem('cartData', JSON.stringify(parsedData));
    };

 
    return (
        <article className="product-card">
            <Image src={`data:image/png;base64, ${imageSrc}`} alt={title} />
            <h2 className="product-title">{title}</h2>
            <PriceTag price={price} onclick={handleAddToCart} />
            <style jsx>
                {`
                .product-card {
                  border-radius: 8px;
                  display: flex;
                  max-width: 154px;
                  flex-direction: column;
                  font-family: Inter, sans-serif;
                  font-weight: 700;
                  background-color: #f9f9f9;
                  padding: 10px;
                }
                .product-title {
                  color: #000;
                  font-size: 14px;
                  margin-top: 13px;
                }
                `}
            </style>
        </article>
    );
};

export default ProductCard;