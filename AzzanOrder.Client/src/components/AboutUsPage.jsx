import React, { useState, useEffect, useRef } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import Banner from '../components/HomeItem/Banner';
import AboutUs from './AboutUsComponent/AboutUs';



const Homepage = () => {
    const [about, setAbout] = useState();
    useEffect(() => {
        fetchAbout();
    }, []);
    const fetchAbout = async () => {
        try {
            const response = await fetch(`https://localhost:7183/api/Abouts`);
            const data = await response.json();
            setAbout(data);
        } catch (error) {
            console.error('Error fetching menu items:', error);
        }
    };
    return (
        <>
            <Header />
            {about && (
                <div>
                    {about.map((a) => (
                        <AboutUs key={a.id} title={a.title} content={a.content} />
                    ))}
                </div>
            )};
            <Footer />
        </>
    );
};

export default Homepage;
