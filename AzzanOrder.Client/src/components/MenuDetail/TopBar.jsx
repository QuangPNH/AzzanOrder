import React from "react";

export default function TopBar({ closeModal }) {
    return (
        <>
            <div className="top-bar">
                <img
                    loading="lazy"
                    src="https://cdn.builder.io/api/v1/image/assets/TEMP/32f3b78e1921df6d02584f941cbe40ab28cf7713789ab181fc6d080c5a20736c?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                    className="logo1"
                    alt="Logo1"
                    onClick={closeModal}
                />
                <h1 className="title">Drink Details</h1>
            </div>
            <style jsx>{`
                .top-bar {
                    display: flex;
                    align-items: center; /* Center vertically */
                    justify-content: space-between; /* Distribute space between elements */
                    max-width: 100%; /* Allow it to take full width */
                    padding: 0 10px; /* Add some padding to prevent sticking to edges */
                    box-sizing: border-box; /* Include padding in width calculation */
                    color: #000;
                    font: 700 16px Inter, sans-serif;
                }
                .logo1 {
                    cursor: pointer;
                    aspect-ratio: 1.04;
                    object-fit: contain;
                    object-position: center;
                    width: 25px;
                    margin-right: 15px; /* Add space between logo and title */
                }
                .title {
                    flex: 1; /* Take the remaining space */
                    text-align: center; /* Center text */
                    font-size: 16px;
                }
            `}</style>
        </>
    );
}
