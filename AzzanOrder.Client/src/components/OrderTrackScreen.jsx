import React, { useState, useEffect } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import OrderItem from './TrackingOrder/OrderItem';
import OrderSummary from './TrackingOrder/OrderSummary';
import Button from "./Account/SignUpForm/Button";
import { getCookie } from './Account/SignUpForm/Validate';

const OrderTrackScreen = () => {
    const [orders, setOrders] = useState([]);
    const [customerOrder, setCustomerOrder] = useState(null);

    useEffect(() => {
        const tableqr = getCookie("tableqr");
        if (tableqr) {
            const [qr, id] = tableqr.split('/');
            fetchOrders(qr, id);
        }
        if (getCookie("memberInfo")) {
            fetchCustomerOrder(JSON.parse(getCookie("memberInfo")).memberId);
        }
    }, []);

    const fetchOrders = async (tableQr, id) => {
        try {
            const response = await fetch(`https://localhost:7183/api/Order/GetOrderByTableQr/${tableQr}/${id}`);
            if (response.ok) {
                const data = await response.json();
                setOrders(data);
            }
        } catch (error) {
            console.error('Error fetching orders:', error);
        }
    };

    const fetchCustomerOrder = async (customerId) => {
        try {
            const response = await fetch(`https://localhost:7183/api/Order/GetCustomerOrder/${customerId}`);
            if (response.ok) {
                const data = await response.json();
                setCustomerOrder(data);
            }
        } catch (error) {
            console.error('Error fetching customer order:', error);
        }
    };

    const mapStatus = (status) => {
        if (status === null) return 1;
        if (status === false) return 2;
        if (status === true) return 3;
    };

    const allOrdersCompleted = orders && orders.length > 0 && orders.every(order =>
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

    const handleReturnHome = () => {
        window.location.href = '/';
    };

    return (
        <>
            <Header />
            <div style={{ padding: '20px 0' }}>
                <OrderSummary />
                {orders.length === 0 && !customerOrder ? (
                    <div style={{ textAlign: 'center', marginTop: '20px', color: 'red' }}>
                        <p>The list is empty. <span onClick={handleReturnHome} style={{ textDecoration: 'underline', cursor: 'pointer', color: 'inherit' }}>Return to Home</span></p>
                    </div>
                ) : (
                    <div style={{ marginTop: '20px', maxHeight: '400px', overflowY: 'auto' }}>
                        {orders.map((order, orderIndex) => (
                            <React.Fragment key={order.orderId}>
                                <h3>Order {orderIndex}</h3>
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
                        {customerOrder && (
                            <div style={{ marginTop: '20px' }}>
                                <h2>Customer Order</h2>
                                {customerOrder.orderDetails.map((orderDetail) => (
                                    <OrderItem
                                        key={orderDetail.orderDetailId}
                                        imageSrc={orderDetail.menuItem?.image || 'default-image-url'}
                                        title={orderDetail.menuItem?.itemName || 'Unknown Item'}
                                        details={[orderDetail.description || 'No details']}
                                        status={mapStatus(orderDetail.status)}
                                    />
                                ))}
                            </div>
                        )}
                    </div>
                )}
                <div style={{ display: 'flex', flexDirection: 'column' }}>
                    <Button
                        type="submit"
                        text="Close table to receive points"
                        onClick={handleButtonClick}
                        style={{ backgroundColor: allOrdersCompleted ? 'initial' : 'gray', cursor: allOrdersCompleted ? 'pointer' : 'not-allowed' }}
                        disabled={!allOrdersCompleted}
                    />
                </div>
            </div>
            <Footer />
            <style>{`
                .order-item-spacing {
                    margin: 10px 0; /* Combine margin for spacing */
                }
            `}</style>
        </>
    );
};

export default OrderTrackScreen;