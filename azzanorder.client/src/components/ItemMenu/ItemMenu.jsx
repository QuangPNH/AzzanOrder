import React from "react";
import MenuItem from "./MenuItem";

const menuItems = [
    {
        imageSrc: "https://cdn.builder.io/api/v1/image/assets/TEMP/46dd0124b24b68716eb89c2123db35cff84b973e2b0e63bcaab89a7b968c7bf4?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8",
        name: "Item",
        description: "Item Description here....",
        price: "15.000 đ",
    },
];

function ItemMenu() {
    return (
        <section className="item-menu">
            {menuItems.map((item, index) => (
                <MenuItem key={index} {...item} />
            ))}
            <style jsx>{`
        .item-menu {
          display: flex;
          max-width: 316px;
          flex-direction: column;
          color: #000;
          align-self: stretch;
          font: 400 12px Inter, sans-serif;
        }
      `}</style>
        </section>
    );
}

export default ItemMenu;