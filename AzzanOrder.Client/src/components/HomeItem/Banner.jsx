import React, { useEffect, useState } from 'react';
import DotIndicator from './DotIndicator';
import ImageWrapper from './ImageWrapper';

const Banner = () => {
    const [promotions, setPromotions] = useState([]);

    useEffect(() => {
        fetchImages();
    }, []);

    const fetchImages = async () => {
        try {
            const response = await fetch('https://localhost:7183/api/Promotions/GetByDescription/banner');
            const data = await response.json();
            setPromotions(data);
        } catch (error) {
            console.error('Error fetching images:', error);
        }
    };
    const dots = promotions.map((_, index) => ({ active: index === 0 }));

    return (
        <>
            <section className="banner">
                <div className="image-gallery">
                    {promotions.map((image, index) => (
                        <ImageWrapper key={index} src={image.image} alt={image.title} />
                    ))}
                </div>

                <nav className="banner-navigation">
                    {dots.map((dot, index) => (
                        <DotIndicator key={index} active={dot.active} />
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
