import React, { useState } from 'react';
import Popup from 'reactjs-popup';
import LoginPage from '../../components/Account/LoginPage';
import SignUpPage from '../../components/Account/SignUpPage';

const NavItem = () => {
    const [isPopupVisible, setPopupVisible] = useState(false);

    const navItems = [
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/2000ef5b1388f84bd861a1cfeabcb17787a38e432ea204379ab731580cd84b0b?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Login" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/fe570d9132ffff17abf26958447f9d8a921987650f9e6cb66275a59986d8d8d1?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Sign up" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/70410d91514f262ce614a7b7bcfb761a294b0ce637a9bc36accdd377b655428a?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Notification" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/360aab801c588575a78887d36d50e88cb9803d8221437815dec0250221ff6e7c?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Send Feedback" }
    ];

    const handleLoginClick = () => {
        window.location.href = '/login';
    };

    const handleRegisterClick = () => {
        window.location.href = '/register';
    };

    const handleNotificationClick = () => {
        window.location.href = '/notification';
    };

    const handleSendFeedbackClick = () => {
        window.location.href = '/sendfeedback';
    };


    return (
        <div className="nav-items">
            {/* <div className="nav-item" onClick={handleLoginClick}>
            <img loading="lazy" src={navItems[0].icon} alt={navItems[0].text} className="nav-icon" />
                <span className="nav-text">{navItems[0].text}</span>
            </div> */}
            <Popup position="bottom right"
            trigger={<div className="nav-item1"><img loading="lazy" src={navItems[0].icon} alt={navItems[0].text} className="nav-icon" />
                <span className="nav-text">{navItems[0].text}</span></div>}>
                    <LoginPage style="z-index: 10000000000000"/>
            </Popup>
            {/* <div className="nav-item" onClick={handleRegisterClick}>
                <img loading="lazy" src={navItems[1].icon} alt={navItems[1].text} className="nav-icon" />
                <span className="nav-text">{navItems[1].text}</span>
            </div> */}
            <Popup position="bottom right" 
            trigger={<div className="nav-item1"><img loading="lazy" src={navItems[1].icon} alt={navItems[1].text} className="nav-icon" />
                <span className="nav-text">{navItems[1].text}</span></div>}>
                    <SignUpPage style="z-index: 10000000000000"/>
            </Popup>
            <div className="nav-item1" onClick={handleNotificationClick}>
                <img loading="lazy" src={navItems[2].icon} alt={navItems[2].text} className="nav-icon" />
                <span className="nav-text">{navItems[2].text}</span>
            </div>
            <div className="nav-item1" onClick={handleSendFeedbackClick}>
                <img loading="lazy" src={navItems[3].icon} alt={navItems[3].text} className="nav-icon" />
                <span className="nav-text">{navItems[3].text}</span>
            </div>
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
    );
};

function setCookie(name, value, days) {
  const expires = new Date(Date.now() + days * 864e5).toUTCString(); // Calculate expiration date
  document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`; // Set cookie
}

function getCookie(name) {
  const value = `; ${document.cookie}`; // Add a leading semicolon for easier parsing
  const parts = value.split(`; ${name}=`); // Split the cookie string to find the desired cookie
  if (parts.length === 2) return decodeURIComponent(parts.pop().split(';').shift()); // Return the cookie value
}

function clearCookie(name) {
  // Set the cookie's expiration to a time in the past to delete it
  document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/`; // Clear cookie by setting the date to the past
}


export default NavItem;



