import React from 'react';
import NavItem from './NavItem';

const Navbar = () => (
    <nav className="navbar">
        <NavItem />
        <style jsx>{`
      .navbar {
        position: absolute; /* Position absolutely */
        top: 60px; /* Adjust to place below the header */
        right: 0; /* Align it to the right side */
        border-radius: 10px; /* Rounded corners for the entire navbar */
        background-color: rgba(246, 181, 181, 1); /* Light background */
        display: flex;
        flex-direction: column; /* Vertical stacking of items */
        justify-content: flex-start; /* Align items to start */
        padding: 12px 14px; /* Adjust padding for spacing */
        width: 160px; /* Fixed width for consistent layout */
        border: 1px solid rgba(0, 0, 0, 0.2); /* Border around the navbar */
        font: 400 14px Inter, sans-serif; /* Font styles */
        color: #000; /* Text color */
        z-index: 1000; /* Ensure it overlaps other elements */
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.25); /* Shadow for better visibility */
      }
      .nav-item {
        display: flex; /* Flex display for nav items */
        align-items: center; /* Center align icons and text */
        gap: 10px; /* Space between icon and text */
        padding: 8px; /* Padding for nav items */
        cursor: pointer; /* Cursor change on hover */
        transition: background-color 0.3s; /* Smooth background transition */
      }
      .nav-item:hover {
        background-color: rgba(255, 255, 255, 0.5); /* Light hover effect */
      }
    `}</style>
    </nav>
);

export default Navbar;
