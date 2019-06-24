<template>
    <div class="container-fluid py-5 login">
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-6 mx-auto">
                    <div class="card border border-warning rounded shadow-lg">
                        <div class="card-header border-top-0 border-warning">
                            <h3 class="mb-0">Login to TypeSmash!</h3>
                        </div>
                        <div class="card-body">
                            <form class="form form-login" role="form" @submit.prevent="handleSubmit">
                                <div class="form-group">
                                    <label for="uname1">Username/Email</label>
                                    <div class="input-group-prepend">
                                        <div v-if="prependString" class="input-group-text rounded-0">@</div>
                                        <input v-model="userdata" type="text" class="form-control form-control rounded-0" required>
                                    </div>
                                    <div v-show="submitted && !userdata" class="invalid-feedback">Oops, you missed this one.</div>
                                </div>
                                <div class="form-group">
                                    <label for="pwd1">Password</label>
                                    <input type="password" v-model="password" class="form-control form-control rounded-0" id="pwd1" required>
                                    <div v-show="submitted && !password" class="invalid-feedback">Enter your password too!</div>
                                </div>
                                <button class="btn btn-outline-primary btn-lg" :disabled="submitted || !loginAllowed">Login</button>
                                <br />
                                <img height="60px" width="60px" v-if="submitted" src="../assets/loading.svg" />
                                <br />
                                <div v-if="error" class="alert alert-danger alert-login"><span v-html="error"></span></div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/container-->
</template>

<script>
    import { userService } from "../services/userService";
    import { checkers } from "../services/checkers";
    import { router } from "../services/router";
    import LOGINTYPE from "../services/logintype";

    export default {
        name: 'Login',
        data() {
            return {
                userdata: '',
                password: '',
                submitted: false,
                loading: false,
                error: '',
                logintype: LOGINTYPE.USERNAME,
                loginAllowed: true,
            }
        },
        methods: {
            handleSubmit(e) {
                const { userdata, password } = this;
                // stop here if form is invalid
                if (!(userdata && password)) {
                    return;
                }

                if (this.logintype === LOGINTYPE.NONE) {
                    return;
                }

                this.submitted = true;

                if (this.logintype === LOGINTYPE.USERNAME) {
                    userService.usernameLogin(userdata, password)
                        .then((success) => {
                            if (success) {
                                router.push("game");
                            } else {
                                this.error = "Invalid Credentials!";
                                this.loading = false;
                                this.submitted = false;
                                this.clearCredentials();
                            }
                        }).catch(
                            error => {
                                this.error = error;
                                this.loading = false;
                                this.submitted = false;
                                this.clearCredentials();
                            }
                        );
                }

                if (this.logintype === LOGINTYPE.EMAIL) {
                    userService.emailLogin(userdata, password)
                        .then((success) => {
                            if (success) {
                                router.push("/game");
                            } else {
                                this.error = "Invalid Credentials!";
                                this.loading = false;
                                this.submitted = false;
                                this.clearCredentials();
                            }
                        }).catch(
                            error => {
                                this.error = error;
                                this.loading = false;
                                this.submitted = false;
                                this.clearCredentials();
                            }
                        );
                }
            },
            clearCredentials() {
                this.userdata = "";
                this.password = "";
            }
        },
        watch: {
            userdata: function () {
                this.logintype = LOGINTYPE.NONE;
                this.loginAllowed = false;
                this.error = "";

                if (this.userdata.length <= 5) {
                    this.error = "Username must contain atleast 6 characters!";
                    this.logintype = LOGINTYPE.USERNAME;
                    return;
                }

                /*
                Is Email Valid -> Yes -> return.
                                  No -> Is Username Valid? Yes -> return.
                                                           No  -> Show error (Username and Email not valid.)
                */
                if (checkers.isEmailValid(this.userdata)) {
                    this.logintype = LOGINTYPE.EMAIL;
                    this.loginAllowed = true;
                    return;
                }
                else {
                    if (checkers.isUsernameValid(this.userdata)) {
                        this.logintype = LOGINTYPE.USERNAME;
                        this.loginAllowed = true;
                        return;
                    }
                    else {
                        this.error = `Invalid Username format!
                                      A username can contain ->
                                      <ul>
                                        <li>Numbers 0-9</li>
                                        <li>Letters A-Z</li>
                                        <li>Hyphen( - )</li>
                                        <li>Dot( . )</li>
                                        <li>Underscore( _ )</li>
                                      </ul>`;
                        return;
                    }
                    this.error = "Invalid Email Format!";
                }
            }
        },
        computed: {
            prependString: function () {
                if (this.logintype === LOGINTYPE.USERNAME) {
                    return true;
                }
                return false;
            },
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

    .login {
        font-family: 'IBM Plex Sans', 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        margin: 2rem 0 0 0;
        font-size: 1.2rem;
        background-color: #f1dada;
background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='4' height='4' viewBox='0 0 4 4'%3E%3Cpath fill='%236200ff' fill-opacity='0.4' d='M1 3h1v1H1V3zm2-2h1v1H3V1z'%3E%3C/path%3E%3C/svg%3E");
        height: 100%;
    }

    .card {
        background-color: transparent;
        color: black;
    }

    .form-login {
        width: 70%;
        margin: auto;
    }

    .alert-login {
        margin: 10px 0px 0px 0px;
        background-color: white;
        font-size: 0.8rem;
    }
</style>
