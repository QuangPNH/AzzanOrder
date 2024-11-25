import React, { useState, useEffect } from "react";
import ContactInfo from "./ContactInfo";
import Copyright from "./Copyright";
import { getCookie } from '../Account/SignUpForm/Validate';
import API_URLS from "../../config/apiUrls";

const Footer = () => {
    const [backgroundColor, setBackgroundColor] = useState('#f6b5b5'); // Default background color
    const [logoSrc, setLogoSrc] = useState('');
    const [hotLine, setHotline] = useState('');
    const [mail, setMail] = useState('');
    const [contact, setContact] = useState('');

    const tableqr = getCookie("tableqr");
    const fetchData = async (endpoint, manaId, setData) => {
        try {
            const url = manaId ? API_URLS.API + `Promotions/GetByDescription/${endpoint}?manaId=${manaId}` : API_URLS.API + `Promotions/GetByDescription/${endpoint}`;
            const response = await fetch(url);
            if (response.ok) {
                const data = await response.json();
                setData(data);
            } else {
                throw new Error("Network response was not ok");
            }
        } catch (error) {
            console.error(`Failed to fetch ${endpoint}:`, error);
        }
    };

    useEffect(() => {
        if (tableqr) {
            const manaId = tableqr.split('/')[1];
            fetchData('color', manaId, (data) => setBackgroundColor(data.description.split('/')[1]));
            fetchData('logo', manaId, (data) => setLogoSrc(data.image));
            fetchData('hotLine', manaId, (data) => setHotline(data.description.split('/')[1]));
            fetchData('mail', manaId, (data) => setMail(data.description.split('/')[1]));
            fetchData('contact', manaId, (data) => setContact(data.description.split('/')[1]));
        }
    }, [tableqr]);

    const contactItems = [
        hotLine && {
            icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/c3aac47b727a6612500a96595a3b4d9dea0e4aefb355edcbc066da9019801d47?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8",
            text: `Hotline: ${hotLine}`,
        },
        mail && {
            icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/958d2628ee54563e2bd440632e1f0ac71b655aba4be5db360de18f694a6593ec?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8",
            text: `Mail: ${mail}`,
        },
        contact && {
            icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/259d79add37fc95f3d1523bdcd472dc9158a67373e6e6ef3b90165fe08b90b77?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8",
            text: "Contact",
        },
    ].filter(item => item && item.text);

    return (
        <footer className="footer">
            <div className="footer-content">
                {contactItems && <ContactInfo items={contactItems} />}
                {contact && (
                    <address className="address">
                        {contact}
                    </address>
                )}
                <img
                    src={logoSrc || "https://s6.imgcdn.dev/gl4Iv.png"}
                    alt="Company logo"
                    className="company-logo"
                    loading="lazy"
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