import React, { useState, useEffect, useRef } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import Dropdown from './Dropdown/Dropdown';
import ProductSale from './Voucher/VoucherDetail/ProductSale';
import PointsDisplay from './Voucher/PointDisplay';
import VoucherList from './Voucher/VoucherList';
import { useLocation } from "react-router-dom";
const VoucherScreen = () => {
    const [vouchers, setVouchers] = useState([]);
    const [allVouchers, setAllVouchers] = useState(false);
    const [point, setPoint] = useState(false);
    const [categories, setCategories] = useState([]);
    const [memberVouchers, setMemberVouchers] = useState(false);
    const id = getCookie("tableqr");
    useEffect(() => {
        if (id) {
          
            fetchVouchers('', id.split('/')[1]);
            fetchCategories(id.split('/')[1]);
            if (getCookie('memberInfo') != null) {
                fetchMembers(JSON.parse(getCookie('memberInfo')).memberId);
                fetchMemberVouchers(JSON.parse(getCookie('memberInfo')).memberId, id.split('/')[1]);
            }
        } else {
            console.log(id, 'hello');
            fetchVouchers('', '');
            fetchCategories('');
            if (getCookie('memberInfo') != null) {
                fetchMembers(JSON.parse(getCookie('memberInfo')).memberId);
                fetchMemberVouchers(JSON.parse(getCookie('memberInfo')).memberId,'');
            }
        }
    }, []);
    const fetchMemberVouchers = async (memberId, id) => {
        try {
        
            const response = await fetch(`https://localhost:7183/api/MemberVouchers/memberId?memberId=${memberId}&employeeId=${id}`);
            //dang sửa API cho phần này về việc voucher và một số thành phần cần có sự quản lý của từng employeeId
            const data = await response.json();
            setMemberVouchers([true]);
            setMemberVouchers(data);
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    const fetchVouchers = async (category, id) => {
        try {
            
            const response = category == '' ? await fetch(`https://localhost:7183/api/VoucherDetail/?employeeId=${id}`) : await fetch(`https://localhost:7183/api/VoucherDetail/categoryId?categoryId=${category}&employeeId=${id}`);
            const data = await response.json();
            if (category == '') {
                setAllVouchers(data);
            } else {
                setAllVouchers(false);
                setVouchers(data);

            }
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    const fetchMembers = async (customerId) => {
        try {
            const response = await fetch(`https://localhost:7183/api/Member/${customerId}`);
            const data = await response.json();
            setPoint(true);
            setPoint(data);

        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    const fetchCategories = async (id) => {
        try {
            const response = await fetch(`https://localhost:7183/api/ItemCategory?id=${id}`);
            const data = await response.json();
            setCategories(data);
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    const handleDropdownChange = (selectedCategory) => {
        if(id){
            fetchVouchers(selectedCategory, id.split('/')[1]);
        }
        fetchVouchers(selectedCategory, '');
    };





    return (
        <>
            <Header />
            <VoucherList />
            <div className="content-container">
                {point && (
                    <div>
                        <div className='product-grid'>
                            <PointsDisplay
                                key={point.id}
                                points={point.point}
                            />
                        </div>
                    </div>
                )}
                {memberVouchers && (
                    <div className="product-sale-container">
                        {memberVouchers.map((mv) => (

                            <ProductSale
                                key={mv.memberVoucherId}
                                saleAmount={mv.discount}
                                endDate={mv.endDate}
                                bought={true}
                                voucherDetailId={mv.voucherDetailId}
                                infiniteUses={false}
                            />
                        ))}
                    </div>
                )}

                {/* <Category /> */}
                <p>0934422800</p>
                <Dropdown
                    options={categories.map(category => category.description)}
                    onClick2={handleDropdownChange}
                    onChange={handleDropdownChange} />

                {allVouchers && (
                    <div className="product-sale-container">
                        {allVouchers.map((aV) => (
                            <ProductSale
                                key={aV.id}
                                saleAmount={aV.discount}
                                endDate={aV.endDate}
                                price={aV.price}
                                infiniteUses={true}
                                useCount={0}
                                voucherDetailId={aV.voucherDetailId}
                            />
                        ))}
                    </div>
                )}
                {vouchers && (
                    <div className="product-sale-container">
                        {vouchers.map((v) => (
                            <ProductSale
                                key={v.voucherDetail.id}
                                saleAmount={v.voucherDetail.discount}
                                endDate={v.voucherDetail.endDate}
                                price={v.voucherDetail.price}
                                infiniteUses={true}
                                useCount={0}
                                voucherDetailId={v.voucherDetail.voucherDetailId}
                            />
                        ))}
                    </div>
                )}
            </div>



            <style jsx>{`
                .content-container {
                    padding: 20px; /* Add padding to the container */
                    margin-top: 20px; /* Ensure there's space below the header */
                }
                .product-sale-container {
                    display: flex; /* Use flex display to align items */
                    flex-direction: column; /* Stack items vertically */
                    align-items: center; /* Center items horizontally */
                    margin-top: 20px; /* Space above the product sale section */
                }
            `}</style>
            <Footer />

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
export default VoucherScreen;