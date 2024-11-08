﻿import React from 'react';

const DecorativeLine = ({ width }) => {
  return (
    <>
      <div className="decorative-line" style={{ width: `${width}%` }} />
      <style jsx>{`
        .decorative-line {
          height: 1px;
          align-self: center;
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