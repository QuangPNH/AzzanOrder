import React from 'react';
import NavItem from './NavItem'

const Navbar = () => (
    <nav className="navbar">
        
        <NavItem />
        <style jsx>{`
      .navbar {
        position: absolute; /* Position absolutely */
        top: 100%; /* Adjust to place below the header */
        right: 0; /* Align it to the right side */
        border-bottom-left-radius: 10px;
        border-bottom-right-radius: 10px;
        background-color: rgba(246, 181, 181, 1); /* Light background */
        display: flex;
        flex-direction: column; /* Vertical stacking of items */
        justify-content: flex-start; /* Align items to start */
        padding: 8px 8px; /* Adjust padding for spacing */
        width: 160px; /* Fixed width for consistent layout */
        border: 1px solid rgba(0, 0, 0, 0.2); /* Border around the navbar */
        font: 400 14px Inter, sans-serif; /* Font styles */
        color: #000; /* Text color */
        z-index: 1001; /* Ensure it overlaps other elements */
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.25); /* Shadow for better visibility */
      }
      
      .nav-item:hover {
        background-color: rgba(255, 255, 255, 0.5); /* Light hover effect */
      }
    `}</style>
    </nav>
);

export default Navbar;
