import React from 'react';

const DotIndicator = ({ active }) => {
    return (
        <div className={`dot ${active ? 'active' : ''}`}>
            <style jsx>{`
        .dot {
          background-color: #f0f9ff;
          border-radius: 50%;
          width: 8px;
          height: 8px;
        }
        .active {
          border-radius: 20px;
          background-color: #4c5a61;
          width: 19px;
        }
      `}</style>
        </div>
    );
};

export default DotIndicator;