import React from 'react';
import MenuItem from './MenuItem';

const MenuComponent = ({title, imageurl}) => {
    const menuItems = [
        { icon: imageurl, label: title }
    ];

    return (
        <nav className="menu-container">
            {menuItems.map((item, index) => (
                <MenuItem key={index} icon={item.icon} label={item.label} />
            ))}
            <style jsx>{`
        .menu-container {
          display: flex;
          max-width: 82px;
          flex-direction: column;
          align-items: center;
          justify-content: start;
        }
      `}</style>
        </nav>
    );
};

export default MenuComponent;