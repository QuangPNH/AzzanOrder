import React, { useState } from 'react';

/*
    **import biến PriceCalculator từ file PriceCalculator
    **phải có export từ hàm PriceCalculator
*/
import MenuMainPage from './MenuMainPage';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import ShowMoreLink from './ShowMoreLink/ShowMoreLink';
import ProductCard from './ProductCard/ProductCard';
import HomeItem from './HomeItem/HomeItem';
import PriceCalculator from './PriceCalculator/PriceCalculator'
const Homepage = () => {
    

    const handleClick = () => {
        aiRun();
    }
    return (
            <><div className="page-container">
            <Header />
            <HomeItem />
            <ShowMoreLink title="HOT ITEMS" url='https://google.com' />
            <div className='product-grid'>
                <ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8" title="Product Title" price="1000" />
                <ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8" title="Product Title" price="1000" />
                <ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8" title="Product Title" price="1000" />
                <ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8" title="Product Title" price="1000" />
            </div>
        </div><Footer /><style jsx>{`
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
                `}</style>
            //Import PriceCalculator trong Function Return
            <PriceCalculator /></>
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

export default Homepage;
