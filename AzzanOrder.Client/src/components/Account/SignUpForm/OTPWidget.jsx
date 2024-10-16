import React, { useState } from 'react';
import InputField from "./InputField";
import Button from "./Button";

function OTPWidget({ title, icon, placeholder, buttonText }) {
  const [enterOTP, setEnterOTP] = useState('');
  let receivedOTP;

  const handleEnterOTPChange = (event) => {
    setEnterOTP(event.target.value);
  };


  const handleOTP = async (event) => {
    event.preventDefault();
    receivedOTP = sessionStorage.getItem('savedOTP');
    console.log('Noooooo ' + sessionStorage.getItem('savedOTP'));
    if (enterOTP == receivedOTP){
      setCookie("memberInfo", sessionStorage.getItem('memberInfo'), 100);
      let response = await fetch(`https://localhost:7183/api/Member/`, {
        method: 'POST', // Specify the request method
        headers: {
            'Content-Type': 'application/json', // Inform the server that you're sending JSON data
        },
        body: sessionStorage.getItem('memberInfo'), // Convert JavaScript object to JSON
      });
      console.log(response);
    }
  };

  

    return (
        <>
            <section className="login-widget">
                <form className="login-form" onSubmit={handleOTP}>
                    <h2 className="register-title">{title}</h2>
                    <InputField value={enterOTP} onChange={handleEnterOTPChange}
                        icon={icon}
                        placeholder={placeholder}/>
                    <Button type="submit" text={buttonText} />
                </form>
            </section>
            <style jsx>{`
        .login-widget {
          border-radius: 0;
          display: flex;
          max-width: 328px;
          flex-direction: column;
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


function getCookie(name) {
  const value = `; ${document.cookie}`; // Add a leading semicolon for easier parsing
  const parts = value.split(`; ${name}=`); // Split the cookie string to find the desired cookie
  if (parts.length === 2) return decodeURIComponent(parts.pop().split(';').shift()); // Return the cookie value
}


export default OTPWidget;