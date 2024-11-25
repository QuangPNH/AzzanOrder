import React, { useState } from 'react';
import InputField from "./InputField";
import Button from "./Button";
import SignUpPage from "../SignUpPage";
import API_URLS from '../../../config/apiUrls';

function LoginWidget({ title, icon, placeholder, buttonText, onCheck }) {
    const [phoneNumber, setPhoneNumber] = useState('');
    const [isPopupOpen, setIsPopupOpen] = useState(false);

    const handlePhoneNumberChange = (event) => {
        setPhoneNumber(event.target.value);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            let response = await fetch(API_URLS.API + `Member/Phone/${phoneNumber}`);
            if (response.ok) {
                const memberInfo = await response.json();
                memberInfo.image = "null";
                setCookie('memberInfo', JSON.stringify(memberInfo), 100);
                window.location.href = '';
            } else if (response.status === 404) {
                const result = 'fail';
                onCheck(result);
            }
        } catch (error) {
            console.error('An error occurred:', error);
        }
    };

    const handleClosePopup = () => {
        setIsPopupOpen(false);
    };

    return (
        <>
            <section className="login-widget">
                <form className="login-form" onSubmit={handleSubmit}>
                    <h2 className="register-title">{title}</h2>
                    <InputField
                        value={phoneNumber}
                        onChange={handlePhoneNumberChange}
                        icon={icon}
                        placeholder={placeholder}
                    />
                    <Button type="submit" text={buttonText} />
                </form>
            </section>
            {isPopupOpen && (
                <SignUpPage isOpen={isPopupOpen} handleClosePopup={handleClosePopup} />
            )}
            <style jsx>{`
        .login-widget {
          border-radius: 0;
          display: flex;
          max-width: 328px;
          flex-direction: row-reverse;
          font-family: Inter, sans-serif;
          color: #000;
        }
        .login-form {
          border-radius: 31px;
          background-color: #fff;
          box-shadow: 0 0 10px rgba(0, 0, 0, 0.25);
          display: flex;
          width: 100%;
          flex-direction: column;
          align-items: center;
          padding: 21px 26px;
        }
        .register-title {
          font-size: 20px;
          font-weight: 700;
          text-align: center;
        }
      `}</style>
        </>
    );
}

function setCookie(name, value, days) {
    const expires = new Date(Date.now() + days * 864e5).toUTCString(); // Calculate expiration date
    document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`; // Set cookie
}

export default LoginWidget;