import React from 'react';
import ItemInCart from './ItemInCart/ItemInCart';
import PriceCalculator from './PriceCalculator/PriceCalculator';

function getCartData() {
    const cartDataString = sessionStorage.getItem("cartData");
    const cartData = JSON.parse(cartDataString);
    return cartData || [];
}

const Cart = () => {
    const cartData = getCartData();
    const itemsInCart = cartData?.map((item, index) => (
        <ItemInCart
            key={index}
            name={item?.name}
            options={item?.options}
            price={item?.price}
            quantity={item?.quantity}
        />
    ));

    return (
        <div style={{ background: 'lightgray', border: '1px solid black', borderRadius: '20px', padding: '10px', maxHeight: '400px', overflowY: 'auto' }}>
            {itemsInCart}
            <div>
                <PriceCalculator />
            </div>
        </div>
    );
};

export default Cart;