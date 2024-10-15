import React from "react";
import InputField from "./InputField";
import Button from "./Button";

function LoginWidget({ title, icon, placeholder, buttonText }) {
    return (
        <>
            <section className="login-widget">
                <form className="login-form">
                    <h2 className="register-title">{title}</h2>
                    <InputField
                        icon={icon}
                        placeholder={placeholder}
                    />
                    <Button text={buttonText} />
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

export default LoginWidget;
