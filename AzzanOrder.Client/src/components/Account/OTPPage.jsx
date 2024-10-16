import React from "react";
import OTPWidget from "./SignUpForm/OTPWidget";

function OTPPage() {
    return (
        <div>
            <OTPWidget
                title="REGISTER MEMBER"
                icon="https://cdn.builder.io/api/v1/image/assets/TEMP/25de7a0eaa5cb726bfc455791fb3bc6f51fd4e0c724066f847d7209adeb14a84?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                placeholder="OTP"
                buttonText="Sign up"
            />
        </div>
    );
}

export default OTPPage;
