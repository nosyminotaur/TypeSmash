export const userService = {
    usernameLogin,
    emailLogin,
    logout
};

async function usernameLogin(username, password) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    };
    await sleep(20000);
    const response = await fetch("api/auth/username-login", requestOptions);
    if (response.status === 200) {
        return true;
    }
    return false;
}

async function emailLogin(email, password) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password })
    };
    const response = await fetch("/api/auth/username-login", requestOptions);
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