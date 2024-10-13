import React from 'react';
import DotIndicator from './DotIndicator';
import ImageWrapper from './ImageWrapper';

const Banner = () => {
    const dots = [
        { active: true },
        { active: false },
        { active: false },
        { active: false },
        { active: false },
    ];

    const images = [
        { src: "https://cdn.builder.io/api/v1/image/assets/TEMP/0331dfde48d756041df00dddfc48c167232f3a52ae4fa409b002c70df0f909b3?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", alt: "Background image" },
        { src: "https://cdn.builder.io/api/v1/image/assets/TEMP/c7179b6bc950b4e515cb704ed8a566ba9b06514a09c8d8455c302b6102e7152a?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", alt: "Foreground image" }
    ];

    return (
        <>
            <section className="banner">
                <div className="image-gallery">
                    {images.map((image, index) => (
                        <ImageWrapper key={index} src={image.src} alt={image.alt} />
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
                    bottom: 20px;
                    z-index: 2;
                }

                .gallery-image {
                    width: 100%;
                    height: 100%;
                    object-fit: cover;
                }
            `}</style>
        </>
    );
};

export default Banner;
