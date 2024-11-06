import React, { useState, useEffect } from 'react';
import { getCookie } from '../Account/SignUpForm/Validate';

const Logo = () => {
    const [logoSrc, setLogoSrc] = useState('');

    useEffect(() => {
        const tableqr = getCookie("tableqr");
        if (tableqr) {
            // Fetch the logo URL based on the tableqr value
            const fetchLogoSrc = async (manaId) => {
                try {
                    const url = manaId ? `https://localhost:7183/api/Promotions/GetByDescription/logo?manaId=${manaId}` : 'https://localhost:7183/api/Promotions/GetByDescription/logo';
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
                src={logoSrc || "https://cdn.builder.io/api/v1/image/assets/TEMP/e7970bccea406727a92dffa6eaf3dad60f5580953085ae7d3a1cd0abb435e4e5?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8"}
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
function getCookie(name) {
    const value = `; ${document.cookie}`; // Add a leading semicolon for easier parsing
    const parts = value.split(`; ${name}=`); // Split the cookie string to find the desired cookie
    if (parts.length === 2) return decodeURIComponent(parts.pop().split(';').shift()); // Return the cookie value
}
export default Logo;