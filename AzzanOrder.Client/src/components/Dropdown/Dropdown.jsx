﻿import { useState, useEffect } from 'react';
import DropdownItem from './DropdownItem';

const Dropdown = ({ options, onChange, onClick}) => {
    const [items, setItems] = useState([]);
    const [isExpanded, setIsExpanded] = useState(false);
    const [selectedItem, setSelectedItem] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            const data = await fetchLabels();
            setItems(data);
            setSelectedItem(data[0]);
        };

        fetchData();
    }, []);

    useEffect(() => {
        const interval = setInterval(() => {
            handleCyclingItems(items);
        }, 3000);

        return () => {
            clearInterval(interval);
        };
    }, [items]);

    const fetchLabels = async () => {
        try {
            const response = await fetch('https://localhost:7183/api/ItemCategory');
            const data = await response.json();
            return data;
        } catch (error) {
            console.error('Error fetching labels:', error);
            return [];
        }
    };

    const handleToggleDropdown = () => {
        setIsExpanded(!isExpanded);
    };

    const handleSelectItem = (item) => {
        setSelectedItem(item);
        setIsExpanded(false);
        onChange(item.description);
    };

    const handleCyclingItems = (items) => {
        const randomIndex = Math.floor(Math.random() * items.length);
        const nextItem = items[randomIndex];

        setSelectedItem(nextItem);
    };

    return (
        <>
            <div className="dropdown">
                <DropdownItem
                    label={selectedItem ? selectedItem.description : ''}
                    iconSrc={
                        !isExpanded
                            ? 'https://cdn.builder.io/api/v1/image/assets/TEMP/149dee1c832975b05bb91e7928d007f9cfbf8aff03b0c89e8080bdf1f9308e5f?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533'
                            : 'https://cdn.builder.io/api/v1/image/assets/TEMP/149dee1c832975b05bb91e7928d007f9cfbf8aff03b0c89e8080bdf1f9308e5f?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533'
                    }
                    onClick1={onClick}
                    onClick={handleToggleDropdown}
                />

                {isExpanded && (
                    <div className="dropdown-items">
                        {items.map((item, index) => (
                            <DropdownItem
                                key={index}
                                label={item.description}
                                onClick={() => handleSelectItem(item)}
                            />
                        ))}
                    </div>
                )}
            </div>

            <style>
                {`
                .dropdown {
                    position: -webkit-sticky; /* For Safari */
                    position: sticky;
                    top: 0; /* Adjust this value to control the sticky position */
                    border-radius: 10px;
                    background-color: #fff;
                    box-shadow: 0 5px 10px rgba(0, 0, 0, 0.25);
                    display: flex;
                    flex-direction: column;
                    max-width: 328px;
                    padding: 9px;
                    font: 600 16px Inter, sans-serif;
                    border: 1px solid rgba(0, 0, 0, 0.5);
                    cursor: pointer;
                    z-index: 1000; /* Ensure it stays above other content */
                }

                .dropdown-items {
                    display: flex;
                    flex-direction: column;
                    margin-top: 10px;
                }
                `}
            </style>
        </>
    );
};

export default Dropdown;
