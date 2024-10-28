import React, { useState, useEffect } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import OrderItem from './TrackingOrder/OrderItem';
import OrderSummary from './TrackingOrder/OrderSummary';
import Button from "./Account/SignUpForm/Button";
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

    const fetchOrders = async (tableQr, id) => {
        try {
            const response = await fetch(`https://localhost:7183/api/Order/GetOrderByTableQr/${tableQr}/${id}`);
            const data = await response.json();
            setOrders(data);
        } catch (error) {
            console.error('Error fetching orders:', error);
        }
    };

    const mapStatus = (status) => {
        if (status === null) return 1;
        if (status === false) return 2;
        if (status === true) return 3;
    };

    const allOrdersCompleted = orders.every(order =>
        order.orderDetails.every(orderDetail => orderDetail.status === true)
    );

    const updateMemberPoints = async (memberId) => {
        try {
            await fetch(`https://localhost:7183/api/Members/UpdatePoints/${memberId}/100`, {
                method: 'PUT'
            });
        } catch (error) {
            console.error('Error updating member points:', error);
        }
    };

    const handleButtonClick = () => {
        if (orders.length > 0 && orders[0].memberId) {
            updateMemberPoints(orders[0].memberId);
        }
    };

    return (
        <>
            <Header />
            <div style={{ padding: '20px 0' }}>
                <OrderSummary />
                <div style={{ marginTop: '20px' }}>
                    {orders.map((order, orderIndex) => (
                        <React.Fragment key={order.orderId}>
                            {order.orderDetails.map((orderDetail, detailIndex) => (
                                <React.Fragment key={orderDetail.orderDetailId}>
                                    <OrderItem
                                        imageSrc={orderDetail.menuItem?.image || 'default-image-url'}
                                        title={orderDetail.menuItem?.itemName || 'Unknown Item'}
                                        details={[orderDetail.description || 'No details']}
                                        status={mapStatus(orderDetail.status)}
                                    />
                                    {detailIndex < order.orderDetails.length - 1 && <div className="order-item-spacing" />}
                                </React.Fragment>
                            ))}
                            {orderIndex < orders.length - 1 && <div className="order-item-spacing" />}
                        </React.Fragment>
                    ))}
                </div>
                {allOrdersCompleted && <Button type="submit" text="Close table to receive" onClick={handleButtonClick} />}
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