import React, { useState } from "react";

const PlaceOrderButton = ({ accountNo, accountName, acqId, addInfo, amount }) => {
  const [qrDataURL, setQRDataURL] = useState(null);
  const [error, setError] = useState(null);

  const handlePlaceOrder = async () => {
    console.log("amount:", amount);
    console.log("addInfo:", addInfo);

    try {
      const response = await fetch(`https://localhost:7183/api/Order/QR/${amount}/${addInfo}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const data = await response.json();
      console.log("data", data); // Log the response data

      // Assuming the base64 string is in the 'qrCode' property
      if (data && data.base64Image) {
        console.log("Base64 Image Data:", data.base64Image); // Log the base64 image data
        setQRDataURL(data.base64Image);
      } else {
        console.error("QR code data is missing in the response:", data);
        throw new Error("QR code data is missing in the response");
      }
    } catch (error) {
      console.error("Error fetching QR code:", error);
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
