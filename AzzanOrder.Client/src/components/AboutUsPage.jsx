import React from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import Banner from '../components/HomeItem/Banner';
import AboutUs from './AboutUsComponent/AboutUs';

const Homepage = () => {
    const aboutUsData = [
        {
            title: "About Us",
            content: `
                AZZAN was born to fulfill the mission of restoring the original coffee culture that has been forgotten and realizing the dream of Vietnamese Chocolate.
                
                We are doing the opposite of what is happening with Cacao and coffee in Vietnam. From choosing seedlings to taking care of the trees, harvesting, processing raw materials, roasting and packaging finished products.
                
                We want to be involved in every step of the process to be able to control and create the best products. Our dream is to produce products from pure, quality, safe coffee and Cacao that are recognized by the community and honored in every place and country we visit.
            `
        },
        {
            title: "Vision",
            content: "Is an organization that guides the culture of enjoying and providing high-quality agricultural products - recognized by the community."
        },
        {
            title: "Mission",
            content: "Bringing high-quality standards to Vietnamese agricultural products (coffee and cocoa)."
        },
        {
            title: "Core Value",
            content: `
                Thinking and acting based on the interests of the people.
                
                Creative research on the art of appreciation and the science of production.
                
                Living creatively and romantically in a scientific environment and ethical standards.
            `
        }
    ];

    return (
        <>
            <Header />
            <Banner />
            <div>
                <AboutUs aboutUsData={aboutUsData} />
            </div>
            <Footer />
        </>
    );
};

export default Homepage;
