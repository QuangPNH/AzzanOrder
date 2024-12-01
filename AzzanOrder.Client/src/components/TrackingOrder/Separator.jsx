import React from 'react';

const Separator = () => {
    return (
        <div className="menu-separator">
            <div className="separator-line" />
            <style jsx>{`
        .menu-separator {
          border-radius: 8px;
          display: flex;
          margin-top: 12px;
          width: 100%;
          flex-direction: column;
          justify-content: center;
          padding: 8px 16px;
        }
        .separator-line {
          background: var(--primary-color);
          display: flex;
          min-height: 1px;
          width: 100%;
          height: var(--Stroke-Border, 1px);
        }
      `}</style>
        </div>
    );
};

export default Separator;