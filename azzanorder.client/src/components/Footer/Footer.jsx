import React, { useState, useEffect } from "react";
import ContactInfo from "./ContactInfo";
import Copyright from "./Copyright";
import { getCookie } from '../Account/SignUpForm/Validate';

const Footer = () => {
    const [backgroundColor, setBackgroundColor] = useState('#f6b5b5'); // Default background color

    useEffect(() => {
        const tableqr = getCookie("tableqr");
        if (tableqr) {
            // Fetch the background color based on the tableqr value
            const fetchBackgroundColor = async (manaId) => {
                try {
                    const url = manaId ? `https://localhost:7183/api/Promotions/GetByDescription/color?manaId=${manaId}` : `https://localhost:7183/api/Promotions/GetByDescription/color`;
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

    const contactItems = [
        {
            icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/c3aac47b727a6612500a96595a3b4d9dea0e4aefb355edcbc066da9019801d47?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8",
            text: "Hotline: 0967375045",
        },
        {
            icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/958d2628ee54563e2bd440632e1f0ac71b655aba4be5db360de18f694a6593ec?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8",
            text: "Mail: dungthhe170357@fpt.edu.vn",
        },
        {
            icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/259d79add37fc95f3d1523bdcd472dc9158a67373e6e6ef3b90165fe08b90b77?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8",
            text: "Contact",
        },
    ];

    return (
        <footer className="footer">
            <div className="footer-content">
                <ContactInfo items={contactItems} />
                <address className="address">
                    2G7G+8F3, Thạch Hoà, Thạch Thất, Hà Nội, Vietnam
                </address>
                <img
                    src="https://cdn.builder.io/api/v1/image/assets/TEMP/01b1ac975c883874a9251382d0fac643e51155a65744cab318a1afa1ef964ae8?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8"
                    alt="Company logo"
                    className="company-logo"
                />
            </div>
            <Copyright />
            <style jsx>{`
                .footer {
                    background-color: ${backgroundColor};
                    display: flex;
                    padding-top: 26px;
                    flex-direction: column;
                    overflow: hidden;
                    font-family: Inter, sans-serif;
                    width: 100vw;
                    margin-left: calc(-50vw + 50%); /* Adjust to offset the left side */
                    box-sizing: border-box;
                }
                .footer-content {
                    display: flex;
                    width: 100%;
                    flex-direction: column;
                    align-items: flex-start;
                    font-size: 16px;
                    color: #000;
                    padding: 0 43px 0 16px;
                }
                .address {
                    color: rgba(0, 0, 0, 0.6);
                    font-size: 14px;
                    margin-top: 20px;
                    font-style: normal;
                }
                .company-logo {
                    aspect-ratio: 2.14;
                    object-fit: contain;
                    width: 201px;
                    align-self: center;
                    margin-top: 20px;
                    max-width: 100%;
                }
            `}</style>
        </footer>
    );
};

export default Footer;