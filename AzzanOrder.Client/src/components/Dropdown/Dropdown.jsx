import React, { useState } from 'react';
import DropdownItem from './DropdownItem';

const Dropdown = () => {
  const items = [
    { label: 'Coffee' },
    { label: 'Tea' },
    { label: 'Juice' }
  ];

  const [isExpanded, setIsExpanded] = useState(false);
  const [selectedItem, setSelectedItem] = useState(items[0]); // Set the first item as default

  // Handle toggle dropdown
  const handleToggleDropdown = () => {
    setIsExpanded(!isExpanded);
  };

  // Handle selecting an item
  const handleSelectItem = (item) => {
    setSelectedItem(item);  // Set selected item
    setIsExpanded(false);    // Collapse dropdown after selection
  };

  return (
    <>
      <div className="dropdown">
        {/* Show the selected item and icon when collapsed */}
        <DropdownItem
          label={selectedItem.label}
          iconSrc={
            !isExpanded
              ? 'https://cdn.builder.io/api/v1/image/assets/TEMP/149dee1c832975b05bb91e7928d007f9cfbf8aff03b0c89e8080bdf1f9308e5f?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533'
              : null
          }
          onClick={handleToggleDropdown}
        />

        {/* Only show items if dropdown is expanded */}
        {isExpanded && (
          <div className="dropdown-items">
            {items.map((item, index) => (
              <DropdownItem
                key={index}
                label={item.label}
                onClick={() => handleSelectItem(item)}
              />
            ))}
          </div>
        )}
      </div>

      <style jsx>{`
        .dropdown {
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
        }

        .dropdown-items {
          display: flex;
          flex-direction: column;
          margin-top: 10px;
        }
      `}</style>
    </>
  );
};

export default Dropdown;
