import React from 'react';
import Image from './Image';
import PriceTag from './PriceTag';

const ProductCard = ({ imageSrc, title, price }) => {
    return (
        <article className="product-card">
            <Image src={imageSrc} alt={title} />
            <h2 className="product-title">{title}</h2>
            <PriceTag price={price} />
            <style jsx>{`
        .product-card {
          border-radius: 0;
          display: flex;
          max-width: 154px;
          flex-direction: column;
          font-family: Inter, sans-serif;
          font-weight: 700;
        }
        .product-title {
          color: #000;
          font-size: 14px;
          margin-top: 13px;
        }
      `}</style>
        </article>
    );
};

export default ProductCard;