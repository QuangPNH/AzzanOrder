import React from 'react';

const MenuItem = ({ icon, label }) => {
  return (
    <div className="menu-item">
      <div className="icon-wrapper">
        <img loading="lazy" src={icon} alt="" className="icon" />
      </div>
      <span className="label">{label}</span>
      <style jsx>{`
        .menu-item {
          display: flex;
          flex-direction: column;
          align-items: center;
        }
        .icon-wrapper {
          border-radius: 12px;
          background-color: rgba(224, 242, 254, 0.7);
          display: flex;
          width: 44px;
          height: 44px;
          align-items: center;
          justify-content: center;
          overflow: hidden;
          padding: 10px;
        }
        .icon {
          aspect-ratio: 1;
          object-fit: contain;
          object-position: center;
          width: 24px;
          align-self: stretch;
          margin: auto 0;
        }
        .label {
          color: rgba(76, 90, 97, 1);
          letter-spacing: -0.5px;
          text-align: center;
          margin-top: 12px;
          font: 400 14px Inter, sans-serif;
        }
      `}</style>
    </div>
  );
};

export default MenuItem;