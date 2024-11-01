import React, { useState, useEffect } from 'react';
import Modal from 'react-modal';
import LoginPage from '../../components/Account/LoginPage';
import SignUpPage from '../../components/Account/SignUpPage';
import LogoutPage from '../Account/LogoutPage';

// Set the app element to avoid accessibility warnings in react-modal
Modal.setAppElement('#root');

const NavItem = () => {

    const [showRecentlyOrdered, setShowRecentlyOrdered] = useState(false);
    const [showLogin, setLogin] = useState(false);
    const [showSignup, setSignup] = useState(false);
    const [showLogout, setLogout] = useState(false);

    const navItems = [
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/2000ef5b1388f84bd861a1cfeabcb17787a38e432ea204379ab731580cd84b0b?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Login" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/fe570d9132ffff17abf26958447f9d8a921987650f9e6cb66275a59986d8d8d1?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Sign up" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/70410d91514f262ce614a7b7bcfb761a294b0ce637a9bc36accdd377b655428a?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Notification" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/360aab801c588575a78887d36d50e88cb9803d8221437815dec0250221ff6e7c?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Send Feedback" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/78f4bb1510aa509cca4a6da407039f2446e5d7484ce18824cdaeed7e7c64d055?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Logout" }
    ];

    const handleNotificationClick = () => {
        window.location.href = '/notification';
    };

    const handleSendFeedbackClick = () => {
        window.location.href = '/feedback';
    };

    const handleLoginCheck = (result) => {
        if (result === 'fail') {
            setLogin(false);
            setSignup(true);
        }
    };

    const handleLogout = () => {
        setCookie('memberInfo', '', -1);
    };

    const handleSignUpCheck = (result) => {
        if (result === 'fail') {
            setSignup(false);
            setLogin(true);
        }
    };

    useEffect(() => {
        if (getCookie('memberInfo') != null) {
            setShowRecentlyOrdered(true);
        }
    }, []);

    return (
        <div>
            <LoginPage isOpen={showLogin} handleClosePopup={() => setLogin(false)} onCheck={handleLoginCheck} />
            <SignUpPage isOpen={showSignup} handleClosePopup={() => setSignup(false)} onCheck={handleSignUpCheck} />
            <LogoutPage isOpen={showLogout} handleClosePopup={() => setLogout(false)} func={handleLogout} title={"You sure you want to log out ?"} />
            <div className="nav-items">
                {!showRecentlyOrdered && (
                    <div className="nav-item1" onClick={() => setLogin(true)}>
                        <img loading="lazy" src={navItems[0].icon} alt={navItems[0].text} className="nav-icon" />
                        <span className="nav-text">{navItems[0].text}</span>
                    </div>
                )}
                {showRecentlyOrdered && (
                    <div className="nav-item1" onClick={() => setLogout(true)}>
                        <img loading="lazy" src={navItems[4].icon} alt={navItems[4].text} className="nav-icon" />
                        <span className="nav-text">{navItems[4].text}</span>
                    </div>
                )}
                {!showRecentlyOrdered && (
                    <div className="nav-item1" onClick={() => setSignup(true)}>
                        <img loading="lazy" src={navItems[1].icon} alt={navItems[1].text} className="nav-icon" />
                        <span className="nav-text">{navItems[1].text}</span>
                    </div>
                )}
                {showRecentlyOrdered && (
                    <div className="nav-item1" onClick={handleNotificationClick}>
                        <img loading="lazy" src={navItems[2].icon} alt={navItems[2].text} className="nav-icon" />
                        <span className="nav-text">{navItems[2].text}</span>
                    </div>
                )}
                {showRecentlyOrdered && (
                    <div className="nav-item1" onClick={handleSendFeedbackClick}>
                        <img loading="lazy" src={navItems[3].icon} alt={navItems[3].text} className="nav-icon" />
                        <span className="nav-text">{navItems[3].text}</span>
                    </div>
                )}
                <style jsx>{`
                .nav-items1 {
                    display: flex;
                    flex-direction: column;
                    gap: 8px; /* Space between nav items */
                }
                .nav-item1 {
                    display: flex; /* Flex display for each nav item */
                    align-items: center; /* Center align icon and text */
                    gap: 10px; /* Space between icon and text */
                    padding: 8px; /* Padding for nav items */
                    cursor: pointer; /* Cursor change on hover */
                    transition: background-color 0.3s; /* Smooth background transition */
                }
                .nav-item1:hover {
                    background-color: rgba(255, 255, 255, 0.5); /* Light hover effect */
                }
                .nav-icon {
                    aspect-ratio: 1; /* Maintain aspect ratio */
                    object-fit: contain; /* Ensure the icon fits within its bounds */
                    object-position: center; /* Center the icon */
                    width: 24px; /* Adjust icon size */
                }
                .nav-text {
                    white-space: nowrap;
                    font-size: 14px; /* Font size for text */
                    line-height: 1.5; /* Adjust line height for better spacing */
                    color: #000; /* Text color */
                }
            `}</style>
            </div>
        </div>
    );
};

function setCookie(name, value, days) {
  const expires = new Date(Date.now() + days * 864e5).toUTCString();
  document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`;
}

function getCookie(name) {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return decodeURIComponent(parts.pop().split(';').shift());
}

function clearCookie(name) {
  document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/`;
}

export default NavItem;
