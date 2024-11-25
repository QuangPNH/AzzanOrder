import React, { useState, useEffect } from 'react';
import API_URLS from '../../config/apiUrls';

const Category = () => {
    // const [category, setCategorys] = useState([]);
    // useEffect(() => {
    //     fetchCategorys();
    // });
    // const fetchCategorys = async () => {
    //     try {
    //         const response = await fetch(API_URLS.API + `Category/${CategoryId}`);
    //         const data = await response.json();
    //         setCategorys(data);
    //     } catch (error) {
    //         console.error('Error fetching menu items:', error);
    //     }
    // };
    return (
        <>
            <div className="category">
                
                {/* {ponit && ( */}
                    <div>
                        <div className="category-title">Category</div>
                            {/* {ponit.map((m) => (
                                <Category
                                    key={m.id}
                                    points={m.title}
                                />
                            ))} */}
                    </div>
                {/* )} */}
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
