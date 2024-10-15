import React from 'react';
import Cart from './Cart';

const CartButton = () => {
  const handleCartButtonClick = () => {
    // Open Cart.jsx as a popup
    const cartPopup = window.open('', 'Cart', 'width=400,height=400');
    cartPopup.document.body.innerHTML = '<div id="cart-root"></div>';

    // Render Cart component inside the popup
    ReactDOM.render(<Cart />, cartPopup.document.getElementById('cart-root'));
  };

  return (
    <button onClick={handleCartButtonClick}>
      Open Cart
    </button>
  );
};

export default CartButton;
