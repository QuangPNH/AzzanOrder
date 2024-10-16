import React, { useState, useEffect } from 'react';

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

const ItemDetail = ({ closeModal, imageSrc, title, price, cate, desc }) => {

    const [products, setProducts] = useState([]); // Initialize products state as an empty array
    const [toppings, setToppings] = useState([]); // Initialize toppings state as an empty array

    useEffect(() => {
        fetchProducts({cate});
        fetchToppings();
    }, []);

    const fetchProducts = async (category) => {
        try {
            const response = await fetch(`https://localhost:7183/api/MenuItem/Category/${category.cate}`);
            const data = await response.json();
            const limitedData = data.slice(0, 4); // Get only the first 4 items from the data array
            setProducts(limitedData);
        } catch (error) {
            console.error('Error fetching products:', error);
        }
    };

    const fetchToppings = async () => {
        try {
            const response = await fetch(`https://localhost:7183/api/MenuItem/Category/TOPPING`);
            const data = await response.json();
            setToppings(data);
        } catch (error) {
            console.error('Error fetching toppings:', error);
        }
    };
   
    return (
        <>
            <TopBar closeModal={closeModal} />
            <ProductCardSingle
                imageSrc={imageSrc}
                name={title}
                price={price} />

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
                {toppings && toppings.map((topping) => (
                    <ToppingAdd
                        key={topping.menuItemId}
                        toppingName={topping.itemName}
                        toppingNameEnglish={topping.description}
                        toppingPrice={topping.price}
                    />
                ))}
            </div>

            <AddToCartButton />
            <Description content={desc} />

            <div>
                <ShowMoreLink title='RELATED PRODUCTS' />
                <div className='product-grid'>
                    {products && products.map((product) => (
                        <ProductCard
                            key={product.id}
                            imageSrc={product.imageBase64}
                            title={product.title}
                            price={product.price}
                            cate={product.categoryId}
                            desc={product.description}
                        />
                    ))}
                </div>
            </div>
        </>
    );
};

export default ItemDetail;
