import React from 'react';

function NavigationItem({ icon, label }) {
    return (
        <>
            <div className="nav-item2">
                <div className="icon-wrapper">
                    <img src={icon} alt={`${label} icon`} className="icon" />
                </div>
                <span className="label">{label}</span>
            </div>
            <style jsx>{`
        .nav-item2 {
            cursor: pointer;
          box-shadow: 0 4px 4px rgba(0, 0, 0, 0.25);
          align-self: stretch;
          display: flex;
          width: 82px;
          flex-direction: column;
          align-items: center;
          justify-content: flex-start;
          margin: auto 0;
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
          width: 24px;
          height: 24px;
        }
        .label {
          color: rgba(76, 90, 97, 1);
          letter-spacing: -0.5px;
          text-align: center;
          margin-top: 12px;
          font: 400 14px Inter, sans-serif;
        }
      `}</style>
        </>
    );
}

export default NavigationItem;