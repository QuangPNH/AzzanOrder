import { useState, useEffect } from 'react';
import DropdownItem from '../Dropdown/DropdownItem';
import { getCookie, setCookie, deleteCookie } from '../Account/SignUpForm/Validate';
import API_URLS from '../../config/apiUrls';

function getVoucher() {
    const v = getCookie("voucher");
    if (!v) {
        return [];
    }
    try {
        let vou = JSON.parse(v);
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
    const id = getCookie("tableqr");
    useEffect(() => {
        const fetchData = async () => {
            if (getCookie('memberInfo')) {
                if (id) {
                    const data = await fetchMemberVouchers(JSON.parse(getCookie('memberInfo')).memberId, '', id.split('/')[1]);
                    setItems(data);
                   // Chọn mục đầu tiên
                }
                else {
                    const data = await fetchMemberVouchers(JSON.parse(getCookie('memberInfo')).memberId, '', '');
                    setItems(data);
                    // Chọn mục đầu tiên
                }
                items != '' ? setSelectedItem(items) : setSelectedItem(); 
            }
        };
        fetchData();
    }, []);



    const fetchMemberVouchers = async (memberId, categoryId, id) => {
        try {
            const response = categoryId != '' ? await fetch(API_URLS.API + `MemberVouchers/memberId/itemCategoryId?memberId=${memberId}&categoryId=${categoryId}&employeeId=${id}`) : await fetch(API_URLS.API + `MemberVouchers/memberId?memberId=${memberId}&employeeId=${id}`);
            const data = await response.json();
            let a = [];
            for (let i of data) {
                if (i.endDate > new Date().toISOString() || i.endDate === null) {
                    a.push(i);
                }
            }
            return a;

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
    //         const response = await fetch(API_URLS.API + 'ItemCategory');
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

    // const handleSelectItem = (item) => {
    //     setSelectedItem(item);
    //     setIsExpanded(false);
    //     onChange(item); // Gửi cả item thay vì chỉ description
    // };
    const handleSelectItem = (item) => {
        if (selectedItem && selectedItem.voucherDetailId == item.voucherDetailId) {
            // Nếu người dùng click vào voucher đã chọn, xóa cookie và đặt lại trạng thái
            deleteCookie("voucher");
            setSelectedItem(null); // Đặt lại selectedItem là null khi không chọn voucher
            onChange(null); // Thông báo với parent component rằng không có voucher nào được chọn
            setIsExpanded(false); // Đóng dropdown
        } else {
            // Nếu người dùng chọn một voucher khác
            setSelectedItem(item); // Cập nhật voucher đã chọn
            setCookie("voucher", JSON.stringify(item), 7); // Lưu voucher đã chọn vào cookie
            onChange(item); // Thông báo với parent component voucher đã chọn
            setIsExpanded(false); // Đóng dropdown
        }
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
                                iconSrc={selectedItem && selectedItem.voucherDetailId == item.voucherDetailId ? 'https://img.icons8.com/ios-filled/50/000000/checkmark.png' : ''} // Hiển thị dấu tích nếu là voucher đã chọn
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

export default DropTest;