import React, { useState } from "react";
import { getCookie } from '../Account/SignUpForm/Validate';

const PlaceOrderButton = ({ amount }) => {
    const [qrDataURL, setQRDataURL] = useState(null);
    const [error, setError] = useState(null);

    const handlePlaceOrder = async () => {
        console.log("amount:", amount);

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
            await postOrder();
        } catch (error) {
            console.error("Error fetching QR code:", error);
            setError(error.message);
        }
    };

    const postOrder = async () => {
        const cartDataString = getCookie("cartData");
        if (!cartDataString) {
            setError("No item in cart");
        }else
        try {
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

            const order = {
                OrderDate: new Date(),
                TableId: parseInt(getCookie('tableqr').split('/')[2]),
                Cost: amount,
                MemberId: JSON.parse(getCookie('memberInfo')).memberId,
                OrderDetails: orderDetails,
            };

            const response = await fetch("https://localhost:7183/api/Order", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(order),
            });

            const updateMemberPoints = await fetch(`https://localhost:7183/api/Members/UpdatePoints/${JSON.parse(getCookie('memberInfo')).memberId}/${amount / 1000}`, {
                method: 'PUT'
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data = await response.json();
            console.log("Order created successfully:", data);
        } catch (error) {
            console.error("Error creating order:", error);
            setError(error.message);
        }
    };

    return (
        <div>
            {error && <p style={{ color: "red" }}>{error}</p>}
            {qrDataURL ? (
                <img src={`data:image/png;base64,${qrDataURL}`} alt="QR Code" className="qr-image" />
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