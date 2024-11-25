import React, { useState, useEffect } from 'react';
import ItemInCart from './ItemInCart/ItemInCart';
import CartHeader from './ItemInCart/CartHeader';
import PriceCalculator from './PriceCalculator/PriceCalculator';
import { getCookie, setCookie } from './Account/SignUpForm/Validate';
import VoucherCart from './VoucherCart';
import API_URLS from '../config/apiUrls';

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

function getVoucher() {
    const v = getCookie("voucher");
    if (!v) {
        return [];
    }
    try {
        const vou = JSON.parse(v);
        return vou || [];
    } catch (error) {
        console.error("Error parsing cart data from cookie:", error);
        return [];
    }
}

export const calculateTotal = async () => {
    const cartData = getCartData();
    const voucher = getVoucher();
    const id = getCookie("tableqr");

    let total = 0;
    let totalDiscount = 0;

    const legalCheckResults = await Promise.all(
        id ? cartData.map((item) => checkLegal(item, id.split('/')[1], voucher)) : cartData.map((item) => checkLegal(item, '', voucher))
    );

    cartData.forEach((item, index) => {
        const data = legalCheckResults[index];
        if (data) {
            // Calculate discount for each item and multiply by quantity
            const itemDiscount = voucher != '' ? (item.price * (voucher.discount / 100)) * item.quantity : 0;
            totalDiscount += itemDiscount; // Add to total discount
        }

        // Calculate total cart value (including discounted price if applicable)
        const toppingsPrice = item.options?.selectedToppings?.reduce((sum, topping) => sum + topping.price, 0) || 0;
        const discountedPricePerItem = data && voucher != '' ? item.price * (1 - voucher.discount / 100) : item.price;
        total += (discountedPricePerItem + toppingsPrice) * item.quantity;
    });

    return { total, totalDiscount: -totalDiscount }; // Return total and total discount
};

const checkLegal = async (item, id, voucher) => {
    try {
        const response = await fetch(API_URLS.API + `Vouchers/voucherDetailId/menuItemId?voucherDetailid=${voucher.voucherDetailId}&menuItemId=${item.id}&employeeId=${id}`);
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching notifications:', error);
    }
};

const Cart = () => {
    const [cartData, setCartData] = useState(() => {
        const initialCartData = getCartData();
        return initialCartData;
    });
    const [voucher, setVoucher] = useState(getVoucher());
    const [totalPrice, setTotalPrice] = useState(0);
    const [discountPrice, setDiscountPrice] = useState(0);
    const [headerText, setHeaderText] = useState(false);
    const id = getCookie("tableqr");
    const member = getCookie('memberInfo');
    useEffect(() => {
        const fetchTotal = async () => {
            const { total, totalDiscount } = await calculateTotal();
            setTotalPrice(total);
            setDiscountPrice(totalDiscount);
        };

        fetchTotal();
    }, [cartData, voucher, id]);

    const handleQuantityChange = (updatedCartData) => {
        setCartData(updatedCartData);
    };

    const handleSelectVoucher = (selectedItem) => {
        setVoucher(selectedItem);
    };

    const handleTakeOutChange = (isTake) => {
        setHeaderText(isTake);
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
        <div style={{ background: 'white', border: '1px solid black', borderRadius: '20px', padding: '10px', maxHeight: '700px', width: '320px', overflowY: 'auto' }}>
            <CartHeader headerText={headerText} />
            <div style={{ background: 'white', maxHeight: '250px', overflowY: 'auto' }}>
                {cartData.length > 0 ? (
                    itemsInCart
                ) : (
                    <p>Your cart is empty.</p>
                )}
            </div>
            {member && 
            <VoucherCart onSelectVoucher={handleSelectVoucher} />
            }
            <div>
                <PriceCalculator totalPrice={totalPrice} discountPrice={discountPrice} onTakeOutChange={handleTakeOutChange} />
            </div>
        </div>
    );
};

export default Cart;