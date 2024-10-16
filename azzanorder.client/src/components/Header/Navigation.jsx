import React from "react";

const Navigation = ({ toggleNavbar }) => {
    const navItems = [
        {
            src: "https://cdn.builder.io/api/v1/image/assets/TEMP/aae56868fdcb862e605ea9a58584175c78f8bec2f1376557a9d660d8863bf323?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8",
            alt: "Navigation item 1",
        },
        {
            src: "https://cdn.builder.io/api/v1/image/assets/TEMP/189297da07ec9868357cb4291401ef50667416493bf889bffb02c9cca138ebca?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8",
            alt: "Navigation item 2",
            onClick: toggleNavbar, // Pass the toggle function for the second item
        },
    ];

    return (
        <nav className="navigation">
            {navItems.map((item, index) => (
                <img
                    key={index}
                    src={item.src}
                    alt={item.alt}
                    className={`nav-item ${index === 1 ? "nav-item-last" : ""}`}
                    loading="lazy"
                    onClick={index === 1 ? toggleNavbar : undefined} // Only bind click for the second item
                />
            ))}
            <style jsx>{`
                .navigation {
                    display: flex;
                    gap: 5px; /* Reduce gap even more to make it more compact */
                    align-items: center;
                }
                .nav-item {
                    aspect-ratio: 1.02;
                    object-fit: contain;
                    object-position: center;
                    width: 40px; /* Significantly reduce width */
                }
                .nav-item-last {
                    aspect-ratio: 1;
                    width: 38px; /* Smaller width for the last item */
                    align-self: center;
                    margin: auto 0 auto auto;
                }
            `}</style>
        </nav>
    );
};

export default Navigation;
