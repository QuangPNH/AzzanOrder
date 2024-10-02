import React from 'react';

const Image = ({ src, alt }) => {
    return (
        <>
            <img src={src} alt={alt} className="product-image" />
            <style jsx>{`
        .product-image {
          aspect-ratio: 1;
          object-fit: contain;
          object-position: center;
          width: 100%;
          border-radius: 10px;
          box-shadow: 2px 0 5px rgba(0, 0, 0, 0.25);
        }
      `}</style>
        </>
    );
};

export default Image;