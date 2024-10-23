import React, { useState, useEffect } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import FeedbackTitle from './Feedback/FeedbackTitle';
import FeedbackComponent from './Feedback/FeedbackComponent';
import FeedbackForm from './Feedback/FeedbackForm';
import FeedbackNotice from './Feedback/FeedbackNotice';
import FeedbackButton from './Feedback/FeedbackButton';
import { getCookie } from './Account/SignUpForm/Validate';

const FeedbackScreen = () => {
    const [feedback, setContent] = useState(null);
    useEffect(() => {
        const memberInfoCookie = getCookie('memberInfo');
        if (memberInfoCookie != null) {
            fetchContentFromAPI(JSON.parse(memberInfoCookie).memberId)
        }
    }, []);

    const fetchContentFromAPI = async (id) => {
        try {
            const response = await fetch(`https://localhost:7183/api/Feedback/ByMemberId/${id}`);
            if (response.ok) {
                const data = await response.json();
                setContent(data);
            }
        } catch (error) {
            throw new Error('Error fetching content from API:', error);
        }
    };

    const handleSave = async () => {
        try {
            const response = await fetch('https://localhost:7183/api/Feedback/Update', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(feedback)
            });
            if (response.ok) {
                console.log('Member info updated successfully');
            } else {
                console.error('Failed to update member info');
            }
        } catch (error) {
            console.error('Error updating member info:', error);
        }
    };

    const handleChange = (field, value) => {
        setContent((prevInfo) => ({
            ...prevInfo,
            [field]: value
        }));
    };

    return (
        <>
            <Header />
            <div className="feedback-container">
                <FeedbackTitle />
                <FeedbackComponent />
                <div className="feedback-textbox-notice-container">
                    <FeedbackForm data={feedback?.content} onChange={(value) => handleChange('content', value)} />
                    <FeedbackNotice />
                </div>
                <FeedbackButton text='Send' onClick={handleSave}/>
            </div>
            <Footer />
            <style jsx>{`
                .feedback-container {
                    display: flex;
                    flex-direction: column; /* Stack children vertically */
                    align-items: center; /* Center children horizontally */
                }
                .feedback-textbox-notice-container {
                    display: flex;
                    flex-direction: column; /* Align vertically */
                    align-items: center; /* Center children horizontally */
                    margin-top: 15px; /* Add some spacing */
                }
                .feedback-textbox-notice-container > * {
                    margin: 5px 0; /* Add spacing between textbox and notice */
                }
            `}</style>
        </>
    );
};

export default FeedbackScreen;
