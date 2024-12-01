import React from 'react';

const NotificationPageTitle = () => {
    return (
        <div className="your-notification-page">
            <div className="your-notification-page-title">Notification</div>
            <style jsx>{`
                .your-notification-page {
                    width: 100%;
                    text-align: center;
                    margin-top: 20px; /* Add margin to create space */
                }
                .your-notification-page-title {
                    color: var(--primary-color); /* Set color to BD3326 (100%) */
                    font: bold 32px Inter, sans-serif; /* Use Inter Bold 32 */
                    margin: 0;
                    padding-top: 20px;
                }
            `}</style>
        </div>
    );
};

export default NotificationPageTitle;
