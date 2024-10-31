import React from "react";
import YNWidgetVoucher from './YNWidgetVoucher';
import Popup from 'reactjs-popup';

function BuyVoucher({ isOpen, handleClosePopup, points }) {
    const text = `Are you sure to redeem ${points} points for this voucher?`
    return (
        <div>
            <Popup open={isOpen} onClose={handleClosePopup} >
                <YNWidgetVoucher
                    title = {text}
                    onClose={handleClosePopup }
                />
            </Popup>
        </div>
    );
};
export default BuyVoucher;