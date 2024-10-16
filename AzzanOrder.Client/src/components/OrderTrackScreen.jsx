import React from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import OrderItem from './TrackingOrder/OrderItem';
import OrderSummary from './TrackingOrder/OrderSummary';

const OrderTrackScreen = () => {
    return (
        <>
            <Header />
            <div style={{ padding: '20px 0' }}> {/* Add padding to the container */}
                <OrderSummary />
                <div style={{ marginTop: '20px' }}> {/* Margin to create space between title and orders */}
                    <OrderItem
                        imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/609badea55eb9a7c49a6fd238210454fdacf4304741bbbbdf1f4146216488e6f?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                        title="Trà chanh"
                        details={["30% đường, 0% đá", "+ Thạch dừa"]}
                        status={1}
                    />
                    <div className="order-item-spacing" /> {/* Spacing between order items */}
                    <OrderItem
                        imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/609badea55eb9a7c49a6fd238210454fdacf4304741bbbbdf1f4146216488e6f?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                        title="Trà chanh"
                        details={[null, null]}
                        status={2}
                    />
                    <div className="order-item-spacing" /> {/* Spacing between order items */}
                    <OrderItem
                        imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/609badea55eb9a7c49a6fd238210454fdacf4304741bbbbdf1f4146216488e6f?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                        title="Trà chanh"
                        details={["30% đường, 0% đá", null]}
                        status={3}
                    />
                </div>
            </div>
            <Footer />
            <style jsx>{`
                .order-item-spacing {
                    margin: 10px 0; /* Combine margin for spacing */
                }
            `}</style>
        </>
    );
};

export default OrderTrackScreen;
