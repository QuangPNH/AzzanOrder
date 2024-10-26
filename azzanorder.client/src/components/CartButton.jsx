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
        <div style={{ position: 'fixed', bottom: '80px', right: '20px', zIndex: '9999' }}>
            <button onClick={handleCartButtonClick} style={{ borderRadius: '100%', padding: '12px', backgroundColor: '#BD3326', cursor: 'pointer' }}>
                <img src="../src/assets/shoppingCart.svg" style={{ width: '20px', height: '20px' }} />
            </button>
            <Popup open={isOpen} onClose={handleClosePopup}>
                <Cart />
            </Popup>
        </div>
    );
};

export default CartButton;
