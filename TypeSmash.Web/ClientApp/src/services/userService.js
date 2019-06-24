export const userService = {
    usernameLogin,
    emailLogin,
    logout
};

import Axios from "axios";

async function usernameLogin(username, password) {
    const data = {
        username,
        password
    }

    const response = await Axios.post("api/auth/username-login", data)
    if (response.status === 200) {
        return true;
    }
    return false;
}

async function emailLogin(email, password) {
    const data = {
        email,
        password
    }

    const response = await Axios.post("api/auth/email-login", data)
    if (response.status === 200) {
        return true;
    }
    return false;
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('loggedIn');
}