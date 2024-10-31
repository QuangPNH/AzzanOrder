import React, { useState, useEffect } from 'react';
import ItemInCart from './ItemInCart/ItemInCart';
import CartHeader from './ItemInCart/CartHeader';
import PriceCalculator from './PriceCalculator/PriceCalculator';
import { getCookie, setCookie } from './Account/SignUpForm/Validate';
import VoucherCart from './VoucherCart';

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

function getVoucherList() {
    const voucherListString = getCookie("voucherList");
    if (!voucherListString) {
        return [];
    }
    try {
        const voucherList = JSON.parse(voucherListString);
        return voucherList || [];
    } catch (error) {
        console.error("Error parsing cart data from cookie:", error);
        return [];
    }
}

const Cart = () => {
    const [cartData, setCartData] = useState(getCartData());
    const [voucherList, setVoucherList] = useState(getVoucherList());  
    const [totalPrice, setTotalPrice] = useState(0);

    useEffect(() => {
        const calculateTotal = () => {
            let total = 0;
            // console.log(voucherList, "voucher ne");
            //TODO: Tính toán lại giá tiền khi áp dụng voucher.
            //
            cartData.forEach((item) => {
                const toppingsPrice = item.options?.selectedToppings?.reduce((sum, topping) => sum + topping.price, 0) || 0;
                total += (item.price + toppingsPrice) * item.quantity;
            });
            setTotalPrice(total);
        };

        calculateTotal();
    }, [cartData]);

    const handleQuantityChange = (updatedCartData) => {
        setCartData(updatedCartData);
    };

    const handleSelectVoucher = (selectedItem) => {
        const voucherSelectedList = [];
        const found = voucherSelectedList.some(item => item.id === selectedItem.id);

        // Nếu không tìm thấy, thêm đối tượng mới vào mảng
        if (!found) {
            voucherSelectedList.push(selectedItem);
        }
        setCookie("voucherList", JSON.stringify(voucherSelectedList), 7);
        getCartData();
        
        console.log(selectedItem, "cart", cartData);
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
        <div style={{ background: 'white', border: '1px solid black', borderRadius: '20px', padding: '10px', maxHeight: '700px',width: '320px', overflowY: 'auto' }}>

            <CartHeader />
            <div style={{ background: 'white', maxHeight: '250px', overflowY: 'auto' }}>
                {cartData.length > 0 ? (
                    itemsInCart
                ) : (
                    <p>Your cart is empty.</p>
                )}
            </div>
            <VoucherCart onSelectVoucher={handleSelectVoucher} />
            <div>
                <PriceCalculator totalPrice={totalPrice} />
            </div>
        </div>
    );
};

export default Cart;