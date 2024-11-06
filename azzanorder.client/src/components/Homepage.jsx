import React, { useState, useEffect } from 'react';
import { useLocation } from "react-router-dom";

/*
    **import biến PriceCalculator từ file PriceCalculator
    **phải có export từ hàm PriceCalculator
*/
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import ShowMoreLink from './ShowMoreLink/ShowMoreLink';
import ProductCard from './ProductCard/ProductCard';
import Navbar from './HomeItem/Navbar';
import Frame from './HomeItem/Frame';
import { getCookie } from './Account/SignUpForm/Validate';

    // Rest of the code...
import Cart from './Cart';
import { del } from 'framer-motion/client';
import Banner from './HomeItem/Banner';

const Homepage = () => {
    const [menuItems, setMenuItems] = useState([]);
    const [recentMenuItems, setRecentMenuItems] = useState([]);
    const [showRecentlyOrdered, setShowRecentlyOrdered] = useState(false);
    const search = useLocation().search;
    const id=new URLSearchParams(search).get("tableqr");

    useEffect(() => {
        console.log(id, 'hello');
        const memberInfo = getCookie('memberInfo');
        const memberId = memberInfo ? JSON.parse(memberInfo).memberId : null;

        const fetchData = async () => {
            await fetchMenuItems(id ? id.split('/')[1] : null);
            if (memberId) {
                await fetchRecentMenuItems(memberId, id ? id.split('/')[1] : null);
                setShowRecentlyOrdered(true);
            }
            if (id) {
                setCookie('tableqr', id, 1);
                await fetchOrderExits(id.split('/')[0], id.split('/')[1]);
            }
        };

        fetchData();
    }, [id]);

    const fetchMenuItems = async (manaId) => {
        try {
            const url = manaId ? `https://localhost:7183/api/MenuItem/top4?manaId=${manaId}` : 'https://localhost:7183/api/MenuItem/top4';
            const response = await fetch(url);
            const data = await response.json();
            setMenuItems(data);
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    const fetchOrderExits = async (tableQr, manaId) => {
        try {
            const response = await fetch(`https://localhost:7183/api/Order/GetOrderByTableQr/${tableQr}/${manaId}`);
            if (response.ok) {
                window.location.href = '/order';
            }
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    const fetchRecentMenuItems = async (customerId, manaId) => {
        try {
            const url = manaId ? `https://localhost:7183/api/MenuItem/RecentMenuItems/${customerId}?manaId=${manaId}` : `https://localhost:7183/api/MenuItem/RecentMenuItems/${customerId}`;
            const response = await fetch(url);
            const data = await response.json();
            setRecentMenuItems(data);
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    return (
        <>
            <Header />
            <div className="page-container">
                <Navbar />
                <ShowMoreLink title="LIMITED COMBO" />
                <Frame />
                <div>
                    {showRecentlyOrdered && (
                        <div>
                            <ShowMoreLink title="RECENTLY ORDERED" />
                            <div className='product-grid'>
                                {recentMenuItems?.map((menuItem) => (
                                    <ProductCard
                                        key={menuItem.menuItemId}
                                        id={menuItem.menuItemId}
                                        title={menuItem.itemName}
                                        price={menuItem.price}
                                        imageSrc={menuItem.imageBase64}
                                        cate={menuItem.category}
                                        desc={menuItem.description}
                                    />
                                ))}
                            </div>
                        </div>
                    )}
                    
                    <ShowMoreLink title="HOT ITEMS" />
                    <div className='product-grid'>
                        {menuItems?.map((menuItem) => (
                            <ProductCard
                                key={menuItem.menuItemId}
                                id={menuItem.menuItemId}
                                title={menuItem.itemName}
                                price={menuItem.price}
                                imageSrc={menuItem.imageBase64}
                                cate={menuItem.category}
                                desc={menuItem.description}
                            />
                        ))}
                    </div>
                    <Banner />
                </div>
                <style jsx>{`
                .page-container {
                    display: flex;
                    flex-direction: column;
                    align-items: center;
                    padding: 0 20px;
                    width: 100%;
                    box-sizing: border-box;
                }

                .product-grid {
                    display: flex;
                    flex-wrap: wrap;
                    justify-content: center; /* Center-align the grid */
                    gap: 20px;
                    width: 100%;
                    max-width: 1200px; /* Set a max-width to avoid stretching */
                }
                .product-grid > * {
      flex: 1 1 calc(50% - 20px); /* Ensures 2 items per row */
      box-sizing: border-box;
    }
                `}
                </style>
                
            </div>
            <Footer />
        </>
        /*
        <div>
            <div style={{ display: 'flex' }}>
                <input placeholder='Search Food with Category using Generative AI' onChange={(e) => handleChangeSearch(e)} />
                <button style={{ marginLeft: '20px' }} onClick={() => handleClick()}>Search</button>
            </div>
                .product-grid > * {
                    flex: 1 1 calc(50% - 20px);
                    box-sizing: border-box;
                }
            {
                loading == true && (aiResponse == '') ?
                    <p style={{ margin: '30px 0' }}>Loading ...</p>
                    :
                    <div style={{ margin: '30px 0' }}>
                        <p>{aiResponse}</p>
                    </div>
            }
        </div>
        */
        //<MenuButton title="Menu" imageurl='../src/assets/book.svg' />
        //<ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8" title="Product Title" price="1000" />
        //<ShowMoreLink title="Recently Ordered" url='https://google.com' />

        //<WarningWindow />
        //<MenuMainPage />
    );
};


function setCookie(name, value, days) {
    const expires = new Date(Date.now() + days * 864e5).toUTCString(); // Calculate expiration date
    document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`; // Set cookie
  }



  function deleteCookie(name) {
    setCookie(name, '', -1); // Call setCookie with negative days to delete
  }

export default Homepage;
