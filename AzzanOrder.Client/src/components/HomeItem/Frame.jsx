import React, { useState, useEffect } from 'react';
import { getCookie } from '../Account/SignUpForm/Validate';
import API_URLS from '../../config/apiUrls';

const Frame = () => {
    const [promotions, setPromotions] = useState([]);
    const [currentIndex, setCurrentIndex] = useState(0);

    useEffect(() => {
        const fetchPromotions = async (manaId) => {
            try {
                const url = manaId ? API_URLS.API + `Promotions/GetByDescription/carousel?manaId=${manaId}` : API_URLS.API + 'Promotions/GetByDescription/carousel';
                const response = await fetch(url);
                const data = await response.json();
                setPromotions(data);
            } catch (error) {
                console.error('Error fetching promotions:', error);
            }
        };

        fetchPromotions(getCookie('tableqr') ? getCookie('tableqr').split('/')[1] : null);
    }, []);

    const handleNext = () => {
        setCurrentIndex((prevIndex) => (prevIndex + 1) % promotions.length);
    };

    const handlePrev = () => {
        setCurrentIndex((prevIndex) => (prevIndex - 1 + promotions.length) % promotions.length);
    };

    if (promotions.length === 0) {
        return <div>Loading...</div>;
    }

    const bigPromotion = promotions[currentIndex];
    const leftPromotion = promotions[(currentIndex - 1 + promotions.length) % promotions.length];
    const rightPromotion = promotions[(currentIndex + 1) % promotions.length];

    return (
        <div className="carousel-container">
            <div className="small-image-container left">
                <img
                    className="small-image"
                    alt={leftPromotion?.title}
                    src={leftPromotion?.image}
                    onClick={handlePrev}
                />
            </div>
            <div className="big-image-container">
                <img className="big-image" alt={bigPromotion?.title} src={bigPromotion?.image} />
                <div className="promotion-details">
                    <h2>{bigPromotion?.title}</h2>
                </div>
            </div>
            <div className="small-image-container right">
                <img
                    className="small-image"
                    alt={rightPromotion?.title}
                    src={rightPromotion?.image}
                    onClick={handleNext}
                />
            </div>

            <style jsx>{`
                .carousel-container {
                    position: relative;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    width: 100%;
                    height: 20vh;
                    overflow: hidden;
                }

                .big-image-container {
                    position: relative;
                    width: 70%;
                    z-index: 2;
                    border-radius: 12px;
                    overflow: hidden;
                    text-align: center;
                }

                .big-image {
                    width: 100%;
                    height: 20vh;
                    object-fit: cover;
                }

                .promotion-details {
                    position: absolute;
                    bottom: 0;
                    background: rgba(0, 0, 0, 0.1);
                    color: white;
                    width: 100%;
                    padding: 10px;
                }

                .small-image-container {
                    position: absolute;
                    background: rgba(0, 0, 0, 0.3);
                    width: 56%; /* 70% of 80% */
                    z-index: 1;
                }

                .small-image-container.left {
                    left: 0;
                    transform: translateX(-30%);
                }

                .small-image-container.right {
                    right: 0;
                    transform: translateX(30%);
                }

                .small-image {
                    width: 100%;
                    height: 18vh;
                    cursor: pointer;
                    border-radius: 12px;
                    object-fit: cover;
                }

                @media (max-width: 768px) {

                    .small-image-container {
                        width: 63%; /* 70% of 90% */
                    }

                    .small-image-container.left {
                        transform: translateX(-35%);
                    }

                    .small-image-container.right {
                        transform: translateX(35%);
                    }
                }
            `}</style>
        </div>
    );
};

export default Frame;