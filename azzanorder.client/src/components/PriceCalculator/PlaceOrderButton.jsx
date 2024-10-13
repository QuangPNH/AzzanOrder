/**
 * This code was generated by Builder.io.
 */
import React from "react";

const PlaceOrderButton = () => {
  return (
    <button className="place-order">
      Place Order
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
    </button>
  );
};

export default PlaceOrderButton;