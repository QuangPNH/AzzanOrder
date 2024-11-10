import React, { useEffect, useState } from "react";
import { getCookie, setCookie } from '../Account/SignUpForm/Validate';
import LogoutPage from '../Account/LogoutPage';
import { useLocation } from "react-router-dom";
import { table } from "framer-motion/client";

async function AddPoint (memberId, amount){
    const response = await fetch(`https://localhost:7183/api/Member/UpdatePoints/memberId/point?memberId=${memberId}&point=${amount/1000}`);
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    const response1 = await fetch(`https://localhost:7183/api/MemberVouchers/memberId/voucherDetailId?memberId=${memberId}&voucherDetailId=${JSON.parse(getCookie('voucher')).voucherDetailId}`);
    if (!response1.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data1 = await response1.json();
    const response2 = await fetch(`https://localhost:7183/api/MemberVouchers/Delete/${data1.memberVoucherId}`, {
        method : "DELETE",
    });
    if (!response2.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
};

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

        if (memberIn) {
            AddPoint(JSON.parse(memberIn).memberId, amount);

        }
        deleteCookie('cartData');
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
                const bank = {
                    PAYOS_CLIENT_ID: "c3ab25b3-a36b-44bc-8a15-0a2406de4642",
                    PAYOS_API_KEY: "5e6bd626-9e42-4e4b-8b8a-6858d1f7615a",
                    PAYOS_CHECKSUM_KEY: "bf84f6e8550cecf8ef0cf8c0b3eca70a37dd2ceb610efb6c3cc01d25148d637b"
                };
                //Set owner PayOs
                fetch("https://localhost:3002/?tableqr=" + getCookie("tableqr") + "&Price=" + amount + "&Item=OASItem&Message=Order", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(bank)
                }).then(response => response.text()) // Expect a text response (the URL as a string)
                .then(url => {
                    // Redirect to the URL returned by the server
                    window.location.href = url;
                });
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
    setCookie('voucher', '', -1);
};
export default PlaceOrderButton;