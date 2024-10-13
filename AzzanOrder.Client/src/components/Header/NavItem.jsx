import React from 'react';

const NavItem = () => {
    const navItems = [
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/2000ef5b1388f84bd861a1cfeabcb17787a38e432ea204379ab731580cd84b0b?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Login" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/fe570d9132ffff17abf26958447f9d8a921987650f9e6cb66275a59986d8d8d1?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Sign up" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/70410d91514f262ce614a7b7bcfb761a294b0ce637a9bc36accdd377b655428a?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Notification" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/360aab801c588575a78887d36d50e88cb9803d8221437815dec0250221ff6e7c?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Send Feedback" }
    ];

    return (
        <div className="nav-items">
            {navItems.map((item, index) => (
                <div key={index} className="nav-item">
                    <img loading="lazy" src={item.icon} alt={item.text} className="nav-icon" />
                    <span className="nav-text">{item.text}</span>
                </div>
            ))}
            <style jsx>{`
        .nav-items {
          display: flex;
          flex-direction: column;
          gap: 8px; /* Space between nav items */
        }
        .nav-item {
          display: flex; /* Flex display for each nav item */
          align-items: center; /* Center align icon and text */
          gap: 10px; /* Space between icon and text */
          padding: 8px; /* Padding for nav items */
          cursor: pointer; /* Cursor change on hover */
          transition: background-color 0.3s; /* Smooth background transition */
        }
        .nav-item:hover {
          background-color: rgba(255, 255, 255, 0.5); /* Light hover effect */
        }
        .nav-icon {
          aspect-ratio: 1; /* Maintain aspect ratio */
          object-fit: contain; /* Ensure the icon fits within its bounds */
          object-position: center; /* Center the icon */
          width: 24px; /* Adjust icon size */
        }
        .nav-text {
          font-size: 14px; /* Font size for text */
          line-height: 1.5; /* Adjust line height for better spacing */
          color: #000; /* Text color */
        }
      `}</style>
        </div>
    );
};

export default NavItem;
