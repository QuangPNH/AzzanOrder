import React, { useState, useEffect, useRef } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import Dropdown from './Dropdown/Dropdown';
import ProductSale from './Voucher/VoucherDetail/ProductSale';
import PointsDisplay from './Voucher/PointDisplay';
import VoucherList from './Voucher/VoucherList';

const VoucherScreen = () => {
    const [vouchers, setVouchers] = useState([]);
    const [point, setPoint] = useState(false);
    const [categories, setCategories] = useState([]);
    const [memberVouchers, setMemberVouchers] = useState(false);
    useEffect(() => {
        fetchVouchers();
        fetchCategories();
        if (getCookie('memberInfo') != null) {
            fetchMembers(JSON.parse(getCookie('memberInfo')).memberId);
            fetchMemberVouchers(JSON.parse(getCookie('memberInfo')).memberId);
            setPoint(true);
            setMemberVouchers(true);
        }
    }, []);
    const fetchMemberVouchers = async (memberId) => {
        try {
            const response = await fetch(`https://localhost:7183/api/MemberVouchers/memberId?memberId=${memberId}`);
            const data = await response.json();
            setMemberVouchers(data);
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    const fetchVouchers = async (category = '') => {
        try {
            const response = category == '' ? await fetch(`https://localhost:7183/api/VoucherDetail`) : await fetch(`https://localhost:7183/api/VoucherDetail/categoryId?categoryId=${category}`);
            const data = await response.json();
            setVouchers(data);
            console.log(category);
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    const fetchMembers = async (customerId) => {
        try {
            const response = await fetch(`https://localhost:7183/api/Member/${customerId}`);
            const data = await response.json();
            setPoint(data);
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    const fetchCategories = async () => {
        try {
            const response = await fetch('https://localhost:7183/api/ItemCategory');
            const data = await response.json();
            setCategories(data);
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    const handleDropdownChange = (selectedCategory) => {
        fetchVouchers(selectedCategory);
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

                {/* <Category /> */}

                <Dropdown
                    options={categories.map(category => category.description)}
                    onClick2={handleDropdownChange}
                    onChange={handleDropdownChange} />
                
                        {vouchers && (
                            <div className="product-sale-container">
                                {vouchers.map((voucher) => (
                                    <ProductSale
                                        key={voucher.id}
                                        saleAmount={voucher.discount}
                                        endDate={voucher.endDate}
                                        price={voucher.price}
                                        infiniteUses={true}
                                        useCount={0}
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
