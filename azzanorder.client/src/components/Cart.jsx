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

const Cart = () => {
    const [cartData, setCartData] = useState(getCartData());
    const [voucher, setVoucher] = useState(getVoucher());
    const [totalPrice, setTotalPrice] = useState(0);
    const [discountPrice, setDiscountPrice] = useState(0);
    const [headerText, setHeaderText] = useState(false);
    useEffect(() => {
        const calculateTotal = async () => {
            let total = 0;
            //TODO: Tính toán lại giá tiền khi áp dụng voucher.
            //
            const legalCheckResults = await Promise.all(
                cartData.map((item) => checkLegal(item))
            );
            cartData.forEach((item, index) => {
                // let data = false;
                // if (cartData != '') {
                //     data = checkLegal(item);
                //     console.log(data);
                // }
                const data = legalCheckResults[index];
                if(data != false){
                    setDiscountPrice( - item.price * voucher.discount / 100 * item.quantity);
                }
                console.log(discountPrice);
                const toppingsPrice = item.options?.selectedToppings?.reduce((sum, topping) => sum + topping.price, 0) || 0;
                total += (data != false ? item.price - item.price * voucher.discount / 100 + toppingsPrice : (item.price + toppingsPrice)) * item.quantity;
            });
            setTotalPrice(total);
        };

        calculateTotal();
    }, [cartData, voucher]);

    const handleQuantityChange = (updatedCartData) => {
        setCartData(updatedCartData);
    };

    const checkLegal = async (item) => {
        try {
            const response = await fetch(`https://localhost:7183/api/Vouchers/voucherDetailId/menuItemId?voucherDetailid=${voucher.voucherDetailId}&menuItemId=${item.id}`);
            const data = await response.json();
            return data;
        } catch (error) {
            console.error('Error fetching notifications:', error);
        }
    };
    const handleSelectVoucher = (selectedItem) => {
        // const voucherSelectedList = [];
        // const found = voucherSelectedList.some(item => item.id === selectedItem.id);

        // // Nếu không tìm thấy, thêm đối tượng mới vào mảng
        // if (!found) {
        //     voucherSelectedList.push(selectedItem);
        // }
        setCookie("voucher", JSON.stringify(selectedItem), 7);
        setVoucher(selectedItem);
       
    };
    const handleTakeOutChange = (isTake) => {
        if (isTake) {
            setHeaderText(true);
        } else {
            setHeaderText(false);
        }
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
        // discount = {discount}
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
            <VoucherCart onSelectVoucher={handleSelectVoucher} />
            <div>
                <PriceCalculator totalPrice={totalPrice}  discountPrice = {discountPrice}  onTakeOutChange={handleTakeOutChange} />
            </div>
        </div>
    );
};

export default Cart;