import React, { useState, useEffect } from 'react';
import ItemInCart from './ItemInCart/ItemInCart';
import CartHeader from './ItemInCart/CartHeader';
import PriceCalculator from './PriceCalculator/PriceCalculator';
import { getCookie, setCookie } from './Account/SignUpForm/Validate';

function getCartData() {
    const cartDataString = getCookie("cartData");
    if (!cartDataString) {
        return [];
    }
    try {
        const cartData = JSON.parse(cartDataString);
        return cartData || [];
    } catch (error) {
        console.error("Error parsing cart data from cookie:", error);
        return [];
    }
}

const Cart = () => {
    const [cartData, setCartData] = useState(getCartData());
    const [totalPrice, setTotalPrice] = useState(0);

    useEffect(() => {
        const calculateTotal = () => {
            let total = 0;
            cartData.forEach((item) => {
                total += item.price * item.quantity;
            });
            setTotalPrice(total);
        };

        calculateTotal();
    }, [cartData]);

    const handleQuantityChange = (updatedCartData) => {
        setCartData(updatedCartData);
    };

    const itemsInCart = cartData?.map((item, index) => (
        <ItemInCart
            key={index}
            id={item.id}
            name={item.name}
            options={item.options}
            price={item.price}
            quantity={item.quantity}
            onQuantityChange={handleQuantityChange}
        />
    ));

    return (
        <div style={{ background: 'white', border: '1px solid black', borderRadius: '20px', padding: '10px', maxHeight: '550px' }}>
            <CartHeader />
            <div style={{ background: 'white', maxHeight: '250px', overflowY: 'auto' }}>
                {cartData.length > 0 ? (
                    itemsInCart
                ) : (
                    <p>Your cart is empty.</p>
                )}
            </div>
            <div>
                <PriceCalculator totalPrice={totalPrice} />
            </div>
        </div>
    );
};

export default Cart;