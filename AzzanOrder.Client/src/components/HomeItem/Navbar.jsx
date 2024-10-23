import React from 'react';
import NavigationItem from './NavigationItem';

const navigationItems = [
    { icon: 'https://cdn.builder.io/api/v1/image/assets/TEMP/4248bf27bbf27f1c483ff23778eaba7c542de4e8e12b9707e9f5dfec6988ba65?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533', label: 'Menu' },
    { icon: 'https://cdn.builder.io/api/v1/image/assets/TEMP/7a853134f70b6e70ec8c0b2dd80275ee6662820bbc34ce628dce07c644383e1b?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533', label: 'About Us' },
    { icon: 'https://cdn.builder.io/api/v1/image/assets/TEMP/77b7958b81123b01b3263f4d163a20b07db78de7b2898a7f38789ad7f4172289?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533', label: 'Order' },
    { icon: 'https://cdn.builder.io/api/v1/image/assets/TEMP/d896ad111b7b0d08a8da0eef1536518980ea24bf8935c612f44c1ad667772f3c?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533', label: 'Voucher' }
];
const handleMenuClick = () => {
    window.location.href = '/menu';
};

const handleAboutClick = () => {
    window.location.href = '/about';
};

const handleOrderClick = () => {
    window.location.href = '/order';
};

const handleVoucherClick = () => {
    window.location.href = '/voucher';
};
function Navbar() {
    return (
        <>
            <nav className="redirect-bar-header">
                <div className="redirect-bar-navigation">
                    <div onClick={handleMenuClick}><NavigationItem icon={navigationItems[0].icon} label={navigationItems[0].label} /></div>
                    <div onClick={handleAboutClick}><NavigationItem icon={navigationItems[1].icon} label={navigationItems[1].label} /></div>
                    <div onClick={handleOrderClick}><NavigationItem icon={navigationItems[2].icon} label={navigationItems[2].label} /></div>
                    <div onClick={handleVoucherClick}><NavigationItem icon={navigationItems[3].icon} label={navigationItems[3].label} /></div>
                </div>
            </nav>
            <style jsx>{`
                .redirect-bar-header {
                    border-radius: 16px;
                    background-color: rgba(255, 255, 255, 0.4); /* Semi-transparent white */
                    backdrop-filter: blur(10px); /* Background blur */
                    display: flex;
                    position: relative;
                    max-width: 328px;
                    height: 20vh;
                    overflow: hidden;
                    padding: 15px 0;
                    margin-top: -20px; /* To overlap part of the banner */
                    z-index: 1;
                }
                .redirect-bar-navigation {
                    display: flex;
                    align-items: center;
                    justify-content: space-around;
                    width: 100%;
                }
            `}</style>
        </>
    );
}

export default Navbar;
