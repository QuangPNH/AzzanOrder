import React from 'react';

const Image = ({ src, alt, onClick }) => {
    return (
        <>
            <img src={src} alt={alt} className="product-image" onClick={onClick} />
            <style jsx>{`
        .product-image {
            cursor: pointer;
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