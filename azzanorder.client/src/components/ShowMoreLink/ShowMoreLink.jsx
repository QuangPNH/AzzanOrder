import React from 'react';
import ShowMoreButton from './ShowMoreButton';

const RecentlyOrdered = ({ title, url }) => {
    return (
        <section className="recently-ordered-section">
            <h2 className="section-title">{title}</h2>
            <ShowMoreButton url={url} />

            <style jsx>{`
        .recently-ordered-section {
          display: flex;
          max-width: 80%; /* Container width that centers the elements but does not fill the entire page */
          margin: 0 auto; /* Center the whole section on the page */
          padding: 1px 0 0;
          align-items: center;
          justify-content: space-between; /* Distribute title and button with space in between */
          font-family: Inter, sans-serif;
          color: #000;
        }
        .section-title {
           white-space: nowrap;
          font-size: 20px;
          font-weight: 700;
          margin: 0;
        }
      `}</style>
        </section>
    );
};

export default RecentlyOrdered;
