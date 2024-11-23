export function getCookie(name) {
    const value = `; ${document.cookie}`; // Add a leading semicolon for easier parsing
    const parts = value.split(`; ${name}=`); // Split the cookie string to find the desired cookie
   if (parts.length === 2) {
        return decodeURIComponent(parts.pop().split(';').shift()); // Return the cookie value
    }
}
export function setCookie(name, value, days) {
    let expires = "";
    if (days) {
        const date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/; SameSite=None; Secure";
}
export function deleteCookie(name) {
    setCookie(name, '', -1);
}