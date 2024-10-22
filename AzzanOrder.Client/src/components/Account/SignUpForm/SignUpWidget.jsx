import React, { useState } from 'react';
import InputField from "./InputField";
import Button from "./Button";
import LoginPage from '../LoginPage';

function SignUpWidget({ title, icon, placeholder, buttonText }) {
    const [phoneNumber, setPhoneNumber] = useState('');
    const [isOTPSent, setOTPSent] = useState(false);
    const [enterOTP, setEnterOTP] = useState('');
    const [isPopupOpen, setIsPopupOpen] = useState(false);

    let memberInfo;

    const handlePhoneNumberChange = (event) => {
        setPhoneNumber(event.target.value);
    };

    const handleOTPChange = (event) => {
        setEnterOTP(event.target.value);
    };

    const handleClosePopup = () => {
        setIsPopupOpen(false);
    };

    const handleSubmitPhoneNumber = async (event) => {
        event.preventDefault();
        try {
            let response = await fetch(`https://localhost:7183/api/Member/Register/${phoneNumber}`);
            if (response.ok) {
                memberInfo = await response.json();
                sessionStorage.setItem('memberInfo', JSON.stringify(memberInfo));
                sessionStorage.setItem('savedOTP', memberInfo.memberName); // Assuming OTP is memberName
                setOTPSent(true);  // Proceed to OTP step
            } else if (response.status === 400) {
                setIsPopupOpen(true);
            }
        } catch (error) {
            console.error('An error occurred:', error);
        }
    };

    const handleSubmitOTP = async (event) => {
        event.preventDefault();
        const receivedOTP = sessionStorage.getItem('savedOTP');
        if (enterOTP === receivedOTP) {
            setCookie('memberInfo', JSON.stringify(sessionStorage.getItem('memberInfo')), 100);
            let response = await fetch(`https://localhost:7183/api/Member/`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: sessionStorage.getItem('memberInfo'),
            });
            console.log('OTP verified and member info saved', response);
            window.location.href = '';
        } else {
            console.error('Incorrect OTP');
        }
    };

    return (
        <>
            <section className="login-widget">
                <form className="login-form" onSubmit={isOTPSent ? handleSubmitOTP : handleSubmitPhoneNumber}>
                    <h2 className="register-title">{title}</h2>
                    {!isOTPSent ? (
                        <>
                            <InputField
                                value={phoneNumber}
                                onChange={handlePhoneNumberChange}
                                icon={icon}
                                placeholder={placeholder}
                            />
                            <Button type="submit" text="Send OTP" />
                        </>
                    ) : (
                        <>
                            <InputField
                                value={enterOTP}
                                onChange={handleOTPChange}
                                icon={icon}
                                placeholder="Enter OTP"
                            />
                            <Button type="submit" text="Verify OTP" />
                        </>
                    )}
                </form>
            </section>
            {isPopupOpen && (
                <LoginPage isOpen={isPopupOpen} handleClosePopup={handleClosePopup} />
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
    const expires = new Date(Date.now() + days * 864e5).toUTCString();
    document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`;
}

export default SignUpWidget;
