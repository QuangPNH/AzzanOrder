import { useState, useEffect } from 'react';
import DropdownItem from '../Dropdown/DropdownItem';

function getVoucherList() {
    const v = getCookie("voucher");
    if (!v) {
        return [];
    }
    try {
        const vou = JSON.parse(v);
        return vou || [];
    } catch (error) {
        console.error("Error parsing cart data from cookie:", error);
        return [];
    }
}

const DropTest = ({ options, onChange }) => {
    const [items, setItems] = useState(getVoucher());
    const [isExpanded, setIsExpanded] = useState(false);
    const [selectedItem, setSelectedItem] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            if (getCookie('memberInfo')) {
                const data = await fetchMemberVouchers(JSON.parse(getCookie('memberInfo')).memberId, '');
                setItems(data);
                setSelectedItem(items); // Chọn mục đầu tiên
            }
        };
        fetchData();
    }, []);

    

    const fetchMemberVouchers = async (memberId, categoryId) => {
        try {
            const response = categoryId != '' ? await fetch(`https://localhost:7183/api/MemberVouchers/memberId/itemCategoryId?memberId=${memberId}&categoryId=${categoryId}`) : await fetch(`https://localhost:7183/api/MemberVouchers/memberId?memberId=${memberId}`);
            const data = await response.json();
            return data;

        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };

    // useEffect(() => {
    //     const fetchData = async () => {
    //         const data = await fetchLabels();
    //         setItems(data);
    //         setSelectedItem(); // Chọn mục đầu tiên
    //     };

    //     fetchData();
    // }, []);

    // const fetchLabels = async () => {
    //     try {
    //         const response = await fetch('https://localhost:7183/api/ItemCategory');
    //         const data = await response.json();
    //         return data;
    //     } catch (error) {
    //         console.error('Error fetching labels:', error);
    //         return [];
    //     }
    // };

    const handleToggleDropdown = () => {
        setIsExpanded(!isExpanded);
    };

    const handleSelectItem = (item) => {
        setSelectedItem(item);
        setIsExpanded(false);
        onChange(item); // Gửi cả item thay vì chỉ description
    };

    return (
        <>
            <div className="dropdown">
                <DropdownItem
                    label={selectedItem ? selectedItem.title + " Sale " + selectedItem.discount + "%" : ''}
                    iconSrc={
                        !isExpanded
                            ? 'https://cdn.builder.io/api/v1/image/assets/TEMP/149dee1c832975b05bb91e7928d007f9cfbf8aff03b0c89e8080bdf1f9308e5f?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533'
                            : 'https://cdn.builder.io/api/v1/image/assets/TEMP/60173c54a3ed014fe5a59386ec2a441bf961180f99b494537706a65900f41de2?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533'
                    }
                    onClick={handleToggleDropdown}
                />

                {isExpanded && (
                    <div className="dropdown-items">
                        {items.map((item, index) => (
                            <DropdownItem
                                key={index}
                                label={item.title + " Sale " + item.discount + "%"}
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
                    top: 50px; /* Adjust this value to control the sticky position */
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
                    z-index: 999; /* Ensure it stays above other content */
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
function setCookie(name, value, days) {
    const expires = new Date(Date.now() + days * 864e5).toUTCString(); // Calculate expiration date
    document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`; // Set cookie
}

function getCookie(name) {
    const value = `; ${document.cookie}`; // Add a leading semicolon for easier parsing
    const parts = value.split(`; ${name}=`); // Split the cookie string to find the desired cookie
    if (parts.length === 2) return decodeURIComponent(parts.pop().split(';').shift()); // Return the cookie value
}
export default DropTest;