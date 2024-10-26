import React, { useState } from 'react';
import Modal from 'react-modal';
import Image from './Image';
import PriceTag from './PriceTag';
import ItemDetail from '../ItemDetail'; // Adjusted the import path
import { getCookie, setCookie } from '../Account/SignUpForm/Validate';

export function generateRandomKey(length) {
  const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  let key = '';
  for (let i = 0; i < length; i++) {
    const randomIndex = Math.floor(Math.random() * characters.length);
    key += characters.charAt(randomIndex);
  }
  return key;
}

const ProductCard = ({ imageSrc, title, price, desc, cate }) => {
    const handleAddToCart = () => {
        console.log("Add to cart");
        const storedData = getCookie('cartData');
        let parsedData = [];
        if (storedData) {
            parsedData = JSON.parse(storedData);
        }
        const newItem = {
            key: generateRandomKey(5),
            name: title,
            options: {
                selectedSugar: '100',
                selectedIce: '100'
            },
            price: price,
            quantity: 1
        };
        parsedData.push(newItem);
        setCookie("cartData", JSON.stringify(parsedData),7);
        
    };

    const [modalIsOpen, setModalIsOpen] = useState(false);

    const openModal = () => {
        setModalIsOpen(true);
    };

    const closeModal = () => {
        setModalIsOpen(false);
    };

    return (
        <article className="product-card">
            <Image src={imageSrc} alt={title} onClick={openModal} />
            <h2 className="product-title" onClick={openModal}>{title}</h2>
            <PriceTag price={price} onclick={handleAddToCart} />
            <style jsx>
                {`
                .product-card {
                  border-radius: 8px;
                  display: flex;
                  max-width: 154px;
                  flex-direction: column;
                  font-family: Inter, sans-serif;
                  font-weight: 700;
                  background-color: #f9f9f9;
                  padding: 10px;
                }
                .product-title {
                    cursor: pointer;
                  color: #000;
                  font-size: 14px;
                  margin-top: 13px;
                }
                `}
            </style>
            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                contentLabel="Item Menu"
                style={{
                    content: {
                        top: '60%',
                        left: '50%',
                        right: 'auto',
                        bottom: 'auto',
                        marginRight: '-50%',
                        transform: 'translate(-50%, -50%)',
                        maxHeight: '80vh',
                        position: 'absolute',
                        zIndex: 3,
                        overflowY: 'auto',
                        width: '80%',
                    },
                    overlay: {
                        zIndex: 2, // Ensure the overlay is behind the modal
                        backgroundColor: 'rgba(0, 0, 0, 0.7)'
                    }
                }}
            >
                <ItemDetail
                    closeModal={closeModal}
                    imageSrc={imageSrc}
                    title={title}
                    price={price}
                    cate={cate}
                    desc={desc}
                />
            </Modal>
        </article>

    );
};

export default ProductCard;