import React, { useEffect } from 'react';
import LineSeperator from './LineSeperator';

const AboutUs = ({ title, content  }) => {  // Accepting aboutUsData as a prop
    useEffect(()=> {
        console.log(title);
        console.log(content);
    });
    return (
        <>
        <div className="about-us__item">  {/* Individual item container */}
                    <h2 className="about-us__title">{title}</h2>
                    <LineSeperator width={30} />
                    <p className="about-us__description">
                        {content.split('\n').map((text, idx) => (
                            <span key={idx}>{text}<br /></span>
                        ))}
                    </p>
                </div>
            
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
                    max-width: 100%;       /* Optional: limits width for readability */
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
        </>
    );
};

export default AboutUs;
