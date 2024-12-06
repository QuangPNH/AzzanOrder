import React, { useState, useEffect, useRef } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import ShowMoreLink from './ShowMoreLink/ShowMoreLink';
import ProductCard from './ProductCard/ProductCard';
import Dropdown from './Dropdown/Dropdown';
import { getCookie } from './Account/SignUpForm/Validate';
import API_URLS from '../config/apiUrls';

const fetchCategoriesAndProducts = async (setCategories, setProducts) => {
    try {
        const id = getCookie("tableqr") ? getCookie("tableqr").split('/')[1] : null;
        const url = id ? API_URLS.API + `ItemCategory/GetAllItemCategoriesValid?id=${id}` : API_URLS.API + 'ItemCategory/GetAllItemCategoriesValid';
        const response = await fetch(url);
        const categories = await response.json();
      

        const initialProducts = categories.reduce((acc, category) => {
            acc[category.description] = category.menuCategories.map(mc => ({
                menuItemId: mc.menuItem.menuItemId,
                title: mc.menuItem.itemName,
                price: mc.menuItem.price,
                description: mc.menuItem.description,
                imageBase64: mc.menuItem.image,
                category: category.description
            }));
            return acc;
        }, {});

        setProducts(initialProducts);
        setCategories(categories);
    } catch (error) {
        console.error('Error fetching categories and products:', error);
    }
};

const Menu = () => {
    const [products, setProducts] = useState({});
    const [categories, setCategories] = useState([]);
    const categoryRefs = useRef({});

    useEffect(() => {
        fetchCategoriesAndProducts(setCategories, setProducts);
    }, []);

    const handleDropdownChange = (selectedCategory) => {
        if (categoryRefs.current[selectedCategory]) {
            const offset = -400; // Adjust this value to scroll further up or down
            const elementPosition = categoryRefs.current[selectedCategory].getBoundingClientRect().top + window.scrollY;
            const offsetPosition = elementPosition + offset;

            window.scrollTo({
                top: offsetPosition,
                behavior: 'smooth'
            });
        }
    };

    return (
        <>
            <Header />
            <Dropdown
                onClick2={handleDropdownChange}
                onChange={handleDropdownChange} />
            {categories?.map((category) => (
                <div key={category.description} ref={el => categoryRefs.current[category.itemCategoryId] = el}>
                    <ShowMoreLink title={category.description} />
                    <div className='product-grid'>
                        {products[category.description]?.map((product) => (
                            <ProductCard
                                key={product.menuItemId}
                                id={product.menuItemId}
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

export default Menu;