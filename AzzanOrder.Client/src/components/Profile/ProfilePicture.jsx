import React from "react";

const ProfilePicture = ({ src }) => {
    return (
        <>
            <header className="profile-picture">
                <div className="image-container">
                    <img
                        loading="lazy"
                        src={src} // Use the src prop for the avatar
                        alt="Avatar"
                        className="background-image"
                    />
                    <img
                        loading="lazy"
                        src="https://cdn.builder.io/api/v1/image/assets/TEMP/457b9ecac6099422780b54cdecae2713eaa12c0b26069d1314f5d2ed648556e5?placeholderIfAbsent=true&apiKey=a971ff9380c749fd99c76f2c51698533"
                        alt="Add"
                        className="foreground-icon"
                    />
                </div>
            </header>
            <style jsx>{`
                .profile-picture {
                    border-radius: 270px;
                    background-color: rgba(0, 0, 0, 0.5);
                    display: flex;
                    flex-direction: column;
                    overflow: hidden;
                    align-items: center;
                    aspect-ratio: 1;
                    border: 5px solid #fff;
                }
                .image-container {
                    display: flex;
                    flex-direction: column;
                    box-shadow: 3px 6px 10px rgba(0, 0, 0, 0.25);
                    border-radius: 50%;
                    position: relative;
                    aspect-ratio: 1;
                    width: 100%;
                    align-items: center;
                    justify-content: center;
                    padding: 97px 55px;
                }
                .background-image {
                    position: absolute;
                    inset: 0;
                    height: 100%;
                    width: 100%;
                    object-fit: cover;
                    object-position: center;
                }
                .foreground-icon {
                    aspect-ratio: 1;
                    object-fit: contain;
                    object-position: center;
                    width: 80px;
                }
            `}</style>
        </>
    );
};

export default ProfilePicture;
