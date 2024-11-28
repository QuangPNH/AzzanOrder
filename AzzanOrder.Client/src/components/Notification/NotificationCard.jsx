import * as React from "react";

function NotificationCard({ title, content }) {
    const [isRead, setIsRead] = React.useState(false);
    const [isExpanded, setIsExpanded] = React.useState(false);

    const handleToggleReadMore = () => {
        setIsRead(true);  // Mark as read once expanded at least once
        setIsExpanded(!isExpanded); // Toggle the expanded state
    };

    return (
        <>
            <div className="container">
                <div className={`notification ${isRead ? 'read' : 'unread'}`}>
                    <div className="notification-content">
                        <div className="notification-title">{title}</div>
                        <div className={`notification-message ${isExpanded ? 'expanded' : 'collapsed'}`}>
                            {content}
                        </div>
                        <button
                            className="read-more-btn"
                            onClick={handleToggleReadMore}
                            tabIndex="0"
                            aria-label={isExpanded ? "Read less about your order" : "Read more about your order"}
                        >
                            {isExpanded ? "Read less..." : "Read more..."}
                        </button>
                        {!isRead && (
                            <span className="unread-indicator" role="status" aria-label="Unread notification" />
                        )}
                    </div>
                </div>
            </div>
            <div className="notification-separator">
                <hr className="notification-separator-line" />
            </div>

            <style jsx>{`
                * {
                    font-family: 'Inter', sans-serif;
                    word-wrap: break-word;
                }

                .container {
                    display: flex;
                    justify-content: center;
                    position: relative;
                    width: 100vw; /* Full width of the viewport */
                }

                .notification {
                    position: relative;
                    border-radius: 0;
                    display: flex;
                    flex-direction: column;
                    background-color: #fff;
                    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                    padding: 20px;
                    margin-top: 20px;
                    width: 90%; /* Make the notification responsive */
                    max-width: 360px; /* Limit maximum width for larger screens */
                }

                @media (max-width: 600px) {
                    .notification {
                        width: 95%; /* Take up more space on smaller screens */
                        padding: 15px; /* Adjust padding for small screens */
                    }
                }

                .notification.read {
                    background-color: #fff;
                }

                .notification.unread {
                    background-color: #fff5f5;
                }

                .notification-content {
                    display: flex;
                    flex-direction: column;
                    align-items: flex-start;
                    color: #000;
                }

                .notification-title {
                    font-size: 20px;
                    font-weight: 600;
                    text-align: left;
                    margin: 0;
                }

                .notification-message {
                    margin-top: 6px;
                    font-size: 16px;
                    font-weight: 400;
                    overflow: hidden;
                    text-overflow: ellipsis;
                    max-width: 100%; /* Ensure content does not exceed container width */
                    -webkit-box-orient: vertical;
                    max-height: 1em; /* Adjust based on line height */
                    white-space: pre-wrap; /* Ensure content breaks into lines */
                }

                .notification-message.expanded {
                    overflow: visible; /* Show all content when expanded */
                    text-overflow: unset; /* Remove ellipsis when expanded */
                    max-height: none; /* Remove max-height when expanded */
                }

                .read-more-btn {
                    color: var(--primary-color);
                    align-self: flex-end;
                    margin-top: 20px;
                    background: none;
                    border: none;
                    cursor: pointer;
                    padding: 0;
                    font-size: 12px;
                    font-style: italic;
                    transition: opacity 0.2s ease;
                }

                .read-more-btn:hover,
                .read-more-btn:focus {
                    opacity: 0.8;
                    outline: none;
                }

                .unread-indicator {
                    position: absolute;
                    top: 10px;
                    right: 10px;
                    background-color: rgb(255, 0, 0);
                    border-radius: 50%;
                    width: 12px;
                    height: 12px;
                }

                .notification-separator {
                    margin-top: 10px;
                    display: flex;
                    justify-content: center;
                    width: 90%; /* Match the width of the notification */
                    max-width: 360px; /* Match the max width of the notification */
                    margin-left: auto; /* Center the notification-separator */
                    margin-right: auto; /* Center the notification-separator */
                }

                .notification-separator-line {
                    background: var(--primary-color);
                    border: none;
                    height: 1px;
                    width: 100%; /* Takes the full width of the notification-separator container */
                }
            `}</style>
        </>
    );
}

export default NotificationCard;