export const userService = {
    usernameLogin,
    emailLogin,
    signup,
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
        return Promise.resolve();
    }
    return Promise.reject(response.data);
}

async function emailLogin(email, password) {
    const data = {
        email,
        password
    };

    const response = await Axios.post("api/auth/email-login", data)
    if (response.status === 200) {
        return Promise.resolve();
    }
    return Promise.reject(response.data);
}

async function signup(email, username, password) {
    const data = {
        email,
        username,
        password
    };

    const response = await Axios.post("api/auth/signup", data);
    if (response.status === 200) {
        return Promise.resolve();
    }
    return Promise.reject(response.data);
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function logout() {
    const response = await Axios.get("api/auth/logout");
    return (response.status === 200);
}