import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import React from 'react';

//import LoginPage from './components/Account/LoginPage';
import Homepage from './components/Homepage';

import LoginPage from './components/Account/LoginPage';
import Cart from './components/Cart';



function App() {
  const [count, setCount] = useState(0)

    return (
        <div className="App">
            <Cart/>
        </div>
    );
}

export default App
