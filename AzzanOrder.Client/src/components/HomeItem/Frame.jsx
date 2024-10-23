import React, { useState } from 'react';

const Frame = () => {
    const [activeImage, setActiveImage] = useState(null); // State to track the active image

    const handleIconClick = (position) => {
        setActiveImage(position); // Update the active image based on which icon is clicked
    };

    return (
        <div className="image-48-parent">
            <img
                className="image-48-icon"
                alt=""
                src="https://cdn.builder.io/api/v1/image/assets/TEMP/597b0a02ed608c600001fc12a8c900fab2ea7b20e253c0d9e392607176db0535?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                onClick={() => handleIconClick('right')} // Click event for the right icon
            />
            <div className="image-25-parent">
                {activeImage === 'left' ? (
                    <img className="image-27-icon" alt="" src="https://cdn.builder.io/api/v1/image/assets/TEMP/597b0a02ed608c600001fc12a8c900fab2ea7b20e253c0d9e392607176db0535?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533" />
                ) : (
                    <img className="image-27-icon" alt="" src="https://cdn.builder.io/api/v1/image/assets/TEMP/597b0a02ed608c600001fc12a8c900fab2ea7b20e253c0d9e392607176db0535?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533" />
                )}
            </div>
            <img
                className="image-47-icon"
                alt=""
                src="https://cdn.builder.io/api/v1/image/assets/TEMP/597b0a02ed608c600001fc12a8c900fab2ea7b20e253c0d9e392607176db0535?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                onClick={() => handleIconClick('left')} // Click event for the left icon
            />

            <style jsx>{`
                .image-48-icon {
                    width: 100%; /* Full width for responsiveness */
                    max-width: 272px; /* Max width to maintain aspect ratio */
                    border-radius: 12px;
                    flex-shrink: 0;
                    object-fit: cover;
                    cursor: pointer; /* Change cursor to pointer for clickable image */
                }

                .image-27-icon {
                    position: absolute;
                    width: 100%; /* Full width for responsiveness */
                    height: 100%; /* Full height to fit the parent */
                    object-fit: cover; /* Ensure it covers the parent */
                }

                .image-25-parent {
                    width: 80%; /* Adjust as needed for responsiveness */
                    max-width: 272px; /* Max width */
                    position: relative;
                    border-radius: 12px;
                    background-color: #d4c3c3;
                    height: 20vh; /* Responsive height */
                    overflow: hidden; /* Ensure it only shows parts of images inside */
                    flex-shrink: 0;
                    display: flex; /* Flexbox for centering */
                    justify-content: center; /* Center the inner image */
                    align-items: center; /* Center the inner image */
                }

                .image-47-icon {
                    width: 100%; /* Full width for responsiveness */
                    max-width: 272px; /* Max width to maintain aspect ratio */
                    border-radius: 12px;
                    flex-shrink: 0;
                    cursor: pointer; /* Change cursor to pointer for clickable image */
                }

                .image-48-parent {
                    width: 100%;
                    position: relative;
                    height: 20vh; /* Responsive height */
                    overflow: hidden; /* Changed to hidden to only show parts of images */
                    display: flex;
                    flex-direction: row;
                    align-items: center; /* Center elements vertically */
                    justify-content: center; /* Center elements horizontally */
                    gap: 12px;
                }

                @media (max-width: 768px) {
                    .image-25-parent {
                        width: 90%; /* Wider for smaller screens */
                    }

                    .image-48-parent {
                        height: 30vh; /* Adjust height for smaller screens */
                    }
                }
            `}</style>
        </div>
    );
};

export default Frame;
