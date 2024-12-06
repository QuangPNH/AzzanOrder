import { useState, useEffect } from 'react';
import DropdownItem from './DropdownItem';
import { getCookie } from '../Account/SignUpForm/Validate';
import API_URLS from '../../config/apiUrls';

const Dropdown = ({ options, onChange, onClick}) => {
    const [items, setItems] = useState([]);
    const [isExpanded, setIsExpanded] = useState(false);
    const [selectedItem, setSelectedItem] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            const data = await fetchLabels(getCookie("tableqr") ? getCookie("tableqr").split('/')[1] : null);
            setItems(data);
            
            setSelectedItem();
        };
        fetchData();
        console.log(items);
    }, []);

    const fetchLabels = async (id) => {
        try {
            const url = id ? API_URLS.API + `ItemCategory/GetAllItemCategoriesValid?id=${id}` : API_URLS.API + 'ItemCategory/GetAllItemCategoriesValid';
            const response = await fetch(url);
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
        onChange(item.itemCategoryId); // sửa lại thành description thì sẽ menu sẽ hoạt động
    };


    return (
        <>
            <div className="dropdown">
                <DropdownItem
                    label={selectedItem ? selectedItem.description : 'ALL'}
                    iconSrc={
                        !isExpanded
                            ? 'https://cdn.builder.io/api/v1/image/assets/TEMP/149dee1c832975b05bb91e7928d007f9cfbf8aff03b0c89e8080bdf1f9308e5f?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533'
                            : 'https://cdn.builder.io/api/v1/image/assets/TEMP/60173c54a3ed014fe5a59386ec2a441bf961180f99b494537706a65900f41de2?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533&fbclid=IwZXh0bgNhZW0CMTEAAR2PR55PudHJ84uB3tn8Cx6eoZqDjOg8J1zPp-eGd945o90FACVUX1OLPTU_aem_fIfp5zTSe7E7_w3sPYsByg'
                    }
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

export default Dropdown;
