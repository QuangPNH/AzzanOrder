import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import CartButton from './components/CartButton'
import React from 'react';

//import LoginPage from './components/Account/LoginPage';
import Homepage from './components/Homepage';

import LoginPage from './components/Account/LoginPage';
function App() {
  const [count, setCount] = useState(0)

    return (
        <div className="App">
            <Homepage />

            <CartButton />
        </div>
    );
}

export default App
