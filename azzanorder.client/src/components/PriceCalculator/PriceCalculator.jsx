import { useState, useEffect } from 'react';
import PriceItem from './PriceItem';
import PlaceOrderButton from './PlaceOrderButton';

const PriceCalculator = ({ totalPrice }) => {
    const cartData = JSON.parse(sessionStorage.getItem("cartData")) || [];
    const [trueTotalPrice, setTrueTotalPrice] = useState(totalPrice);

    useEffect(() => {
        setTrueTotalPrice(totalPrice);
    }, [totalPrice]);

    const getFormattedCartData = () => {
        const formattedList = cartData.map((item) => {
            if (item.label) {
                return item.label.replace(/ /g, "_");
            }
            return "";
        });
        return formattedList;
    };

    return (
        <section className="price-calculator">
            <div className="price-details">
                <div className="price-list">
                    <PriceItem label="Total:" value={`${trueTotalPrice} đ`} isTotal={true} />
                </div>
            </div>
            <PlaceOrderButton
                addInfo={getFormattedCartData()}
                amount={trueTotalPrice}
            />
            <style jsx="true">{`
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
                    width: 90%;
                    flex-direction: column;
                    align-items: start;
                }
            `}</style>
        </section>
    );
};

export default PriceCalculator;