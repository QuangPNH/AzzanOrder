import React from "react";
import SignUpWidget from "./SignUpForm/SignUpWidget";


function SignUpPage() {
    return (
        <div>
            <SignUpWidget
                title="REGISTER MEMBER"
                icon="https://cdn.builder.io/api/v1/image/assets/TEMP/c3aac47b727a6612500a96595a3b4d9dea0e4aefb355edcbc066da9019801d47?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                placeholder="Enter phone number..."
                buttonText="Send OTP"
            />
        </div>
    );
}
export default SignUpPage;