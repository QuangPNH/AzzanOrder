import React, { useState } from "react";
import { getCookie, setCookie } from '../Account/SignUpForm/Validate';
import LogoutPage from '../Account/LogoutPage';

const PlaceOrderButton = ({ amount }) => {
    const [qrDataURL, setQRDataURL] = useState(null);
    const [error, setError] = useState(null);
    const [showLogout, setLogout] = useState(false);
    const [proceedWithOrder, setProceedWithOrder] = useState(true);

    const handlePlaceOrder = async () => {
        console.log("amount:", amount);

        const cartDataString = getCookie("cartData");
        if (!cartDataString) {
            setError("No item in cart");
            return;
        }

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
        if (getCookie('memberInfo')) {
            order = {
                TableId: parseInt(getCookie('tableqr').split('/')[2]),
                Cost: amount,
                MemberId: JSON.parse(getCookie('memberInfo')).memberId,
                OrderDetails: orderDetails,
            };
        } else {
            order = {
                TableId: parseInt(getCookie('tableqr').split('/')[2]),
                Cost: amount,
                OrderDetails: orderDetails,
            };
        }
        await fetchQRAndPostOrder(order);
    };

    const fetchQRAndPostOrder = async (order) => {
        try {
            const response = await fetch(`https://localhost:7183/api/Order/QR/${amount}`, {
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

    //const handleClosePopup = (proceed) => {
    //    setLogout(false);
    //    if (proceed) {
    //        setProceedWithOrder(true);
    //        handlePlaceOrder();
    //    }
    //};

    return (
        <div>
            {error && <p style={{ color: "red" }}>{error}</p>}
            {qrDataURL ? (
                <img src={`data:image/png;base64,${qrDataURL}`} alt="QR Code" className="qr-image" style={{ width: '300px' }} />
            ) : (
                <button className="place-order" onClick={handlePlaceOrder}>
                    Place Order
                </button>
            )}
            {/*{showLogout && (*/}
            {/*    <LogoutPage isOpen={showLogout} handleClosePopup={handleClosePopup} func={deleteCookie} title={"You will get point for this if you log in"} />*/}
            {/*)}*/}
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