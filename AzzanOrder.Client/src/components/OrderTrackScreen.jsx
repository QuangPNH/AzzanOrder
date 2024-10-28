import React, { useState, useEffect } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import OrderItem from './TrackingOrder/OrderItem';
import OrderSummary from './TrackingOrder/OrderSummary';
import { getCookie } from './Account/SignUpForm/Validate';

const OrderTrackScreen = () => {
    const [orders, setOrders] = useState([]);

    useEffect(() => {
        const tableqr = getCookie("tableqr");
        if (tableqr) {
            const [qr, id] = tableqr.split('/');
            fetchOrders(qr, id);
        }
    }, []);

    const fetchOrders = async (tableQr,id) => {
        try {
            const response = await fetch(`https://localhost:7183/api/Order/GetOrderByTableQr/${tableQr}/${id}`);
            const data = await response.json();
            setOrders(data);
        } catch (error) {
            console.error('Error fetching orders:', error);
        }
    };
    return (
        <>
            <Header />
            <div style={{ padding: '20px 0' }}>
                <OrderSummary />
                <div style={{ marginTop: '20px' }}>
                    {orders.map((order, index) => (
                        <React.Fragment key={order.orderId}>
                            <OrderItem
                                imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/609badea55eb9a7c49a6fd238210454fdacf4304741bbbbdf1f4146216488e6f?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                                title="Trà chanh"
                                details={["30% đường, 0% đá", "+ Thạch dừa"]}
                                status={order.status}
                            />
                            {index < orders.length - 1 && <div className="order-item-spacing" />}
                        </React.Fragment>
                    ))}
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
