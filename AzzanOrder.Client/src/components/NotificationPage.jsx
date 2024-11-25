import React, { useState, useEffect } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import NotificationCard from './Notification/NotificationCard';
import NotificationPageTitle from './Notification/NotificationPageTitle';
import { getCookie } from './Account/SignUpForm/Validate';
import API_URLS from '../config/apiUrls';

const NotificationPage = () => {
    const [notifications, setNotifications] = useState([]);

    useEffect(() => {
        const memberInfoCookie = getCookie('memberInfo');
        if (memberInfoCookie != null) {
            const memberInfo = JSON.parse(memberInfoCookie);
            fetchNotifications(memberInfo);
        } else {
            window.location.href = '';
        }
    }, []);

    const fetchNotifications = async (memberInfo) => {
        try {
            const response = await fetch(API_URLS.API + `Notification/Member/${memberInfo.memberId}`);
            const data = await response.json();
            setNotifications(data);
        } catch (error) {
            console.error('Error fetching notifications:', error);
        }
    };

    return (
        <>
            <Header />
            <NotificationPageTitle />

            <div className="notification-container">
                {notifications?.map((notification, index) => (
                    <NotificationCard
                        key={index}
                        title={notification.title}
                        content={notification.content}
                    />
                ))}
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