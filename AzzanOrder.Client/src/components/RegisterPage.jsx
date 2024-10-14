import React, { useState } from 'react';
import { useHistory, withRouter, Redirect } from 'react-router-dom';

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
      const response = await fetch(`https://localhost:7183/api/Member/phone/${phoneNumber}`);
      if (response.ok) {
        const memberInfo = await response.json();
        // Save the member info in session memory
        sessionStorage.setItem('memberInfo', JSON.stringify(memberInfo));
        
        
        // Redirect to Homepage.jsx
        history('/HomePage');
      } else {
        console.error('Failed to retrieve member info');
      }
    } catch (error) {
      console.error('An error occurred:', error);
    }
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <label>
          Mobile Phone:
          <input type="text" value={phoneNumber} onChange={handlePhoneNumberChange} />
        </label>
        <button type="submit">Submit</button>
      </form>
    </div>
  );
}

export default LoginPage;
