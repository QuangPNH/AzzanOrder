import React, { useEffect, useState } from "react";
import { getCookie, setCookie } from '../Account/SignUpForm/Validate';
import LogoutPage from '../Account/LogoutPage';
import { useLocation } from "react-router-dom";

export async function postOrder(amount, isCash) {
    try {
        const cartDataString = getCookie("cartData");
        if (!cartDataString) {
            throw new Error("No item in cart");
                    }

        const memberIn = getCookie('memberInfo');
        const cartData = JSON.parse(cartDataString);
        const orderDetails = cartData.map(item => {
            const { selectedIce, selectedSugar, toppings } = item.options || {};
            const descriptionParts = [];

            if (selectedIce) {
                descriptionParts.push(`${selectedIce} Ice`);
            }
            if (selectedSugar) {
                descriptionParts.push(`${selectedSugar} Sugar`);
            }
            if (toppings && toppings.length > 0) {
                descriptionParts.push(...toppings);
            }

            return {
                Quantity: item.quantity,
                MenuItemId: item.id,
                Description: descriptionParts.length > 0 ? descriptionParts.join(', ') : null,
            };
        });

                const cookieValue = getCookie('tableqr');
        const tableId = cookieValue
            ? parseInt(cookieValue.split('/')[2] ?? 0)
            : null;
        
        let order = {
                TableId: tableId,
                Cost: amount,
                OrderDetails: orderDetails,
            };

        if (memberIn) {
            order.MemberId = JSON.parse(memberIn).memberId;
        }

        if (isCash) {
            order.tax = 1;
            }

            const response = await fetch("https://localhost:7183/api/Order", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(order),
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data = await response.json();
            console.log("Order created successfully:", data);

            if (getCookie('memberInfo')) {
                await fetch(`https://localhost:7183/api/Members/UpdatePoints/${JSON.parse(getCookie('memberInfo')).memberId}/${amount / 1000}`, {
                    method: 'PUT'
                });
                deleteCookie();
}

        return data;
    } catch (error) {
        console.error("Error creating order:", error);
        throw error;
    }
}

const PlaceOrderButton = ({ amount, isTake, isCash }) => {
    const [qrDataURL, setQRDataURL] = useState(null);
    const [error, setError] = useState(null);

    const handlePlaceOrder = async () => {
        try {
            if (isCash) {
                await postOrder(amount, isCash);
            } else {
                window.location.href = "https://localhost:3002/?tableqr=" + getCookie("tableqr") + "&Price=" + amount + "&Item=OASItem&Message=Order";
            }
        } catch (error) {
            setError(error.message);
        }
        };

    return (
        <div>
            {error && <p style={{ color: "red" }}>{error}</p>}
            {qrDataURL && !isCash ? (
                <img src={`data:image/png;base64,${qrDataURL}`} alt="QR Code" className="qr-image" style={{ width: '300px' }} />
            ) : (
                <button className="place-order" onClick={handlePlaceOrder}>
                    Place Order
                </button>
            )}
            <style jsx>{`
                .place-order {
                    border-radius: 10px;
                    background: var(--Azzan-Color, #bd3326);
                    box-shadow: 0 4px 4px 0 rgba(0, 0, 0, 0.25);
                    margin-top: 49px;
                    width: 100%;
                    color: var(--sds-color-text-brand-on-brand);
                    padding: 15px 70px;
                    font: var(--sds-typography-body-font-weight-regular)
                        var(--sds-typography-body-size-medium) / 1
                        var(--sds-typography-body-font-family);
                    border: 1px solid #bd3226;
                    cursor: pointer;
                }
                .qr-image {
                    width: 100%;
                    max-width: 100%;
                    height: auto;
                    margin-top: 49px;
                }
            `}</style>
        </div>
    );
};
const deleteCookie = () => {
        setCookie('cartData', '', -1); // Call setCookie with negative days to delete
    };
export default PlaceOrderButton;