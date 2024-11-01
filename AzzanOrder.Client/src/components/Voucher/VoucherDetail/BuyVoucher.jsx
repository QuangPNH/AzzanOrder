import React from "react";
import YNWidgetVoucher from './YNWidgetVoucher';
import Popup from 'reactjs-popup';

function BuyVoucher({ isOpen, handleClosePopup, points, voucherDetailId }) {
    const text = `Are you sure to redeem ${points} points for this voucher?`
    const errorText = `Your point not enough to redeem this voucher!`
    return (
        <div>
            <Popup open={isOpen} onClose={handleClosePopup}>
                <YNWidgetVoucher
                    title = {text}
                    errorTitle = {errorText}
                    onClose={handleClosePopup }
                    voucherDetailId= {voucherDetailId}
                />
            </Popup>
        </div>
    );
};
export default BuyVoucher;