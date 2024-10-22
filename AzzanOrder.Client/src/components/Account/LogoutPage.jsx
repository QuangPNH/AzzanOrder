import React from "react";
import YNWidget from "./SignUpForm/YNWidget";
import Popup from 'reactjs-popup';

function LogoutPage({ isOpen, handleClosePopup }) {
    return (
        <div>
            <Popup open={isOpen} onClose={handleClosePopup}>
                <YNWidget
                    title="Confirm log out ?"
                    onClose={handleClosePopup }
                />
            </Popup>
        </div>
    );
}
export default LogoutPage;