import React, { useState, useEffect } from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import DetailInfo from '../components/Profile/DetailInfo';
import DetailCheckbox from '../components/Profile/DetailCheckbox';
import DetailDateTime from '../components/Profile/DetailDateTime';
import NameLabel from '../components/Profile/NameLabel';
import ProfilePicture from '../components/Profile/ProfilePicture';
import ProfileButton from './Profile/ProfileButton';
import { getCookie } from './Account/SignUpForm/Validate';
import API_URLS from '../config/apiUrls';

const MemberProfile = () => {
    const [memberInfo, setMemberInfo] = useState({
        memberName: '',
        phone: '',
        gmail: '',
        birthDate: '',
        gender: false,
        address: '',
        image: ''
    });
    const [initialMemberInfo, setInitialMemberInfo] = useState(null);

    useEffect(() => {
        const memberInfoCookie = getCookie('memberInfo');
        if (memberInfoCookie != null) {

            setFormattedMemberInfo(JSON.parse(memberInfoCookie).phone);
        } else {
            window.location.href = '/';
        }
    }, []);

    const setFormattedMemberInfo = async (phone) => {
        try {
            const response = await fetch(API_URLS.API + `Member/Phone/${phone}`);
            if (response.ok) {
                const data = await response.json();
                const formattedMemberInfo = {
                    ...data,
                    birthDate: formatDate(data.birthDate)
                };
                setMemberInfo(formattedMemberInfo);
                setInitialMemberInfo(formattedMemberInfo); // Store the initial state
            } else {
                console.error('Failed to fetch member info');
            }
        } catch (error) {
            console.error('Error fetching member info:', error);
        }
    };

    const formatDate = (dateString) => {
        const date = new Date(dateString);
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        return `${year}-${month}-${day}`;
    };

    const handleSave = async () => {
        try {
            const response = await fetch(API_URLS.API + 'Member/Update', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(memberInfo)
            });
            if (response.ok) {
                console.log('Member info updated successfully');
                setInitialMemberInfo(memberInfo); // Update the initial state after saving
            } else {
                console.error('Failed to update member info');
            }
        } catch (error) {
            console.error('Error updating member info:', error);
        }
    };

    const handleCancel = () => {
        setMemberInfo(initialMemberInfo); // Reset to the initial state
    };

    const handleChange = (field, value) => {
        setMemberInfo((prevInfo) => ({
            ...prevInfo,
            [field]: value
        }));
    };

    if (!memberInfo) {
        return <div>Loading...</div>;
    }

    return (
        <>
            <Header />
            <div>
                <ProfilePicture src={memberInfo?.image} onChange={(value) => handleChange('image', value)} />
                <div className="name-container">
                    <NameLabel name={memberInfo?.memberName} onChange={(value) => handleChange('memberName', value)} />
                </div>

                <div className="detail-container">
                    <DetailInfo title="Mobile Phone" memberDetail={memberInfo?.phone} onChange={(value) => handleChange('phone', value)} />
                    <DetailInfo title="Mail" memberDetail={memberInfo?.gmail} onChange={(value) => handleChange('gmail', value)} />
                    <DetailDateTime title="Birthday" memberDetail={memberInfo?.birthDate} onChange={(value) => handleChange('birthDate', value)} />
                    <DetailCheckbox title="Gender" memberDetail={memberInfo?.gender} onChange={(value) => handleChange('gender', value)} />
                    <DetailInfo title="Address" memberDetail={memberInfo?.address} onChange={(value) => handleChange('address', value)} />
                </div>

                <div className="button-container">
                    <ProfileButton text='Cancel' onClick={handleCancel} />
                    <ProfileButton text='Save' onClick={handleSave} />
                </div>
            </div>
            <Footer />
        </>
    );
};

export default MemberProfile;