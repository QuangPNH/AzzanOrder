import React from 'react';

const Logo = () => {
    const handleLogoClick = () => {
        window.location.href = '/';
    };

    return (
        <>
            <img
                src="https://cdn.builder.io/api/v1/image/assets/TEMP/e7970bccea406727a92dffa6eaf3dad60f5580953085ae7d3a1cd0abb435e4e5?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8"
                alt="Company logo"
                className="logo"
                loading="lazy"
                onClick={handleLogoClick}
            />
            <style jsx>{`
                .logo {
                    aspect-ratio: 4.67;
                    cursor: pointer;
                    object-fit: contain;
                    object-position: center;
                    width: 126px;
                    max-width: 100%;
                    margin: auto 0;
                }
            `}</style>
        </>
    );
};

export default Logo;
