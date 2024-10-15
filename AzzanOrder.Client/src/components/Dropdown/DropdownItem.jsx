﻿import React from 'react';

const DropdownItem = ({ label, iconSrc, onClick }) => {
    return (
        <>
            <div className="dropdown-item" onClick={onClick}>
                <span className="item-label">{label}</span>
                {/* Display icon only if iconSrc is provided */}
                {iconSrc && <img src={iconSrc} alt="" className="item-icon" />}
            </div>
            <style jsx>{`
        .dropdown-item {
          display: flex;
          align-items: center;
          justify-content: space-between;
          width: 100%;
          padding: 10px;
          cursor: pointer;
        }
        .item-label {
          margin: auto 0;
        }
        .item-icon {
          width: 24px;
          height: 24px;
        }
      `}</style>
        </>
    );
};

export default DropdownItem;
