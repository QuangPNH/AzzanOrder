import React, { useState } from 'react';
import Modal from 'react-modal';
import LoginPage from '../../components/Account/LoginPage';
import SignUpPage from '../../components/Account/SignUpPage';

// Set the app element to avoid accessibility warnings in react-modal
Modal.setAppElement('#root');

const NavItem = () => {
    const [isLoginModalOpen, setLoginModalOpen] = useState(false);
    const [isSignUpModalOpen, setSignUpModalOpen] = useState(false);

    const navItems = [
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/2000ef5b1388f84bd861a1cfeabcb17787a38e432ea204379ab731580cd84b0b?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Login" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/fe570d9132ffff17abf26958447f9d8a921987650f9e6cb66275a59986d8d8d1?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Sign up" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/70410d91514f262ce614a7b7bcfb761a294b0ce637a9bc36accdd377b655428a?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Notification" },
        { icon: "https://cdn.builder.io/api/v1/image/assets/TEMP/360aab801c588575a78887d36d50e88cb9803d8221437815dec0250221ff6e7c?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533", text: "Send Feedback" }
    ];

    const handleLoginClick = () => setLoginModalOpen(true);
    const handleSignUpClick = () => setSignUpModalOpen(true);
    const closeLoginModal = () => setLoginModalOpen(false);
    const closeSignUpModal = () => setSignUpModalOpen(false);

    const handleNotificationClick = () => {
        window.location.href = '/notification';
    };

    const handleSendFeedbackClick = () => {
        window.location.href = '/feedback';
    };

    return (
        <div className="nav-items">
            {/* Login Modal Trigger */}
            <div className="nav-item" onClick={handleLoginClick}>
                <img loading="lazy" src={navItems[0].icon} alt={navItems[0].text} className="nav-icon" />
                <span className="nav-text">{navItems[0].text}</span>
            </div>

            {/* Login Modal */}
            <Modal
                isOpen={isLoginModalOpen}
                onRequestClose={closeLoginModal}
                contentLabel="Login Modal"
                className="modal"
                overlayClassName="modal-overlay"
            >
                <LoginPage />
                <button onClick={closeLoginModal}>Close</button>
            </Modal>

            {/* Sign Up Modal Trigger */}
            <div className="nav-item" onClick={handleSignUpClick}>
                <img loading="lazy" src={navItems[1].icon} alt={navItems[1].text} className="nav-icon" />
                <span className="nav-text">{navItems[1].text}</span>
            </div>

            {/* Sign Up Modal */}
            <Modal
                isOpen={isSignUpModalOpen}
                onRequestClose={closeSignUpModal}
                contentLabel="Sign Up Modal"
                className="modal"
                overlayClassName="modal-overlay"
            >
                <SignUpPage />
                <button onClick={closeSignUpModal}>Close</button>
            </Modal>

            <div className="nav-item" onClick={handleNotificationClick}>
                <img loading="lazy" src={navItems[2].icon} alt={navItems[2].text} className="nav-icon" />
                <span className="nav-text">{navItems[2].text}</span>
            </div>

            <div className="nav-item" onClick={handleSendFeedbackClick}>
                <img loading="lazy" src={navItems[3].icon} alt={navItems[3].text} className="nav-icon" />
                <span className="nav-text">{navItems[3].text}</span>
            </div>

            <style jsx>{`
                .nav-items {
                    display: flex;
                    flex-direction: column;
                    gap: 8px;
                }
                .nav-item {
                    display: flex;
                    align-items: center;
                    gap: 10px;
                    padding: 8px;
                    cursor: pointer;
                    transition: background-color 0.3s;
                }
                .nav-item:hover {
                    background-color: rgba(255, 255, 255, 0.5);
                }
                .nav-icon {
                    aspect-ratio: 1;
                    object-fit: contain;
                    object-position: center;
                    width: 24px;
                }
                .nav-text {
                    white-space: nowrap;
                    font-size: 14px;
                    line-height: 1.5;
                    color: #000;
                }
                .modal {
                    position: absolute;
                    top: 50%;
                    left: 50%;
                    right: auto;
                    bottom: auto;
                    margin-right: -50%;
                    transform: translate(-50%, -50%);
                    background-color: #fff;
                    padding: 20px;
                    border-radius: 4px;
                }
                .modal-overlay {
                    background-color: rgba(0, 0, 0, 0.75);
                }
            `}</style>
        </div>
    );
};

function setCookie(name, value, days) {
  const expires = new Date(Date.now() + days * 864e5).toUTCString();
  document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`;
}

function getCookie(name) {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return decodeURIComponent(parts.pop().split(';').shift());
}

function clearCookie(name) {
  document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/`;
}

export default NavItem;
