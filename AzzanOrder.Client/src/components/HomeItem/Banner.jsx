import React, { useEffect, useState } from 'react';
import DotIndicator from './DotIndicator';
import ImageWrapper from './ImageWrapper';
import { getCookie } from '../Account/SignUpForm/Validate';
import API_URLS from '../../config/apiUrls';

const Banner = () => {
    const [promotions, setPromotions] = useState([]);
    const [activeIndex, setActiveIndex] = useState(0);

    useEffect(() => {
        fetchImages(getCookie('tableqr') ? getCookie('tableqr').split('/')[1] : null);
    }, []);

    const fetchImages = async (manaId) => {
        try {
            const url = manaId ? API_URLS.API + `Promotions/GetByDescription/banner?manaId=${manaId}` : API_URLS.API + 'Promotions/GetByDescription/banner';
            const response = await fetch(url);
            const data = await response.json();
            setPromotions(Array.isArray(data) ? data : []);
        } catch (error) {
            console.error('Error fetching images:', error);
        }
    };

    const dots = promotions.map((_, index) => ({ active: index === activeIndex }));

    return (
        <>
            <section className="banner">
                <div className="image-gallery">
                    {promotions?.map((image, index) => (
                        <ImageWrapper key={index} src={image.image} alt={image.title} style={{ display: index === activeIndex ? 'block' : 'none' }} />
                    ))}
                </div>

                <nav className="banner-navigation">
                    {dots.map((dot, index) => (
                        <DotIndicator key={index} active={dot.active} onClick={() => setActiveIndex(index)} />
                    ))}
                </nav>
            </section>

            <style jsx>{`
                .banner {
                    width: 100vw; /* Full width of the viewport */
                    max-width: 100%;
                    height: 300px;
                    display: flex;
                    flex-direction: column;
                    align-items: center;
                    position: relative;
                    padding: 0;
                }

                .image-gallery {
                    width: 100%;
                    height: 100%;
                    position: relative;
                    overflow: hidden;
                }

                .banner-navigation {
                    display: flex;
                    gap: 8px;
                    position: absolute;
                    bottom: 40px; /* Increased space from the bottom */
                    z-index: 2; /* Ensure dots are on top */
                }
            `}</style>
        </>
    );
};

export default Banner;