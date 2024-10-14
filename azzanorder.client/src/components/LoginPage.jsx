import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';

function LoginPage() {
  const [phoneNumber, setPhoneNumber] = useState('');
  let history = useHistory();

  if (history == null) {
    console.log('The variable is null or undefined');
  }

    const handlePhoneNumberChange = (event) => {
        setPhoneNumber(event.target.value);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            const response = await fetch(`https://localhost:7183/api/Member/phone/0934422800%20`);
            if (response.ok) {
                const memberInfo = await response.json();
                // Save the member info in session memory
                sessionStorage.setItem('memberInfo', JSON.stringify(memberInfo));

                // Redirect to Homepage.jsx
                history.push('/homepage');
            } else {
                console.error('Failed to retrieve member info');
            }
        } catch (error) {
            console.error('An error occurred:', error);
        }
    };

    return (
        <>
            <div>
                <form onSubmit={handleSubmit}>
                    <label>
                        Mobile Phone:
                        <input type="text" value={phoneNumber} onChange={handlePhoneNumberChange} />
                    </label>
                    <button type="submit">Submit</button>
                </form>
            </div>
            <div>
                <LoginWidget
                    title="REGISTER MEMBER"
                    icon="https://cdn.builder.io/api/v1/image/assets/TEMP/c3aac47b727a6612500a96595a3b4d9dea0e4aefb355edcbc066da9019801d47?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                    placeholder="Enter Phone Number..."
                    buttonText="Login" />
            </div>
        </>
    );
}

export default withRouter(LoginPage);
