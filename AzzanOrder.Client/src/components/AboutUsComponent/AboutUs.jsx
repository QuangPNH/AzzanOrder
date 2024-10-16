import React from 'react';
import LineSeperator from './LineSeperator';

const AboutUs = ({ aboutUsData }) => {  // Accepting aboutUsData as a prop
    return (
        <section className="about-us">
            {aboutUsData.map((item, index) => (
                <div key={index} className="about-us__item">  {/* Individual item container */}
                    <h2 className="about-us__title">{item.title}</h2>
                    <LineSeperator width={120} />
                    <LineSeperator width={80} />
                    <p className="about-us__description">
                        {item.content.split('\n').map((text, idx) => (
                            <span key={idx}>{text}<br /></span>
                        ))}
                    </p>
                </div>
            ))}
            <style jsx>{`
                .about-us {
                    display: flex;
                    flex-direction: column;  /* Stacks the items vertically */
                    align-items: center;     /* Center align items */
                    font-family: Inter, sans-serif;
                }
                .about-us__item {
                    display: flex;
                    flex-direction: column;  /* Stacks the items vertically */
                    margin-bottom: 40px;    /* Adds space between each item */
                    width: 100%;            /* Full width for each item */
                    max-width: 600px;       /* Optional: limits width for readability */
                    text-align: center;      /* Center align the content */
                }
                .about-us__title {
                    color: #1e1e1e;
                    font-size: 24px;
                    font-weight: 600;
                    line-height: 1.4;
                    margin: 0;              /* No margin for the title */
                }
                .about-us__description {
                    color: #000;
                    font-size: 12px;
                    font-weight: 400;
                    margin-top: 10px;       /* Space above the description */
                    padding: 10px;          /* Added padding for the description */
                    text-align: center;      /* Center align the description */
                }
            `}</style>
        </section>
    );
};

export default AboutUs;
