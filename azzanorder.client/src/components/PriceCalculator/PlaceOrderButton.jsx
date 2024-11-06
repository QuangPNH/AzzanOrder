import React, { useEffect, useState } from "react";
import { getCookie, setCookie } from '../Account/SignUpForm/Validate';
import LogoutPage from '../Account/LogoutPage';
import { useLocation } from "react-router-dom";

const PlaceOrderButton = ({ amount, isTake, isCash }) => {
    const [qrDataURL, setQRDataURL] = useState(null);
    const [error, setError] = useState(null);
    const manaid= getCookie("tableqr");

    const handlePlaceOrder = async () => {
        const cartDataString = getCookie("cartData");
        if (!cartDataString) {
            setError("No item in cart");
            return;
        }

        const memberIn = getCookie('memberInfo');

        const cartData = JSON.parse(cartDataString);
        const orderDetails = cartData.map(item => {
            const { selectedIce, selectedSugar, toppings } = item.options || {};
            const descriptionParts = [];

            if (selectedIce) {
                descriptionParts.push(`${selectedIce}% Ice`);
            }
            if (selectedSugar) {
                descriptionParts.push(`${selectedSugar}% Sugar`);
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

        let order = null;
        const tableId = parseInt(getCookie('tableqr').split('/')[2]);
        //const tableId = parseInt(manaid.split('/')[2]);
        if (memberIn) {
            order = {
                TableId: tableId,
                Cost: amount,
                MemberId: JSON.parse(memberIn).memberId,
                OrderDetails: orderDetails,
            };
        } else {
            order = {
                TableId: tableId,
                Cost: amount,
                OrderDetails: orderDetails,
            };
        }

        if (isCash) {
            order.tax = 1;
            await postOrder(order);
        }else{
            //await fetchQRAndPostOrder(order, manaid.split('/')[1]);
            //window.location.href = "https://localhost:3002/?tableqr=QR_002/1&Price=1000&Item=OASItem&Message=Order";
            window.location.href = "https://localhost:3002/?tableqr=" + getCookie("tableqr") + "&Price=" + order.Cost + "&Item=OASItem&Message=Order";
        }
    };

    const fetchQRAndPostOrder = async (order, manaid) => {
        try {
            console.log(manaid, 'id');
            const response = await fetch(`https://localhost:7183/api/Order/QR/${amount}?employeeId=${manaid}`, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                },
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data = await response.json();

            if (data && data.base64Image) {
                setQRDataURL(data.base64Image);
            } else {
                console.error("QR code data is missing in the response:", data);
                throw new Error("QR code data is missing in the response");
            }

            await postOrder(order);
        } catch (error) {
            console.error("Error fetching QR code:", error);
        }
    };

    const postOrder = async (order) => {
        try {
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
        } catch (error) {
            console.error("Error creating order:", error);
        }
    };

    const deleteCookie = () => {
        setCookie('cartData', '', -1); // Call setCookie with negative days to delete
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

export default PlaceOrderButton;