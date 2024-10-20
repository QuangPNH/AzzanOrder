import React, { useState } from "react";

const PlaceOrderButton = ({ accountNo, accountName, acqId, addInfo, amount }) => {
  const [qrDataURL, setQRDataURL] = useState(null);

  const handlePlaceOrder = async () => {
    console.log("accountNo:", accountNo);
    console.log("accountName:", accountName);
    console.log("acqId:", acqId);
    console.log("addInfo:", addInfo);
    console.log("amount:", amount);

    const response = await fetch("https://api.vietqr.io/v2/generate", {
      method: "POST",
      headers: {
        "x-client-id": "<CLIENT_ID_HERE>",
        "x-api-key": "<API_KEY_HERE>",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        accountNo: accountNo,
        accountName: accountName,
        acqId: acqId,
        addInfo: addInfo,
        amount: amount,
        template: "compact",
      }),
    });

    const data = await response.json();
    console.log("data", data); // Log the response data
    setQRDataURL(data.qrDataURL);
  };

  return (
    <div>
      <button className="place-order" onClick={handlePlaceOrder}>
        Place Order
      </button>
      {qrDataURL && <img src={qrDataURL} alt="QR Code" />}
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
      `}</style>
    </div>
  );
};

export default PlaceOrderButton;
