import React from 'react';
import ShowMoreButton from './ShowMoreButton';

const RecentlyOrdered = ({title, url }) => {
  return (
      <section className="recently-ordered-section">
          <h2 className="section-title">{title}</h2>
          <ShowMoreButton url={url} />
      <style jsx>{`
        .recently-ordered-section {
          display: flex;
          max-width: 331px;
          padding: 1px 0 0;
          align-items: center;
          justify-content: space-between;
          font-family: Inter, sans-serif;
          color: #000;
        }
        .section-title {
          font-size: 20px;
          font-weight: 700;
          margin: auto 0;
        }
      `}</style>
    </section>
  );
};

export default RecentlyOrdered;