import React, { useState } from 'react';
import DetailInfo from '../components/Profile/DetailInfo';
import NameLabel from '../components/Profile/NameLabel';
import ProfileItem from './Profile/ProfileItem';
import ProfilePicture from '../components/Profile/ProfilePicture';
import ProfileButton from './Profile/ProfileButton';

const MemberProfile = () => {
    const [name, setName] = useState('Nguyễn Quang Minh');
    const [mobilePhone, setMobilePhone] = useState('0987654321');
    const [email, setEmail] = useState('minhsay23ra@gmail.com');
    const [birthday, setBirthday] = useState('07-09-2002');
    const [gender, setGender] = useState('M');
    const [address, setAddress] = useState('123 Hoang Quoc Viet');

    // Avatar image URL
    const avatarUrl = "https://cdn.builder.io/api/v1/image/assets/TEMP/af073c35b1871015eb287de06ef4feda70cb6aec68d5d06202816f3a08d59e32?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533";

    return (
        <>
            <div>
                <ProfilePicture src={avatarUrl} /> {/* Pass the avatar URL as a prop */}
                <div className="name-container">
                    <ProfileItem title='Full Name' />
                    <NameLabel name={name} setName={setName} />
                </div>

                <div className="detail-container">
                    <DetailInfo title="Mobile Phone" memberDetail={mobilePhone} setMemberDetail={setMobilePhone} />
                    <DetailInfo title="Mail" memberDetail={email} setMemberDetail={setEmail} />
                    <DetailInfo title="Birthday" memberDetail={birthday} setMemberDetail={setBirthday} />
                    <DetailInfo title="Gender" memberDetail={gender} setMemberDetail={setGender} />
                    <DetailInfo title="Address" memberDetail={address} setMemberDetail={setAddress} />
                </div>

                <div className="button-container">
                    <ProfileButton text='Cancel' />
                    <ProfileButton text='Save' />
                </div>
            </div>
        </>
    );
};

export default MemberProfile;
