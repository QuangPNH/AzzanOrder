import React, { useState, useEffect } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import NotificationCard from './Notification/NotificationCard';
import NotificationPageTitle from './Notification/NotificationPageTitle';

const NotificationPage = () => {

    return (
        <>
            <Header />
            <NotificationPageTitle />

            <div className="notification-container">
                <NotificationCard
                    title="Your Order is Ready"
                    content="Dear Customer, your order is ready. Or not. Lorem ipsum lorem ipsum customer customer. Additional information about your order goes here."
                />
                <NotificationCard
                    title="Your Order is Ready"
                    content="Dear Customer, your order is ready. Or not. Lorem ipsum lorem ipsum customer customer. Additional information about your order goes here."
                />
                <NotificationCard
                    title="Your Order is Ready"
                    content="Dear Customer, your order is ready. Or not. Lorem ipsum lorem ipsum customer customer. Additional information about your order goes here."
                />
            </div>

            <Footer />

            <style jsx>{`
    .notification-container {
        margin-bottom: 25px; /* Adjust as needed for spacing */
    }

    .notification-container > :last-child {
        margin-bottom: 40px; /* Extra margin for the last card */
    }
`}</style>

        </>
    );
};

export default NotificationPage;
