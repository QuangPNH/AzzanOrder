import React from 'react';

const DecorativeLine = ({ width }) => {
    return (
        <>
            <div className="decorative-line" style={{ width: `${width}px` }} />
            <style jsx>{`
        .decorative-line {
          height: 1px;
          background-color: #bd3326;
        }
        .decorative-line:last-of-type {
          margin-top: 9px;
        }
      `}</style>
        </>
    );
};

export default DecorativeLine;