import React from "react";
import LoginWidget from "./SignUpForm/LoginWidget";
import Popup from 'reactjs-popup';
function LoginPage({ isOpen, handleClosePopup, onCheck }) {
    return (
        <div>
            <Popup open={isOpen} onClose={handleClosePopup}>
                <LoginWidget
                    title="LOGIN"
                    icon="https://cdn.builder.io/api/v1/image/assets/TEMP/25de7a0eaa5cb726bfc455791fb3bc6f51fd4e0c724066f847d7209adeb14a84?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                    placeholder="Enter phone number..."
                    buttonText="Log in"
                    onCheck={onCheck}
                />
            </Popup>
        </div>
    );
}

export default LoginPage;