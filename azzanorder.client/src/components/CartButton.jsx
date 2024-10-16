import React, { useState } from 'react';
import ReactDOM from 'react-dom';
import Cart from './Cart';
import Popup from 'reactjs-popup';

const CartButton = () => {
    const [isOpen, setIsOpen] = React.useState(false);

    const handleCartButtonClick = () => {
        setIsOpen(true);
    };

    const handleClosePopup = () => {
        setIsOpen(false);
    };

    return (
        <div style={{ position: 'fixed', top: '80px', right: '20px', zIndex: '9999' }}>
            <button onClick={handleCartButtonClick} style={{ borderRadius: '50%', padding: '10px', backgroundColor: 'lightblue' }}>
                <img src="../src/assets/shoppingCart.svg" style={{ width: '20px', height: '20px' }} />
            </button>
            <Popup open={isOpen} onClose={handleClosePopup}>
                <Cart />
            </Popup>
        </div>
    );
};

export default CartButton;
