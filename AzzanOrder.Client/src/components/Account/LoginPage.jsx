import React from "react";
import LoginWidget from "./SignUpForm/LoginWidget";

function LoginPage() {
    return (
        <div>
            <LoginWidget
                title="LOGIN"
                icon="https://cdn.builder.io/api/v1/image/assets/TEMP/25de7a0eaa5cb726bfc455791fb3bc6f51fd4e0c724066f847d7209adeb14a84?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                placeholder="Enter phone number..."
                buttonText="Log in"
            />
        </div>
    );
}

export default LoginPage;