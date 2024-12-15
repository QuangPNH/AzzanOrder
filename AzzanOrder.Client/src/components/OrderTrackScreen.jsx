import React, { useState, useEffect, useRef  } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import OrderItem from './TrackingOrder/OrderItem';
import OrderSummary from './TrackingOrder/OrderSummary';
import Button from "./Account/SignUpForm/Button";
import { getCookie } from './Account/SignUpForm/Validate';
import API_URLS from '../config/apiUrls';

const OrderTrackScreen = () => {
    const [orders, setOrders] = useState([]);
    const [customerOrder, setCustomerOrder] = useState([]);
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
            const response = await fetch(API_URLS.API + `Order/GetOrderByTableQr/${tableQr}/${id}`);
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
             const response = await fetch(API_URLS.API + `Order/GetCustomerOrder/${customerId}`);
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

    const allOrdersCompleted = orders && orders.length > 0 && orders.every(order => order.status === true);

    const updateMemberPoints = async (memberId) => {
        if(memberId != ''){
            try {

                await fetch(API_URLS.API + `Member/UpdatePoints/memberId/point?memberId=${memberId}&point=25`);
            } catch (error) {
                console.error('Error updating member points:', error);
            }
            window.location.href = '/';
        }
        window.location.href = '/';
    };

    const handleButtonClick = () => {
        if (allOrdersCompleted) {
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
                {orders.length === 0 ? (
                    <div style={{ textAlign: 'center', marginTop: '20px', color: 'red' }}>
                        <p>The list is empty. <span onClick={handleReturnHome} style={{ textDecoration: 'underline', cursor: 'pointer', color: 'inherit' }}>Return to Home</span></p>
                    </div>
                ) : (
                    <div style={{ marginTop: '20px', maxHeight: '400px', overflowY: 'auto' }}>
                        {orders.map((order, orderIndex) => (
                            <React.Fragment key={order.orderId}>
                                <h3>Order {orderIndex +1}</h3>
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
                                    {customerOrder.map((order, orderIndex) => (
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
                        )} 
                    </div>
                )}
                <div style={{ display: 'flex', flexDirection: 'column' }}>
                    <Button
                        type="submit"
                        text="Clean table"
                        onClick={handleButtonClick}
                        style={{ backgroundColor: allOrdersCompleted ? 'Green' : 'Gray', cursor: allOrdersCompleted ? 'pointer' : 'not-allowed' }}
                        disabled={!allOrdersCompleted}
                    />
                    <div style={{ textAlign: 'center', marginTop: '20px', color: 'black' }}>
                        <p>Click to notify staff to clean table once you finished the dishes.</p>
                        <p>Point will be earned.</p>
                    </div>
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