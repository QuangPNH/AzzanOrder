import React from 'react';

const ShowMoreButton = ({ url }) => {
    function openLink() {
        window.open(url);
    }
    return (
        //<button onClick={ openLink } className="show-more-button">
      //<span>Show more</span>
     // <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/5372c727a3b308d7d11d279cd43dbf3122dd2beeeea78672cb0cd88a821bb029?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8" alt="" className="arrow-icon" />
      <style jsx>{`
        .show-more-button {
          display: flex;
          align-items: center;
          gap: 5px;
          font-size: 12px;
          font-weight: 400;
          background: none;
          border: none;
          cursor: pointer;
          padding: 0;
          color: inherit;
        }
        .arrow-icon {
          width: 14px;
          height: 14px;
          object-fit: contain;
        }
      `}</style>
    //</button>
  );
};

export default ShowMoreButton;