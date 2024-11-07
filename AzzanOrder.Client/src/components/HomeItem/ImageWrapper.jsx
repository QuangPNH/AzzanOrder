import React from "react";

const ImageWrapper = ({ src, alt, style }) => {
    return (
        <>
            <img loading="lazy" src={src} alt={alt} className="gallery-image" style={style} />
            <style jsx>{`
                .gallery-image {
                    position: absolute;
                    inset: 0;
                    height: 100%;
                    width: 100%;
                    object-fit: cover;
                    object-position: center;
                }
                .gallery-image:nth-child(2) {
                    position: static;
                    aspect-ratio: 7.63;
                    object-fit: contain;
                    width: 99px;
                }
            `}</style>
        </>
    );
};

export default ImageWrapper;