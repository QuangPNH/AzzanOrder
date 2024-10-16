<div className="product-sale-container">
    <ProductSale
        saleAmount={10}
        endDate="2024-12-01"
        price={0}
        infiniteUses={true}
        useCount={0}
    />

    <ProductSale
        saleAmount={10}
        endDate="2024-12-01"
        price={0}
        infiniteUses={false}
        useCount={5}
    />
</div>

<style jsx>{`
    .product-sale-container {
        display: flex; /* Flex display to align ProductSale components */
        justify-content: center; /* Center the components horizontally */
        align-items: stretch; /* Stretch to equal height */
        margin-top: 20px; /* Space above */
        width: 100%; /* Ensure it takes full width */
    }
`}></style>
