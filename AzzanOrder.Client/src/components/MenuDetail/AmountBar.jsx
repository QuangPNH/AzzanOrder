import React, { useState } from 'react';
import AmountIcon from './AmountIcon';

const progressSteps = [
    { value: '0', label: '0%' },
    { value: '30', label: '30%' },
    { value: '50', label: '50%' },
    { value: '80', label: '80%' },
    { value: '100', label: '100%' }
];

function AmountBar() {
    const [selectedValue, setSelectedValue] = useState('50'); // Default selected value

    const handleStepClick = (value) => {
        setSelectedValue(value); // Update the selected value when a step is clicked
    };

    return (
        <section className="amount-bar">
            {progressSteps.map((step) => (
                <AmountIcon
                    key={step.value}
                    label={step.label}
                    isSelected={step.value === selectedValue} // Check if the step is selected
                    onClick={() => handleStepClick(step.value)} // Handle click event
                />
            ))}
            <style jsx>{`
                .amount-bar {
                    display: flex;
                    justify-content: space-between; /* Evenly distribute buttons */
                    gap: 10px; /* Space between buttons */
                    color: #000;
                    white-space: nowrap;
                    text-align: center;
                    font: 200 15px Inter, sans-serif;
                    max-width: 100%; /* Ensure it doesn't exceed the parent width */
                    overflow: hidden; /* Hide any overflow to prevent scrolling */
                }
            `}</style>
        </section>
    );
}

export default AmountBar;
