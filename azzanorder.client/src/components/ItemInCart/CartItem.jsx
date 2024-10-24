import React, { useState, useEffect } from "react";  
import QuantityControl from "./QuantityControl";  

const CartItem = ({  
   name,  
   options,  
   price,  
   quantity  
}) => {  
   const [currentQuantity, setCurrentQuantity] = useState(quantity);  
   const [totalPrice, setTotalPrice] = useState(price * quantity);  

   const handleQuantityChange = (newQuantity) => {  
       setCurrentQuantity(newQuantity);  
   };  

   useEffect(() => {  
       setTotalPrice(price * currentQuantity);  
   }, [currentQuantity, price]);  

   return (  
       <article className="cart-item">  
           <div className="cart-item-content">  
               <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/febac510c6d584a90466fa7c52c5724906fbbf8624bfa03ec21ea8114b229195?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8" alt="" className="item-icon" />  
               <div className="item-details">  
                   <div className="item-info">  
                       <div className="item-header">  
                           <h3 className="item-name">{name}</h3>  
                       </div>  
                       {Array.isArray(options) ? (  
                           options.map((option, index) => (  
                               <p key={index} className="item-option">  
                                   {option}  
                               </p>  
                           ))  
                       ) : (  
                           <p className="item-option">No options available</p>  
                       )}  
                   </div>  
                   <img src="https://cdn.builder.io/api/v1/image/assets/TEMP/fbbe8e49109e7ba499ad0129a45932bcb03213d85b1c078668fd9d88bca09d81?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8" alt="Delete item" className="delete-icon" />  
               </div>  
           </div>  
           <div className="cart-item-footer">  
               <p className="item-price">{price * currentQuantity}</p>  
               <QuantityControl quantity={currentQuantity} onQuantityChange={handleQuantityChange} />  
           </div>  
           <style jsx>{`  
       .cart-item {  
         display: flex;  
         width: 100%;  
         flex-direction: column;  
       }  
       .cart-item-content {  
         display: flex;  
         width: 100%;  
         gap: 12px;  
         color: rgba(30, 30, 30, 0.6);  
         font: 400 12px/1.4 Inter, sans-serif;  
       }  
       .item-icon {  
         aspect-ratio: 1;  
         object-fit: contain;  
         object-position: center;  
         width: 20px;  
         align-self: start;  
       }  
       .item-details {  
         display: flex;  
         align-items: flex-start;  
         flex-grow: 1;  
         flex-basis: auto;  
       }  
       .item-info {  
         align-self: end;  
         display: flex;  
         margin-right: -34px;  
         height: 46px;  
         width: 100%;  
         flex-direction: column;  
         justify-content: start;  
       }  
       .item-header {  
         display: flex;  
         width: 100%;  
         align-items: center;  
         gap: 40px 100px;  
         color: var(--sds-color-text-default-default);  
         justify-content: space-between;  
         font: var(--sds-typography-body-font-weight-regular)  
           var(--sds-typography-body-size-medium)  
           var(--sds-typography-body-font-family);  
       }  
       .item-name {  
         align-self: stretch;  
         margin: auto 0;  
       }  
       .item-option {  
         margin-top: 4px;  
       }  
       .delete-icon {  
         aspect-ratio: 1.12;  
         object-fit: contain;  
         object-position: center;  
         width: 27px;  
         align-self: start;  
       }  
       .cart-item-footer {  
         align-self: end;  
         display: flex;  
         margin-top: 22px;  
         width: 100%;  
         max-width: 241px;  
         gap: 20px;  
         justify-content: space-between;  
       }  
       .item-price {  
         color: var(--Text-Default-Secondary, #757575);  
         text-decoration-line: underline;  
         margin: auto 0;  
         font: var(--sds-typography-body-font-weight-regular)  
           var(--sds-typography-body-size-small) / 1.4  
           var(--sds-typography-body-font-family);  
       }  
     `}</style>  
       </article>  
   );  
};  

export default CartItem;
