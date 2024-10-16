import React from 'react';

const ProfileItem = ({ title }) => {
    return (
        <>
            <div className="profile-item">{title}</div>
            <style jsx>{`
        .profile-item {
          color: #000;
          white-space: nowrap;
          font: 500 15px Inter, sans-serif;
        }
      `}</style>
        </>
    );
};

export default ProfileItem;
