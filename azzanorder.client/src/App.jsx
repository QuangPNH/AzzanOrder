import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import React from 'react';
import Homepage from './components/Homepage';
import LoginPage from './components/Account/LoginPage';
function App() {
  const [count, setCount] = useState(0)

    return (
        <div className="App">
            <LoginPage/>
        </div>
    );
}

export default App
