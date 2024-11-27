import React, { useState, useEffect, useRef } from "react";
import MenuSeparator from "./MenuSeparator";
import QuantityControl from "./QuantityControl";
import AmountBar from '../MenuDetail/AmountBar';
import CustomItem from '../MenuDetail/CustomItem';
import API_URLS from "../../config/apiUrls";

import { getCookie, setCookie } from '../Account/SignUpForm/Validate';

const ItemInCart = ({ id, name, options, price, quantity, onQuantityChange }) => {
    const [selectedSugar, setSelectedSugar] = useState(options?.selectedSugar); // State for selected sugar amount
    const [selectedIce, setSelectedIce] = useState(options?.selectedIce); // State for selected ice amount
    const [selectedToppings, setSelectedToppings] = useState(options?.selectedToppings || []); // State for selected toppings
    const [currentQuantity, setCurrentQuantity] = useState(quantity);
    const isFirstRender = useRef(true);
    const isAfterDelete = useRef(false);

    useEffect(() => {
        if (isFirstRender.current) {
            isFirstRender.current = false;
            return;
        }
        if (isAfterDelete.current) {
            isFirstRender.current = false;
            return;
        }
        const updateCartData = async () => {
            let cartData = [];
            let voucher = null;

            if (getCookie("cartData")) {
                cartData = JSON.parse(getCookie("cartData"));
            }

            if (getCookie("voucher")) {
                voucher = JSON.parse(getCookie('voucher'));
            }

            const updatedCartData = await Promise.all(cartData.map(async (item) => {
                let discountAmount = 0; // Default discount is 0
                const emp =  JSON.parse(getCookie('memberInfo'));
                if (item.id === id && JSON.stringify(item.options) === JSON.stringify(options)) {
                    // Check if the item is valid with the voucher
                    const data = await checkLegal(item, emp.memberId, voucher);

                    // If `data` is valid (e.g., `data` is not empty or has necessary properties)
                    if (data == true) {
                        discountAmount = item.price * (voucher.discount / 100);

                    }

                    return {
                        ...item,
                        quantity: currentQuantity,
                        options: {
                            ...item.options,
                            selectedSugar,
                            selectedIce,
                            selectedToppings
                        },
                        discount: discountAmount
                    };
                }
                return item;
            }));

            setCookie("cartData", JSON.stringify(updatedCartData), 0.02);
            onQuantityChange(updatedCartData);
        };

        updateCartData();
        
    }, [currentQuantity, selectedSugar, selectedIce, selectedToppings, id]);


    const handleQuantityChange = (newQuantity) => {
        setCurrentQuantity(newQuantity);
    };

    const checkLegal = async (item, memberId, voucher) => {
        try {
            const response = await fetch(API_URLS.API + `Vouchers/voucherDetailId/menuItemId?voucherDetailid=${voucher.voucherDetailId}&menuItemId=${item.id}&employeeId=${memberId}`);
            const data = await response.json();
            return data;
        } catch (error) {
            console.error('Error fetching notifications:', error);
        }
    };

    const handleDelete = () => {
        const cartData = JSON.parse(getCookie("cartData"));
        const updatedCartData = cartData.filter((item) => item.id !== id);
        setCookie("cartData", JSON.stringify(updatedCartData), 0.02);
        onQuantityChange(updatedCartData);
        isFirstRender.current = true;
    };

    const calculateTotalPrice = () => {
        const toppingsPrice = selectedToppings.reduce((total, topping) => total + topping.price, 0);
        return (price + toppingsPrice) * currentQuantity;
    };

    return (
        <section className="item-in-cart">
            <article className="cart-item">
                <div className="cart-item-content">
                    <div className="item-header">
                        <img
                            src="https://cdn.builder.io/api/v1/image/assets/TEMP/febac510c6d584a90466fa7c52c5724906fbbf8624bfa03ec21ea8114b229195?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8"
                            alt=""
                            className="item-icon"
                        />
                        <h3 className="item-name">{name}</h3>
                        <img
                            src="https://cdn.builder.io/api/v1/image/assets/TEMP/fbbe8e49109e7ba499ad0129a45932bcb03213d85b1c078668fd9d88bca09d81?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8"
                            alt="Delete item"
                            className="delete-icon"
                            onClick={handleDelete}
                        />
                    </div>
                    <div className="item-details">
                        {options && (
                            <>
                                <div className="item-option compact">
                                    <CustomItem title="Lượng đường:" compact />
                                    <AmountBar selectedValue={selectedSugar} onStepClick={setSelectedSugar} compact />
                                </div>
                                <div className="item-option compact">
                                    <CustomItem title="Lượng đá:" compact />
                                    <AmountBar selectedValue={selectedIce} onStepClick={setSelectedIce} compact />
                                </div>
                                <div className="item-option compact">
                                    <CustomItem title="Toppings:" compact />
                                    <p>+{selectedToppings.map(topping => `${topping.name}`).join(', ')}</p>
                                </div>
                            </>
                        )}
                    </div>
                </div>
                <div className="cart-item-footer">
                    <p className="item-price">{calculateTotalPrice()}đ</p>
                    <QuantityControl
                        quantity={currentQuantity}
                        onQuantityChange={handleQuantityChange}
                    />
                </div>
                <style jsx>{`
                    .cart-item {
                        display: flex;
                        width: 100%;
                        flex-direction: column;
                    }
                    .cart-item-content {
                        display: flex;
                        flex-direction: column;
                        width: 100%;
                        color: rgba(30, 30, 30, 0.6);
                        font: 400 12px/1.4 Inter, sans-serif;
                    }
                    .item-header {
                        display: flex;
                        align-items: center;
                        justify-content: space-between;
                        margin-bottom: 5px;
                        width: 100%;
                    }
                    .item-icon {
                        aspect-ratio: 1;
                        object-fit: contain;
                        object-position: center;
                        width: 20px;
                    }
                    .item-name {
                        flex-grow: 1;
                        margin: 0 10px;
                    }
                    .delete-icon {
                        aspect-ratio: 1.12;
                        object-fit: contain;
                        object-position: center;
                        width: 27px;
                        cursor: pointer;
                    }
                    .item-details {
                        display: flex;
                        flex-direction: column;
                    }
                    .item-option {
                        display: flex;
                        flex-direction: column;
                    }
                    .item-option.compact {
                        flex-direction: row;
                        justify-content: space-between;
                        align-items: center;
                        gap: 8px;
                    }
                    .cart-item-footer {
                        align-self: end;
                        display: flex;
                        margin-top: 22px;
                        width: 100%;
                        max-width: 241px;
                        gap: 20px;
                        justify-content: space-between;
                    }
                    .item-price {
                        color: var(--Text-Default-Secondary, #757575);
                        text-decoration-line: underline;
                        margin: auto 0;
                        font: var(--sds-typography-body-font-weight-regular)
                            var(--sds-typography-body-size-small) / 1.4
                            var(--sds-typography-body-font-family);
                    }
                    .item-in-cart {
                        border-radius: 0;
                        display: flex;
                        max-width: 284px;
                        flex-direction: column;
                    }
                `}</style>
            </article>
            <MenuSeparator />
        </section>
    );
};

export default ItemInCart;