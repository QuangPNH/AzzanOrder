import React, { useState, useEffect } from 'react';
import { getCookie } from '../Account/SignUpForm/Validate';
import API_URLS from '../../config/apiUrls';

const Logo = () => {
    const [logoSrc, setLogoSrc] = useState('');

    useEffect(() => {
        const tableqr = getCookie("tableqr");
        if (tableqr) {
            // Fetch the logo URL based on the tableqr value
            const fetchLogoSrc = async (manaId) => {
                try {
                    const url = manaId ? API_URLS.API + `Promotions/GetByDescription/logo?manaId=${manaId}` : API_URLS.API + 'Promotions/GetByDescription/logo';
                    const response = await fetch(url);
                    if (!response.ok) {
                        throw new Error("Network response was not ok");
                    }
                    const data = await response.json();
                    setLogoSrc(data.image);
                } catch (error) {
                    console.error("Failed to fetch logo URL:", error);
                }
            };
            fetchLogoSrc();
        }
    }, []);

    const handleLogoClick = () => {
        window.location.href = '/';
    };

    return (
        <>
            <img
                src={logoSrc || "https://s6.imgcdn.dev/gl4Iv.png"}
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