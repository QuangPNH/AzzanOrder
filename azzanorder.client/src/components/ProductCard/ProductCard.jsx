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

const ProductCard = ({ imageSrc, title, price, desc, cate, id }) => {
    const handleAddToCart = () => {
        const storedData = getCookie('cartData');
        let parsedData = [];
        if (storedData) {
            parsedData = JSON.parse(storedData);
        }
        const existingItemIndex = parsedData.findIndex(item =>
            item.name === title &&
            item.options.selectedSugar === 'normal' &&
            item.options.selectedIce === 'normal' &&
            JSON.stringify(item.options.toppings) !== null
        );

        if (existingItemIndex !== -1) {
            // Item exists, increase quantity
            parsedData[existingItemIndex].quantity += 1;
        } else {
            const newItem = {
                key: generateRandomKey(5),
                id: id,
                name: title,
                options: {
                    selectedSugar: 'normal',
                    selectedIce: 'normal'
                },
                price: price,
                quantity: 1
            };
            parsedData.push(newItem);
        }
        setCookie("cartData", JSON.stringify(parsedData), 0.02);
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
                    id={id}
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