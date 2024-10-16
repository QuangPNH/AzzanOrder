import PriceItem from './PriceItem';
import TotalPrice from './TotalPrice';
import PlaceOrderButton from './PlaceOrderButton';

const PriceCalculator = () => {
  const cartData = JSON.parse(sessionStorage.getItem("cartData")) || [];

  const calculateTotal = () => {
    let total = 0;
    cartData.forEach((item) => {
      total += item.price * item.quantity;
    });
    return total;
  };

  const getFormattedCartData = () => {
    const formattedList = cartData.map((item) => {
      if (item.label) {
        return item.label.replace(/ /g, "_");
      }
      return "";
    });
    return formattedList;
  };

  const priceItems = [
    { label: "Voucher", value: "0 đ" },
    { label: "Discount:", value: "0 đ" },
    { label: "Total:", value: `${calculateTotal()} đ`, isTotal: true },
    { label: "Pay in cash", value: "" },
  ];

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
        <TotalPrice finalPrice={calculateTotal()} discountPrice="0" />
      </div>
      <PlaceOrderButton
        accountNo="08454845087"
        accountName="DemoShop"
        acqId="970432"
        addInfo={getFormattedCartData()}
        amount={calculateTotal()}
      />
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

export default PriceCalculator;
