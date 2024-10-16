import React, { useState } from 'react';

import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import TopBar from './MenuDetail/TopBar';
import ProductCardSingle from './MenuDetail/ProductCardSingle';
import AmountBar from './MenuDetail/AmountBar';
import ToppingAdd from './MenuDetail/ToppingAdd';
import AddToCartButton from './MenuDetail/AddToCartButton';
import Description from './MenuDetail/Description';
import CustomItem from './MenuDetail/CustomItem';
import ShowMoreLink from './ShowMoreLink/ShowMoreLink';
import ProductCard from './ProductCard/ProductCard';
const Homepage = () => {


    return (
        <>
            <TopBar />
            <ProductCardSingle
                imageSrc='https://cdn.builder.io/api/v1/image/assets/TEMP/60173c54a3ed014fe5a59386ec2a441bf961180f99b494537706a65900f41de2?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533'
                name='Tuan'
                price={1000} />

            <div>
                <CustomItem title="Lượng đường:" />
                <AmountBar />
            </div>

            <div>
                <CustomItem title="Lượng đá:" />
                <AmountBar />
            </div>

            <div>
                <CustomItem title="Toppings:" />
                <ToppingAdd toppingName='Thạch dừa' toppingNameEnglish='Coconut Jelly' toppingPrice={5000} />
                <ToppingAdd toppingName='Thạch dừa' toppingNameEnglish='Coconut Jelly' toppingPrice={5000} />
                <ToppingAdd toppingName='Thạch dừa' toppingNameEnglish='Coconut Jelly' toppingPrice={5000} />
            </div>

            <AddToCartButton />
            <Description content='A very good drink for your health' />

            <div>
                <ShowMoreLink title='RELATED PRODUCTS' />
                <ProductCard
                    imageSrc='https://cdn.builder.io/api/v1/image/assets/TEMP/60173c54a3ed014fe5a59386ec2a441bf961180f99b494537706a65900f41de2?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533'
                    name='Tuan'
                    price={1000}
                />
                <ProductCard
                    imageSrc='https://cdn.builder.io/api/v1/image/assets/TEMP/60173c54a3ed014fe5a59386ec2a441bf961180f99b494537706a65900f41de2?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533'
                    name='Tuan'
                    price={1000}
                />
                <ProductCard
                    imageSrc='https://cdn.builder.io/api/v1/image/assets/TEMP/60173c54a3ed014fe5a59386ec2a441bf961180f99b494537706a65900f41de2?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533'
                    name='Tuan'
                    price={1000}
                />
                <ProductCard
                    imageSrc='https://cdn.builder.io/api/v1/image/assets/TEMP/60173c54a3ed014fe5a59386ec2a441bf961180f99b494537706a65900f41de2?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533'
                    name='Tuan'
                    price={1000}
                />
            </div>


        </>
    );
};

export default Homepage;
