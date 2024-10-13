/**
 * This code was generated by Builder.io.
 */
import React from "react";
import PriceItem from "./PriceItem";
import TotalPrice from "./TotalPrice";
import PlaceOrderButton from "./PlaceOrderButton";

//Tạo Function tên PriceCalculator
const PriceCalculator = () => {

  const priceItems = [
    { label: "Voucher", value: "0 đ" },
    { label: "Discount:", value: "0 đ" },
    { label: "Total:", value: "105.000 đ", isTotal: true },
    { label: "Pay in cash", value: "" },
  ];
    //Viết HTML trong function Return
    return (


    <section className="price-calculator">
      <div className="price-details">
        <div className="price-list">
          {priceItems.map((item, index) => (
            <PriceItem
              key={index}
              label={item.label}
              value={item.value}
              isTotal={item.isTotal}
            />
          ))}
        </div>
        <TotalPrice />
      </div>
      <PlaceOrderButton />
      <style jsx>{`
        .price-calculator {
          border-radius: 0;
          display: flex;
          max-width: 312px;
          flex-direction: column;
        }
        .price-details {
          display: flex;
          gap: 20px;
          justify-content: space-between;
        }
        .price-list {
          align-self: start;
          display: flex;
          flex-direction: column;
          align-items: start;
        }
      `}</style>
    </section>
  );
};


//Export Hàm PriceCalculator
export default PriceCalculator;
