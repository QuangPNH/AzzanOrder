import { useState } from 'react';

const ItemCheck = ({ label, value, onChange }) => {
    const [checked, setChecked] = useState(value);

    const handleCheckboxChange = (event) => {
        setChecked(event.target.checked);
        onChange(event.target.checked);
    };

    return (
        <div className="price-item">
            <span className="label1">{label}</span>
            <input
                type="checkbox"
                className="value"
                checked={checked}
                onChange={handleCheckboxChange}
            />
            <style jsx>{`
                .price-item {
                    color: var(--primary-color);
                    font: 400 16px Inter, sans-serif;
                    margin: 8px 0;
                    width: 100%;
                }
                .label1 {
                    float: left;
                }
                .value {
                    float: right;
                }
            `}</style>
        </div>
    );
};

export default ItemCheck;