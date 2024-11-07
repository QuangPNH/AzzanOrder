import React from 'react';
import AmountIcon from './AmountIcon';

const progressSteps = [
    { value: 'less', label: 'less' },
    { value: 'normal', label: 'normal' },
    { value: 'more', label: 'more' }
];

function AmountBar({ selectedValue, onStepClick, compact }) {
    return (
        <section className={`amount-bar ${compact ? 'compact' : ''}`}>
            {progressSteps.map((step) => (
                <AmountIcon
                    key={step.value}
                    label={step.label}
                    isSelected={step.value === selectedValue} // Check if the step is selected
                    onClick={() => onStepClick(step.value)} // Handle click event
                    compact={compact }
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
                .amount-bar.compact {
                    gap: 0px; /* Reduce space between buttons */
                    font-size: 12px; /* Smaller font size */
                }
            `}</style>
        </section>
    );
}

export default AmountBar;