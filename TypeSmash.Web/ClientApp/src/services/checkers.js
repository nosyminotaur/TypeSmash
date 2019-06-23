//this file contains methods that can verify whether a parameter is an email/username etc.
export const checkers = {
    isEmailValid,
    isUsernameValid,
    isStillLoggedIn,
}

function isEmailValid(email) {
    return (email.length > 0 && isEmailValidRegex(email));
}

function isUsernameValid(username) {
    return (username.length > 5 && isUsernameValidRegex(username));
}

function isEmailValidRegex(email) {
    let re = /^(([^<>()\\[\]\\.,;:\s@"]+(\.[^<>()\\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

//this function uses the same regex as the ASP.NET Core api.
//this ensures smooth operation, even though there is a checker
//in the api too.
function isUsernameValidRegex(username) {
    let re = /^[-_.A-Za-z0-9]+$/;
    return re.test(String(username).toLowerCase());
}

async function isStillLoggedIn() {
    const response = await fetch("api/auth/");
    return (response.status === 200);
}