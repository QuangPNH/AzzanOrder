import React, { useState, useEffect } from 'react';
import BuyVoucher from './BuyVoucher';
import { getCookie, setCookie } from "../../Account/SignUpForm/Validate";
import API_URLS from '../../../config/apiUrls';
const SaleContent = ({ saleAmount, price, infiniteUses, useCount, bought, voucherDetailId }) => {
    const [vouchers, setVouchers] = useState([]);
    const [showLogout, setLogout] = useState(false);
    const [quantityV, setQuantityV] = useState(false);

    useEffect(() => {
        // voucherDetails(voucherDetailId); 
        quantity();
    }, []);
    // const voucherDetails = async (voucherDetailId) => {
    //     try {
    //         if (voucherDetailId != '') {
    //             const response = await fetch(API_URLS.API + `VoucherDetail/${voucherDetailId}`);
    //             const data = await response.json();
    //             setVouchers(data);
    //         }

    //     } catch (error) {
    //         console.error('Error fetching menu items:', error);
    //     }
    // };

    const quantity = async () => {
        try {
            const response = await fetch(API_URLS.API + `MemberVouchers/memberId/voucherDetailId?memberId=${JSON.parse(getCookie('memberInfo')).memberId}&voucherDetailId=${voucherDetailId}`)
            const data = await response.json();
            setQuantityV(data);
            // console.log(data, 'quantity');
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    }
    // return (
    //     <div>
    //         <h1>Voucher List</h1>

    //     </div>
    // );




    // point && (
    //     <div>
    //         <div className='product-grid'>
    //             <PointsDisplay
    //                 key={point.id}
    //                 points={point.point}
    //             />
    //         </div>
    //     </div>
    // )

    return (
        <>
            <BuyVoucher isOpen={showLogout} handleClosePopup={() => setLogout(false)} points={price} voucherDetailId={voucherDetailId}/>
                <section className="sale-content">
                    <div className="sale-info">
                        <div className="sale-percentage">Sale <span className="discount">{saleAmount}%</span></div>

                        <div className="sale-icon-wrapper">
                            <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/e2eac9604a81877b92936d217befed7bedb4e1a3b5ae5a5bae1e56c9c943e889?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533" alt="" className="sale-icon-bg" />
                            {infiniteUses ? (
                                <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/b3126d40803d317ccbc4a81359fffa8e4a11814bc0316903ab7e68fd911c1ce9?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533" alt="Infinite Sale icon" className="sale-icon" />
                            ) : (
                                quantityV && (
                                    <div className="sale-uses" >x{quantityV.quantity}</div>
                                )
                            )}

                        </div>

                    </div>
                    <p className="sale-warning">
                        <em><strong className="warning-text">Warning:</strong> This voucher only use in web.</em>
                    </p>

                    {bought ? (<div> </div>) : (<div className="sale-price">
                        <p>Price: <span className="price-value">{price} points</span></p>  {/* Dynamic price */}
                        <button className="qr-button" onClick={() => setLogout(true)}>

                            <img
                                src="https://cdn.builder.io/api/v1/image/assets/TEMP/66181310e2851f8fae57b02cad765f22e6988ffb6004f773591a0d8561aba4e0?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                                alt="QR code"
                                className="qr-code"
                            />
                        </button>
                    </div>)}
                        
                    <style jsx>{`
                .sale-content {
                    border-radius: 0 10px 10px 0;
                    width: 50em;
                    background-color: #f6b5b5;
                    display: flex;
                    flex-direction: column;
                    overflow: hidden;
                    padding: 25px 0 5px 18px;
                }
                .sale-info {
                    display: flex;
                    justify-content: space-between;
                    align-items: flex-start;
                }
                .sale-percentage {
                    color: #000;
                    text-align: center;
                    font: 600 14px Inter, sans-serif;
                }
                .discount {
                    font-size: 24px;
                    color: #f00;
                }
                .sale-icon-wrapper {
                    position: relative;
                    aspect-ratio: 1;
                    width: 24px;
                    padding: 0px 5px 9px;
                    justify-content: center;  /* Centers horizontally */
        align-items: center;  /* Centers vertically */
                }
                .sale-icon-bg {
                    position: absolute;
                    inset: 0;
                    height: 100%;
                    width: 100%;
                    object-fit: cover;
                }
                .sale-icon {
                    width: 100%;
                    height: 100%;
                    object-fit: contain;
                }
                    .sale-uses {
        color: #f00;  /* Updated color */
        font-size: 12px;
        font-weight: 600;
        font-family: 'Inter', sans-serif;  /* Updated font to Inter */
        position: absolute;  /* Absolute positioning */
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);  /* Center horizontally and vertically */
        z-index: 1;  /* Ensure it appears above the background */
    }
                .sale-warning {
                    color: #000;
                    text-align: center;
                    align-self: flex-start;
                    margin-top: 5px;
                    font: 300 10px Inter, sans-serif;
                    font-style: italic;
                }
                .warning-text {
                    color: #f00;
                }
                .sale-price {
                    display: flex;
                    justify-content: space-between;
                    align-items: center;
                    color: #000;
                    font: 300 14px Inter, sans-serif;
                }
                .price-value {
                    color: #f00;
                }
                .qr-button {
        background-color: transparent;
        border: none;
        cursor: pointer;
        border-radius: 8px; /* Adjust as needed for rounded corners */
      
        transition: transform 0.2s ease, box-shadow 0.2s ease;
        display: inline-flex;
        align-items: center;
        justify-content: center;
        width: fit-content; /* Button wraps tightly around image */
        height: fit-content;
        padding: 1em 0 0 0; /* Remove padding to fit image closely */
    }

    .qr-button:hover {
        transform: scale(1.05);
       
    }

    .qr-button:active {
        transform: scale(0.95);
    }

    .qr-code {
        border-radius: 8px; /* Same as button for smooth edges */
    }
            `}</style>
                </section>
            



        </>

    );
};

export default SaleContent;
