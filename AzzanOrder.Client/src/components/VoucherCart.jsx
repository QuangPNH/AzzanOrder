import React, { useState, useEffect, useRef } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import DropTest from './Voucher/DropTest';
import ProductSale from './Voucher/VoucherDetail/ProductSale';
import PointsDisplay from './Voucher/PointDisplay';
import VoucherList from './Voucher/VoucherList';

const VoucherCart = ({onSelectVoucher}) => {
    const [vouchers, setVouchers] = useState([]);
    const [categories, setCategories] = useState([]);
    const [memberVouchers, setMemberVouchers] = useState(false);
    const [voucherDetail, setVoucherDetail] = useState();

    useEffect(() => {
        if (getCookie('memberInfo') != null) {
            fetchMemberVouchers(JSON.parse(getCookie('memberInfo')).memberId, 1);
            setMemberVouchers([true]);
        }
    }, []);

    const fetchMemberVouchers = async (memberId, categoryId) => {
        try {
            const response = categoryId != '' ? await fetch(`https://localhost:7183/api/MemberVouchers/memberId/itemCategoryId?memberId=${memberId}&categoryId=${categoryId}`) : await fetch(`https://localhost:7183/api/MemberVouchers/memberId?memberId=${memberId}`);
            const data = await response.json();
            setMemberVouchers(data);
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    const fetchVoucherDetails = async (voucherDetailId) => {
        try {
            const response = await fetch(`https://localhost:7183/api/VoucherDetail/${voucherDetailId}`);
            const data = await response.json();
            setVoucherDetail(data);
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    // const fetchCategories = async () => {
    //     try {
    //         const response = await fetch('https://localhost:7183/api/ItemCategory');
    //         const data = await response.json();
    //         setCategories(data);
    //     } catch (error) {
    //         console.error('Error fetching categories:', error);
    //     }
    // };

    const handleDropdownChange = (selectedItem) => {
        // fetchMemberVouchers(JSON.parse(getCookie('memberInfo')).memberId, selectedItem.voucherDetailId);
        fetchVoucherDetails(selectedItem.voucherDetailId);
        onSelectVoucher(selectedItem);
    };
    return (
        <>


            {/* <Category /> */}
            {
                voucherDetail && (
                    <div>
                        < ProductSale
                            key={voucherDetail.id}
                            saleAmount={voucherDetail.discount}
                            endDate={voucherDetail.endDate}
                            bought={true}
                        />

                    </div>

                )
            }

            <DropTest
                onClick={handleDropdownChange}
                onChange={handleDropdownChange} />
            {/* <div className="content-container">
                {memberVouchers && (
                    <div className="product-sale-container">
                        {memberVouchers.map((memberVoucher) => (
                            <ProductSale
                                key={memberVoucher.id}
                                saleAmount={memberVoucher.discount}
                                endDate={memberVoucher.endDate}
                                price={memberVoucher.price}
                                infiniteUses={true}
                                useCount={0}
                            />
                        ))}
                    </div>
                )}
            </div> */}

        </>
    );
};

function setCookie(name, value, days) {
    const expires = new Date(Date.now() + days * 864e5).toUTCString(); // Calculate expiration date
    document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`; // Set cookie
}

function getCookie(name) {
    const value = `; ${document.cookie}`; // Add a leading semicolon for easier parsing
    const parts = value.split(`; ${name}=`); // Split the cookie string to find the desired cookie
    if (parts.length === 2) return decodeURIComponent(parts.pop().split(';').shift()); // Return the cookie value
}

export default VoucherCart;