import React, { useState, useEffect, useRef } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import ShowMoreLink from './ShowMoreLink/ShowMoreLink';
import ProductCard from './ProductCard/ProductCard';
import Dropdown from './Dropdown/Dropdown';

const Homepage = () => {
    const [products, setProducts] = useState({});
    const [categories, setCategories] = useState([]);
    const categoryRefs = useRef({});

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await fetch('https://localhost:7183/api/ItemCategory');
                const categories = await response.json();
                const initialProducts = categories.reduce((acc, category) => {
                    acc[category.description] = [];
                    return acc;
                }, {});
                setProducts(initialProducts);
                setCategories(categories);

                // Fetch products for each category
                categories.forEach(category => fetchProducts(category));
            } catch (error) {
                console.error('Error fetching categories:', error);
            }
        };

        fetchCategories();
    }, []);

    const fetchProducts = async (category) => {
        try {
            const response = await fetch(`https://localhost:7183/api/MenuItem/Category/${category.description}`);
            const data = await response.json();
            setProducts(prevProducts => ({
                ...prevProducts,
                [category.description]: data
            }));
        } catch (error) {
            console.error('Error fetching products:', error);
        }
    };

    const handleDropdownChange = (selectedCategory) => {
        if (categoryRefs.current[selectedCategory]) {
            categoryRefs.current[selectedCategory].scrollIntoView({ behavior: 'smooth' });
        }
    };
    return (
        <>
            <Header />
            <Dropdown
                options={categories.map(category => category.description)}
                onClick={handleDropdownChange}
                onChange={handleDropdownChange} />
            {categories.map((category) => (
                <div key={category.description} ref={el => categoryRefs.current[category.description] = el}>
                    <ShowMoreLink title={category.description}/>
                    <div className='product-grid'>
                        {products[category.description]?.map((product) => (
                            <ProductCard
                                key={product.id}
                                imageSrc={product.imageBase64}
                                title={product.title}
                                price={product.price}
                                cate={product.category}
                                desc={product.description}
                            />
                        ))}
                    </div>
                </div>
            ))}
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
