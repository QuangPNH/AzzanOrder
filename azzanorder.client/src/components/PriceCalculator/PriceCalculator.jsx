import { useState, useEffect } from 'react';
import PriceItem from './PriceItem';
import ItemCheck from './ItemCheck';
import PlaceOrderButton from './PlaceOrderButton';
import { getCookie } from '../Account/SignUpForm/Validate';

const PriceCalculator = ({ discountPrice, totalPrice, onTakeOutChange }) => {
    const [trueTotalPrice, setTrueTotalPrice] = useState(totalPrice);
    const [error, setError] = useState(null);
    const [isTake, setIsTake] = useState(false);
    const [isCash, setIsCash] = useState(false);

    useEffect(() => {
        const cartDataString = getCookie("memberInfo");
        if (!cartDataString) {
            setError("Order while login will give you points");
        }
        setTrueTotalPrice(totalPrice);
    }, [totalPrice]);

    const handleTakeOutChange = (value) => {
        setIsTake(value);
        onTakeOutChange(value);
    };

    return (
        <section className="price-calculator">
            <div className="price-details">
                <div className="price-list">
                    
                    <PriceItem label="Total:" value={`${trueTotalPrice - discountPrice} đ`} isTotal={true} />
                    {getCookie("memberInfo") ? <PriceItem label="Discount:" value={`${discountPrice} đ`} isTotal={true} /> : <></>}     
                    <PriceItem label="Final:" value={`${trueTotalPrice} đ`} isTotal={true} />
                    <ItemCheck label="Take Out" value={isTake} onChange={handleTakeOutChange} />
                    <ItemCheck label="Pay in cash" value={isCash} onChange={setIsCash} />
                </div>
            </div>
            {error && <p style={{ color: "red" }}>{error}</p>}
            <PlaceOrderButton
                amount={trueTotalPrice}
                isTake={isTake}
                isCash={isCash}
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