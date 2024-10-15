import React, { useState } from 'react';


function RegisterPage() {
  const [phoneNumber, setPhoneNumber] = useState('');
  const [enterOTP, setEnterOTP] = useState('');
  let memberInfo;
  let receivedOTP;


  const handlePhoneNumberChange = (event) => {
    setPhoneNumber(event.target.value);
  };

  const handleEnterOTPChange = (event) => {
    setEnterOTP(event.target.value);
  };



  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      let response = await fetch(`https://localhost:7183/api/Member/Register/${phoneNumber}`);
      if (response.ok) {
        memberInfo = await response.json();
        sessionStorage.setItem('memberInfo', JSON.stringify(memberInfo));
        sessionStorage.setItem('savedOTP', memberInfo.memberName);
        console.log('Yeeeeee ' + sessionStorage.getItem('savedOTP'));
        console.log('Yeeeeee ' + sessionStorage.getItem('memberInfo'));
      } else {
        console.error('Failed to retrieve member info');
      }
    } catch (error) {
      console.error('An error occurred:', error);
    }
  };



  const handleOTP = async (event) => {
    event.preventDefault();
    receivedOTP = sessionStorage.getItem('savedOTP');
    console.log('Noooooo ' + sessionStorage.getItem('savedOTP'));
    if (enterOTP == receivedOTP){
      console.log('Ghet anh Minh');
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
    <div>
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
      <form onSubmit={handleOTP}>
        <label>
          OTP:
          <input type="text" value={enterOTP} onChange={handleEnterOTPChange} />
        </label>
        <button type="submit">Submit</button>
      </form>
      </div>
    </div>
  );

  
}

export default RegisterPage;
