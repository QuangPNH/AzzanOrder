import React, { useState } from "react";
import Logo from "./Logo";
import Navigation from "./Navigation";
import Navbar from "./Navbar"; // Assuming you have a Navbar component

const Header = () => {
    const [showNavbar, setShowNavbar] = useState(false);

    // Toggle Navbar visibility
    const toggleNavbar = () => {
        setShowNavbar((prevShow) => !prevShow);
    };

    return (
        <>
            <header className="header">
                <Logo />
                <Navigation toggleNavbar={toggleNavbar} />
                <style jsx>{`
          .header {
            background-color: #f6b5b5;
            display: flex;
            justify-content: space-between;
            align-items: center;
            width: 100vw; /* Use viewport width for full coverage */
            padding: 8px 14px;
            box-sizing: border-box; /* Ensure padding doesn't affect width */
            position: fixed; /* Optionally, if you want the header to stick to the top */
            top: 0; /* Stick it to the top of the page */
            left: 0; /* Align to the very left edge of the viewport */
            z-index: 1000;
          }
        `}</style>
            </header>
            {showNavbar && <Navbar />} {/* Render Navbar below Header when visible */}
        </>
    );
};

export default Header;
