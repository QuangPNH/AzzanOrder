import React, { useState, useEffect, useRef } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import Dropdown from './Dropdown/Dropdown';
import ProductSale from './Voucher/VoucherDetail/ProductSale';
import PointsDisplay from './Voucher/PointDisplay';
import VoucherList from './Voucher/VoucherList';
import { getCookie } from './Account/SignUpForm/Validate';
import API_URLS from '../config/apiUrls';

const VoucherScreen = () => {
    const [vouchers, setVouchers] = useState([]);
    const [allVouchers, setAllVouchers] = useState(false);
    const [point, setPoint] = useState(false);
    const [categories, setCategories] = useState([]);
    const [memberVouchers, setMemberVouchers] = useState(false);
    const manaId = getCookie("tableqr");
    useEffect(() => {
        if (manaId) {

            fetchVouchers('', manaId.split('/')[1]);
            fetchCategories(manaId.split('/')[1]);
            if (getCookie('memberInfo') != null) {
                fetchMembers(JSON.parse(getCookie('memberInfo')).memberId);
                fetchMemberVouchers(JSON.parse(getCookie('memberInfo')).memberId, manaId.split('/')[1]);
            }
        } else {
            fetchVouchers('', '');
            fetchCategories('');
            if (getCookie('memberInfo') != null) {
                fetchMembers(JSON.parse(getCookie('memberInfo')).memberId);
                fetchMemberVouchers(JSON.parse(getCookie('memberInfo')).memberId, '');
            }
        }
    }, []);
    const fetchMemberVouchers = async (memberId, manaId) => {
        try {

            const response = await fetch(API_URLS.API + `MemberVouchers/memberId?memberId=${memberId}&employeeId=${manaId}`);
            const data = await response.json();
            let a = [];
            for (let i of data) {
               
                if (i.endDate > new Date().toISOString() || i.endDate === null) {
                    a.push(i);
                }
            }
            setMemberVouchers([true]);
            setMemberVouchers(a);
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    const fetchVouchers = async (category, manaId) => {
        try {
            const holdId = manaId;
            const response = category == '' ? await fetch(API_URLS.API + `VoucherDetail/ListVoucherDetail?employeeId=${holdId}`) : await fetch(API_URLS.API + `VoucherDetail/categoryId?categoryId=${category}&employeeId=${holdId}`);
            const data = await response.json();
            
            if (category == '') {
            let a = [];
            for (let i of data) {
                if (i.endDate > new Date().toISOString() || i.endDate === null) {
                    a.push(i);
                }
            }
                setAllVouchers(a);
            } else {
                setAllVouchers(false);
            let a = [];
            for (let i of data) {
                if (i.voucherDetail.endDate > new Date().toISOString() || i.voucherDetail.endDate === null) {
                    a.push(i);
                }
            }
                setVouchers(a);

            }
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    const fetchMembers = async (customerId) => {
        try {
            const response = await fetch(API_URLS.API + `Member/${customerId}`);
            const data = await response.json();
            setPoint(true);
            setPoint(data);

        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    const fetchCategories = async (manaId) => {
        try {
            const response = await fetch(API_URLS.API + `ItemCategory/GetAllItemCategoriesValid?id=${manaId}`);
            const data = await response.json();
            setCategories(data);
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    const handleDropdownChange = (selectedCategory) => {
        if (manaId) {
            fetchVouchers(selectedCategory, manaId.split('/')[1]);
        }else{
            fetchVouchers(selectedCategory, '');
        }
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

export default VoucherScreen;