import React, { useState, useEffect } from 'react';




const SaleContent = ({ saleAmount, price, infiniteUses, useCount, bought }) => {
    const [vouchers, setVouchers] = useState([]);

    useEffect(() => { voucherDetails(); }, []);
    const voucherDetails = async () => {
        try {
            const response = await fetch('https://localhost:7183/api/VoucherDetail');
            const data = await response.json();
            setVouchers(data);
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };
    // return (
    //     <div>
    //         <h1>Voucher List</h1>

    //     </div>
    // );
    return (

        <section className="sale-content">
            <div className="sale-info">
                <div className="sale-percentage">Sale <span className="discount">{saleAmount}%</span></div>
                {bought ? (<div> </div>) : (
                    <div className="sale-icon-wrapper">
                        <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/e2eac9604a81877b92936d217befed7bedb4e1a3b5ae5a5bae1e56c9c943e889?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533" alt="" className="sale-icon-bg" />
                        {infiniteUses ? (
                            <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/b3126d40803d317ccbc4a81359fffa8e4a11814bc0316903ab7e68fd911c1ce9?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533" alt="Infinite Sale icon" className="sale-icon" />
                        ) : (
                            <div className="sale-uses">x{useCount}</div>
                        )}

                    </div>
                )}
            </div>
            <p className="sale-warning">
                <em><strong className="warning-text">Warning:</strong> This voucher only use in web.</em>
            </p>

            {bought ? (<div> </div>) : (<div className="sale-price">
                <p>Price: <span className="price-value">{price} points</span></p>  {/* Dynamic price */}
                <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/66181310e2851f8fae57b02cad765f22e6988ffb6004f773591a0d8561aba4e0?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533" alt="QR code" className="qr-code" />
            </div>)}

            <style jsx>{`
                .sale-content {
                    border-radius: 0 10px 10px 0;
                    width: 50em;
                    background-color: #f6b5b5;
                    display: flex;
                    flex-direction: column;
                    overflow: hidden;
                    padding: 5px 0 5px 18px;
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
                    padding: 3px 5px 9px;
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
                .qr-code {
                    aspect-ratio: 2.44;
                    object-fit: contain;
                    width: 44px;
                    border-radius: 5px;
                }
            `}</style>
        </section>
    );
};

export default SaleContent;
