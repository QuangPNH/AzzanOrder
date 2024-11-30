import React, { useState } from 'react';
import Button from "./Button";
import { setCookie } from './Validate';
function YNWidget({ title, onClose, func }) {

    const handleSubmit = async () => {
        try {
            setCookie('voucher', '' , -1);
            onClose();
            if (func) {
                await func();
            }
        } catch (error) {
            console.error('An error occurred:', error);
        }
    };

    const handleCancel = () => {
        onClose(); // Close the pop-up
    };

    return (
        <>
            <section className="login-widget">
                <form className="login-form" onSubmit={handleSubmit}>
                    <h2 className="register-title">{title}</h2>
                    <div>
                        <button className="submit-button" type="submit">Yes</button>
                        <button className="submit-button" type="reset"onClick={handleCancel}>Cancel </button>
                    </div>
                </form>
            </section>
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
        .submit-button {
          align-self: center;
          border-radius: 15px;
          border: none;
          background: var(--primary-color);
          margin-top: 15px;
          width: 80px;
          min-height: 35px;
          padding: 5px;
          color: #fff;
          font-size: 16px;
          font-weight: 20;
          text-align: center;
          cursor: pointer;
          transition: background-color 0.3s ease-in-out;
        }
      `}</style>
        </>
    );
}

export default YNWidget;