import React from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import FeedbackTitle from './Feedback/FeedbackTitle';
import FeedbackComponent from './Feedback/FeedbackComponent';
import FeedbackForm from './Feedback/FeedbackForm';
import FeedbackNotice from './Feedback/FeedbackNotice';
import FeedbackButton from './Feedback/FeedbackButton';

const FeedbackScreen = () => {
    return (
        <>
            <Header />
            <div className="feedback-container">
                <FeedbackTitle />
                <FeedbackComponent />
                <div className="feedback-textbox-notice-container">
                    <FeedbackForm />
                    <FeedbackNotice />
                </div>
                <FeedbackButton text='Send' />
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
