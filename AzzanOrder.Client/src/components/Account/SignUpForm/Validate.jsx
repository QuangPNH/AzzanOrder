export function getCookie(name) {
    const value = `; ${document.cookie}`; // Add a leading semicolon for easier parsing
    const parts = value.split(`; ${name}=`); // Split the cookie string to find the desired cookie
    if (parts.length === 2) return decodeURIComponent(parts.pop().split(';').shift()); // Return the cookie value
}
export function setCookie(name, value, days) {
    const expires = new Date(Date.now() + days * 864e5).toUTCString(); // Calculate expiration date
    document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`; // Set cookie
}