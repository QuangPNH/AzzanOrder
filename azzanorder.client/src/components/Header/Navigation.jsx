import React, { useState, useEffect } from 'react';
import { getCookie } from '../Account/SignUpForm/Validate';
import LoginPage from '../Account/LoginPage';
import API_URLS from '../../config/apiUrls';

const fetchData = async (phoneNumber, setImage, setNavItems, navItems) => {
    try {
        const response = await fetch(API_URLS.API + `Member/Phone/${phoneNumber}`);
        const data = await response.json();
        setImage(data.image);
        const updatedNavItems = [...navItems];
        updatedNavItems[0].src = data.image;
        setNavItems(updatedNavItems);
    } catch (error) {
        console.error('Error fetching member info:', error);
    }
};

const Navigation = ({ toggleNavbar }) => {
    const [image, setImage] = useState(undefined);
    const [showProfile, setShowProfile] = useState(false);
    const [showLogin, setShowLogin] = useState(false);
    const [navItems, setNavItems] = useState([
        {
            src: "https://cdn.builder.io/api/v1/image/assets/TEMP/aae56868fdcb862e605ea9a58584175c78f8bec2f1376557a9d660d8863bf323?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8",
            alt: "Navigation item 1",
        },
        {
            src: "https://cdn.builder.io/api/v1/image/assets/TEMP/189297da07ec9868357cb4291401ef50667416493bf889bffb02c9cca138ebca?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8",
            alt: "Navigation item 2",
            onClick: toggleNavbar,
        },
    ]);

    useEffect(() => {
        const memberInfo = getCookie('memberInfo');
        if (memberInfo) {
            const memberInfo1 = JSON.parse(memberInfo);
            setShowProfile(true);
            const phoneNumber = JSON.parse(getCookie('memberInfo')).phone;
            fetchData(phoneNumber, setImage, setNavItems, navItems);
        }
    }, [image]);

    const handleProfileClick = () => {
        window.location.href = '/profile';
    };

    const handleLoginClick = () => {
        setShowLogin(true);
    };

    const handleCloseLogin = () => {
        setShowLogin(false);
    };

    const navItem1 = (
        <img
            key={0}
            src={navItems[0].src}
            alt={navItems[0].alt}
            className="nav-item"
            loading="lazy"
            onClick={showProfile ? handleProfileClick : handleLoginClick}
        />
    );

    const navItem2 = (
        <img
            key={1}
            src={navItems[1].src}
            alt={navItems[1].alt}
            className="nav-item nav-item-last"
            loading="lazy"
            onClick={navItems[1].onClick}
        />
    );

    return (
        <nav className="navigation">
            {navItem1}
            {navItem2}
            {showLogin && <LoginPage isOpen={showLogin} handleClosePopup={handleCloseLogin} />}
            <style jsx>{`
                .navigation {
                    display: flex;
                    gap: 5px; /* Reduce gap even more to make it more compact */
                    align-items: center;
                }
                .nav-item {
                    cursor: pointer;
                    aspect-ratio: 1.02;
                    object-fit: contain;
                    object-position: center;
                    width: 40px; /* Significantly reduce width */
                }
                .nav-item-last {
                    aspect-ratio: 1;
                    width: 38px; /* Smaller width for the last item */
                    align-self: center;
                    margin: auto 0 auto auto;
                }
            `}</style>
        </nav>
    );
};

export default Navigation;