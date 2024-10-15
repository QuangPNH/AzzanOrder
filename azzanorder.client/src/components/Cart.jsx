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
        <div>
            {itemsInCart}
            <div>
                <PriceCalculator />
            </div>
        </div>
    );
};

export default Cart;