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
                        gap: 40px 100px;
                        overflow: hidden;
                        padding: 8px 14px;
                    }
                `}</style>
            </header>
            {showNavbar && <Navbar />} {/* Render Navbar below Header when visible */}
        </>
    );
};

export default Header;
