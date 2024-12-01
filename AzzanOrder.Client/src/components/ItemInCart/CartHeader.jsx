import { useState, useEffect } from 'react';
import API_URLS from '../../config/apiUrls';

// Utility functions to get and set cookies
const getCookie = (name) => {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return decodeURIComponent(parts.pop().split(';').shift());
};

const setCookie = (name, value, days) => {
    const expires = new Date(Date.now() + days * 864e5).toUTCString();
    document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`;
};

const fetchDropdownOptions = async (id) => {
    if (id) {
        try {
            const response = await fetch(API_URLS.API + `Table/GetTablesByManagerId/${id}`);
            if (!response.ok) {
                throw new Error("Network response was not ok");
            }
            const data = await response.json();
            return data;
        } catch (error) {
            console.error("Failed to fetch dropdown options:", error);
            return [];
        }
    }
    return [];
};

const CartHeader = ({ headerText }) => {
    // Extract tableqr cookie and parse it
    const tableqr = getCookie("tableqr");
    const [selectedOption, setSelectedOption] = useState("QR_000");
    const [dropdownOptions, setDropdownOptions] = useState([]);
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);

    useEffect(() => {
        if (tableqr) {
            const [qr, id] = tableqr.split('/');
            const fetchData = async () => {
                const options = await fetchDropdownOptions(id);
                if (headerText) {
                    setSelectedOption("QR_000");
                    const matchingTable = options.find(option => option.qr === "QR_000" && String(option.employeeId) === String(id));
                    if (matchingTable) {
                        const newTableId = matchingTable.tableId;
                        setCookie("tableqr", `QR_000/${id}/${newTableId}`, 7);
                    }
                }
            };
            fetchData();
        }
    }, [headerText]);

    useEffect(() => {
        if (tableqr) {
            const [qr, id, tableId] = tableqr.split('/');
            const fetchData = async () => {
                const options = await fetchDropdownOptions(id);
                setDropdownOptions(options);
                setSelectedOption(qr);
                // Update the cookie with tableId if it's missing
                if (!tableId && options.length > 0) {
                    const matchingTable = options.find(option => option.qr === qr && String(option.employeeId) === String(id));
                    if (matchingTable) {
                        const newTableId = matchingTable.tableId;
                        setCookie("tableqr", `${qr}/${id}/${newTableId}`, 7);
                    }
                }
            };
            fetchData();
        }
    }, [tableqr]);

    const toggleDropdown = () => {
        setIsDropdownOpen(!isDropdownOpen);
    };

    const handleOptionSelection = (qr, tableId) => {
        const [, id] = tableqr.split('/'); // Extract the id part from the cookie
        setSelectedOption(qr); // Set selectedOption to qr
        setCookie("tableqr", `${qr}/${id}/${tableId}`, 7); // Update the tableqr cookie with the new qr, existing id, and new tableId
        setIsDropdownOpen(false);
    };

    return (
        <div className="cartHeader">
            <img src="../src/assets/shoppingCart1.svg" style={{ width: '30px', height: '26px' }} />
            <p>Cart at table</p>
            <div className="dropdown1">
                <div className="dropdown-toggle1" onClick={toggleDropdown}>
                    <p className="selected-option1">{selectedOption}</p>
                    <img
                        src="https://cdn.builder.io/api/v1/image/assets/TEMP/170034c8ccaf3722be922eaa999b1f02f19585d5b327ad61ff50748511ec2953?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8"
                        alt=""
                        className="dropdown-icon"
                    />
                </div>
                {isDropdownOpen && (
                    <div className="dropdown-menu1">
                        {dropdownOptions.map((option, index) => (
                            <button key={index} onClick={() => handleOptionSelection(option.qr, option.tableId)}>
                                {option.qr}
                            </button>
                        ))}
                    </div>
                )}
            </div>
            <style>{`
                .cartHeader {
                    display: flex;
                    align-items: center;
                    color: var(--primary-color);
                }
                .cartHeader p {
                    margin-left: 10px;
                }
                .dropdown1 {
                    margin-left: auto; /* Push the dropdown to the right */
                    position: relative;
                }
                .dropdown-toggle1 {
                    display: flex;
                    align-items: center;
                    cursor: pointer;
                }
                .dropdown-icon {
                    width: 22px;
                    height: 22px;
                }
                .selected-option1 {
                    margin-right: 10px;
                    color: var(--primary-color);
                }
                .dropdown-menu1 {
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
                .dropdown-menu1 button {
                    margin-bottom: 5px;
                    background: none;
                    border: none;
                    cursor: pointer;
                    color: var(--primary-color);
                }
                .dropdown-menu1 button:hover {
                    background-color: #f0f0f0;
                }
            `}</style>
        </div>
    );
};

export default CartHeader;