import React from "react";
import YNWidget from "./SignUpForm/YNWidget";
import Popup from 'reactjs-popup';

function LogoutPage({ isOpen, handleClosePopup, func, title}) {
    return (
        <div>
            <Popup open={isOpen} onClose={handleClosePopup}>
                <YNWidget
                    title={title }
                    onClose={handleClosePopup}
                    func={func}
                />
            </Popup>
        </div>
    );
}
export default LogoutPage;