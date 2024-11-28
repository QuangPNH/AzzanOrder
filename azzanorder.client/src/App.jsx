import { useState } from 'react'
import CartButton from './components/CartButton'
import React from 'react';
import { Outlet } from 'react-router-dom';

function App() {
  const [count, setCount] = useState(0)

    return (
        <div className="App">
            <CartButton />
            <Outlet />
        </div>
    );
}

export default App
