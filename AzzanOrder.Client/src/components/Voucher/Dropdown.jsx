import { useState, useEffect } from 'react';
import DropdownItem from '../Dropdown/DropdownItem';

const Dropdown = ({ options, onChange }) => {
    const [items, setItems] = useState([]);
    const [isExpanded, setIsExpanded] = useState(false);
    const [selectedItem, setSelectedItem] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            const data = await fetchLabels();
            setItems(data);
            setSelectedItem(data[0]); // Chọn mục đầu tiên
        };

        fetchData();
    }, []);

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
        onChange(item); // Gửi cả item thay vì chỉ description
    };

    return (
        <div className="dropdown">
            <DropdownItem
                label={selectedItem ? selectedItem.description : ''}
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
                            label={item.description}
                            onClick={() => handleSelectItem(item)}
                        />
                    ))}
                </div>
            )}
        </div>
    );
};
