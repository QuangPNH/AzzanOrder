import React from 'react';
import Banner from './Banner';
import Navbar from './Navbar';

const HomeItem = () => {
    return (
        <>
            <div className="home-item-container">
                <Banner />
                <Navbar />
            </div>

            <style jsx>{`
                .home-item-container {
                    display: flex;
                    flex-direction: column;
                    align-items: center;
                    width: 100%;
                }

                @media (max-width: 768px) {
                    .home-item-container {
                        padding: 0;
                    }
                }
            `}</style>
        </>
    );
};

export default HomeItem;
