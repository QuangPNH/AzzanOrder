import React, { useState, useEffect } from "react";

const ProfilePicture = ({ src, onChange }) => {
    const [avatarSrc, setAvatarSrc] = useState(src);

    useEffect(() => {
        setAvatarSrc(src);
    }, [src]);

    const handleImageChange = (event) => {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = (e) => {
                const newAvatarSrc = e.target.result;
                setAvatarSrc(newAvatarSrc);
                onChange(newAvatarSrc);
            };
            reader.readAsDataURL(file);
        }
    };

    return (
        <>
            <header className="profile-picture">
                <div className="image-container" onClick={() => document.getElementById('fileInput').click()}>
                    <img
                        loading="lazy"
                        src={avatarSrc}
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
                <input
                    type="file"
                    id="fileInput"
                    style={{ display: 'none' }}
                    accept="image/*"
                    onChange={handleImageChange}
                />
            </header>
            <style jsx>{`
                .profile-picture {
                    border-radius: 100%;
                    background-color: rgba(0, 0, 0, 0.5);
                    display: flex;
                    overflow: hidden;
                    align-items: center;
                    aspect-ratio: 1;
                    border: 5px solid #fff;
                    margin: auto;
                    max-width: 400px;
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