import React, { useState, useEffect } from 'react';


const fetchDropdownOptions = async (id) => {
    try {
        const response = await fetch(`https://localhost:7183/api/Table/GetTablesByManagerId/${id}`);
        if (!response.ok) {
            throw new Error("Network response was not ok");
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error("Failed to fetch dropdown options:", error);
        return [];
    }
};

const CartHeader = () => {

    const [isDropdownOpen, setIsDropdownOpen] = useState(false);
    const [selectedOption, setSelectedOption] = useState("0");
    const [dropdownOptions, setDropdownOptions] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const options = await fetchDropdownOptions(1);
            setDropdownOptions(options);
        };
        fetchData();
    }, []);

    const toggleDropdown = () => {
        setIsDropdownOpen(!isDropdownOpen);
    };

    const handleOptionSelection = (option) => {
        setSelectedOption(option);
        setIsDropdownOpen(false);
    };

    return (
        <div className="cartHeader">
            <img src="../src/assets/shoppingCart1.svg" style={{ width: '30px', height: '26px' }} />
            <p>Cart at table</p>
            <div className="dropdown">
                <div className="dropdown-toggle" onClick={toggleDropdown}>
                    <p className="selected-option">{selectedOption}</p>
                    <img
                        src="https://cdn.builder.io/api/v1/image/assets/TEMP/170034c8ccaf3722be922eaa999b1f02f19585d5b327ad61ff50748511ec2953?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8"
                        alt=""
                        className="dropdown-icon"
                    />
                </div>
                {isDropdownOpen && (
                    <div className="dropdown-menu">
                        {dropdownOptions.map((option, index) => (
                            <button key={index} onClick={() => handleOptionSelection(option.tableId)}>
                                {option.tableId}
                            </button>
                        ))}
                    </div>
                )}
            </div>
            <style jsx>{`
                .cartHeader {
                    display: flex;
                    align-items: center;
                    color: var(--Azzan-Color, #bd3326);
                }
                .cartHeader p {
                    margin-left: 10px;
                }
                .dropdown {
                    margin-left: auto; /* Push the dropdown to the right */
                    position: relative;
                }
                .dropdown-toggle {
                    display: flex;
                    align-items: center;
                    cursor: pointer;
                }
                .dropdown-icon {
                    width: 22px;
                    height: 22px;
                }
                .selected-option {
                    margin-right: 10px;
                    color: var(--Azzan-Color, #bd3326);
                }
                .dropdown-menu {
                    display: flex;
                    flex-direction: column;
                    position: absolute;
                    right: 16px;
                    top: 75%;
                    background: white;
                    border: 1px solid #ccc;
                    border-radius: 4px;
                    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                    z-index: 1000;
                    padding: 5px;
                    max-height: 100px; /* Limit the height of the dropdown */
                    overflow-y: auto; /* Make the dropdown scrollable */
                }
                .dropdown-menu button {
                    margin-bottom: 5px;
                    background: none;
                    border: none;
                    cursor: pointer;
                    color: var(--Azzan-Color, #bd3326);
                }
                .dropdown-menu button:hover {
                    background-color: #f0f0f0;
                }
            `}</style>
        </div>
    );
};

export default CartHeader;