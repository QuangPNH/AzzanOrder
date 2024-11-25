import React, { useState, useEffect } from "react";
import Logo from "./Logo";
import Navigation from "./Navigation";
import Navbar from "./Navbar"; // Assuming you have a Navbar component
import { getCookie } from '../Account/SignUpForm/Validate';
import API_URLS from "../../config/apiUrls";

const Header = () => {
    const [showNavbar, setShowNavbar] = useState(false);

    // Toggle Navbar visibility
    const toggleNavbar = () => {
        setShowNavbar((prevShow) => !prevShow);
    };

    const [backgroundColor, setBackgroundColor] = useState('#f6b5b5'); // Default background color

    useEffect(() => {
        const tableqr = getCookie("tableqr");
        if (tableqr) {
            // Fetch the background color based on the tableqr value
            const fetchBackgroundColor = async (manaId) => {
                try {
                    const url = manaId ? API_URLS.API + `Promotions/GetByDescription/color?manaId=${manaId}` : API_URLS.API + `Promotions/GetByDescription/color`;
                    const response = await fetch(url);
                    if (response.ok) {
                        const data = await response.json();
                        setBackgroundColor(data.description.split('/')[1]);
                    }
                } catch (error) {
                    console.error("Failed to fetch background color:", error);
                }
            };
            fetchBackgroundColor();
        }
    }, []);

    return (
        <>
            <header className="header sticky">
                <Logo />
                <Navigation toggleNavbar={toggleNavbar} />
                {showNavbar && <Navbar />} {/* Render Navbar below Header when visible */}
                <style jsx>{`
          .header {
            background-color: ${backgroundColor};
            display: flex;
            justify-content: space-between;
            align-items: center;
            width: 100vw; /* Use viewport width for full coverage */
            height: 64px;
            padding: 8px 14px;
            box-sizing: border-box; /* Ensure padding doesn't affect width */
            position: sticky; /* Make the header sticky */
            top: 0; /* Stick it to the top of the page */
            left: 0; /* Align to the very left edge of the viewport */
            z-index: 1000;
          }
        `}</style>
            </header>
            
        </>
    );
};

export default Header;
