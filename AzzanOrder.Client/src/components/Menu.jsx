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
const Homepage = () => {
    const [products, setProducts] = useState({
        COFFEE: [],
        TEA: [],
        YOGURT: [],
        'SMOOTHIE&FRUIT JUICE': [],
        'ICE BLENDED': []
    });

    // Function to fetch products from API based on the title
    const fetchProducts = async (title, category) => {
        try {
            const response = await fetch(`https://localhost:7183/api/MenuItem/Category/${title}`);
            const data = await response.json();
            setProducts(prevProducts => ({
                ...prevProducts,
                [category]: data
            }));
        } catch (error) {
            console.error('Error fetching products:', error);
        }
    };

    return (
        <>
            <Header />
            <div>
                <ShowMoreLink title="COFFEE" url='https://google.com' />
                <div className='product-grid'>
                    {
                        fetchProducts('COFFEE', 'COFFEE'),
                        products.COFFEE.map((product) => (
                            <ProductCard
                                key={product.id}
                                imageSrc={product.imageSrc}
                                title={product.title}
                                price={product.price}
                            />
                        ))}
                </div>
            </div>
            <div>
                <ShowMoreLink title="TEA" url='https://google.com' />
                <div className='product-grid'>
                    {
                        fetchProducts('TEA', 'TEA'),
                        products.TEA.map((product) => (
                            <ProductCard
                                key={product.id}
                                imageSrc={product.imageSrc}
                                title={product.title}
                                price={product.price}
                            />
                        ))}
                </div>
            </div>
            <div>
                <ShowMoreLink title="YOGURT" url='https://google.com' />
                <div className='product-grid'>
                    {
                        fetchProducts('YOGURT', 'YOGURT'),
                        products.YOGURT.map((product) => (
                            <ProductCard
                                key={product.id}
                                imageSrc={product.imageSrc}
                                title={product.title}
                                price={product.price}
                            />
                        ))}
                </div>
            </div>
            <div>
                <ShowMoreLink title="SMOOTHIE&FRUIT JUICE" url='https://google.com' />
                <div className='product-grid'>
                    {
                        fetchProducts('SMOOTHIE&FRUIT JUICE', 'SMOOTHIE&FRUIT JUICE'),
                        products['SMOOTHIE&FRUIT JUICE'].map((product) => (
                            <ProductCard
                                key={product.id}
                                imageSrc={product.imageSrc}
                                title={product.title}
                                price={product.price}
                            />
                        ))}
                </div>
            </div>
            <div>
                <ShowMoreLink title="ICE BLENDED" url='https://google.com' />
                <div className='product-grid'>
                    {
                        fetchProducts('ICE BLENDED', 'ICE BLENDED'),
                        products['ICE BLENDED'].map((product) => (
                            <ProductCard
                                key={product.id}
                                imageSrc={product.imageSrc}
                                title={product.title}
                                price={product.price}
                            />
                        ))}
                </div>
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
                `}</style>
            <Footer />
        </>
    );
};

export default Homepage;
