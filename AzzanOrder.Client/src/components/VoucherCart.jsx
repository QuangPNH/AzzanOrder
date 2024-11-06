import React, { useEffect } from 'react';
import DropTest from './Voucher/DropTest';


const VoucherCart = ({ onSelectVoucher }) => {
    const handleDropdownChange = (selectedItem) => {
        onSelectVoucher(selectedItem);
    };

    return (
        <>
            <DropTest
                onClick={handleDropdownChange}
                onChange={handleDropdownChange} />
        </>
    );
};

export default VoucherCart;