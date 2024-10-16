import React from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import Dropdown from './Dropdown/Dropdown';
import ProductSale from './Voucher/VoucherDetail/ProductSale';
import Category from './Voucher/Category';
import PointsDisplay from './Voucher/PointDisplay';
import VoucherList from './Voucher/VoucherList';

const VoucherScreen = () => {
    return (
        <>
            <Header />
            <div className="content-container">
                <PointsDisplay points="30.000" />
                <VoucherList />
                <Category />
                <Dropdown />
            </div>

            <div className="product-sale-container">
                <ProductSale
                    saleAmount={10}
                    endDate="2024-12-01"
                    price={0}
                    infiniteUses={true}
                    useCount={0}
                />

                <ProductSale
                    saleAmount={10}
                    endDate="2024-12-01"
                    price={0}
                    infiniteUses={false}
                    useCount={5}
                />
            </div>

            <Footer />
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
        </>
    );
};

export default VoucherScreen;
