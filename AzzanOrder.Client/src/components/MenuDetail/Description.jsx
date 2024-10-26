import React from 'react';

const Description = ({ content }) => {
    return (
        <section className="description">
            <h2 className="title">Description</h2>
            <p className="content">{content}</p>
            <style jsx>{`
                .description {
                    border-radius: 0;
                    display: flex;
                    max-width: 323px;
                    flex-direction: column;
                    align-items: flex-start;
                    font-family: Inter, sans-serif;
                }
                .title {
                    color: #000;
                    font-size: 16px;
                    font-weight: 700;
                    z-index: 10;
                }
                .content {
                    color: rgba(0, 0, 0, 0.8);
                    font-size: 14px;
                    font-weight: 400;
                    z-index: 10;
                    margin-top: 14px;
                }
            `}</style>
        </section>
    );
};

export default Description;
