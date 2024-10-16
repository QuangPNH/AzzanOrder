import React from 'react';

const Category = () => {
    return (
        <>
            <div className="category">
                <div className="category-title">Category</div>
            </div>
            <style jsx>{`
                .category {
                    display: flex;
                    justify-content: flex-start; /* Align to the left */
                    align-items: center;
                    padding: 20px;
                }
                .category-title {
                    color: #000;
                    font: 600 24px Inter, sans-serif;
                    margin: 0;
                }
            `}</style>
        </>
    );
};

export default Category;
