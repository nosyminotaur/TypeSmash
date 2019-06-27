<template>
    <div class="container-fluid py-5 login">
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-6 mx-auto">
                    <div class="card border border-warning rounded shadow-lg">
                        <div class="card-header border-top-0 border-warning">
                            <h3 class="mb-0">Signup for TypeSmash!</h3>
                        </div>
                        <div class="card-body">
                            <form class="form form-login" role="form" @submit.prevent="handleSubmit">
                                <div class="form-group">
                                    <label for="username">Username</label>
                                    <div class="input-group-prepend">
                                        <div class="input-group-text rounded-0">@</div>
                                        <input v-model="username" type="text" class="form-control form-control rounded-0" required>
                                    </div>
                                    <div v-show="submitted && !username" class="invalid-feedback">Oops, you missed this one.</div>
                                </div>
                                <div class="form-group">
                                    <label for="email">Email</label>
                                    <input v-model="email" type="email" class="form-control form-control rounded-0" required>
                                    <div v-show="submitted && !email" class="invalid-feedback">Oops, you missed this one.</div>
                                </div>
                                <div class="form-group">
                                    <label for="pwd1">Password</label>
                                    <input type="password" v-model="password" class="form-control form-control rounded-0" id="pwd1" required>
                                    <div v-show="submitted && !password" class="invalid-feedback">Enter your password too!</div>
                                </div>
                                <button class="btn btn-outline-primary btn-lg" :disabled="submitted">Signup</button>
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

    export default {
        name: 'Signup',
        data() {
            return {
                username: '',
                email: '',
                password: '',
                submitted: false,
                error: '',
            }
        },
        methods: {
            handleSubmit() {
                this.submitted = true;
                const { username, email, password } = this;

                if (!checkers.isEmailValid(this.email)) {
                    this.error = "Invalid Email format!";
                    return;
                }

                if (!checkers.isUsernameValid(this.username)) {
                    this.error = `  Invalid Username format!
                                    Username must be atleast 6 characters long.
                                    A username can contain ->
                                    <ul>
                                    <li>Numbers 0-9</li>
                                    <li>Letters A-Z</li>
                                    <li>Hyphen( - )</li>
                                    <li>Dot( . )</li>
                                    <li>Underscore( _ )</li>
                                    </ul>`;
                }

                userService.signup(email, username, password)
                    .then(() => {
                        router.push("/login");
                    }).catch((error) => {
                        this.error = this.getReadbleErrors(error.errors);
                        this.submitted = false;
                        this.clearCredentials();
                    });
            },
            clearCredentials() {
                this.username = "";
                this.email = "";
                this.password = "";
            },
            getReadbleErrors(errors) {
                //Run only if type of errors is Array
                if (Array.isArray(errors)) {
                    let finalError = `<ul>`;
                    let len = errors.length;
                    for (var i = 0; i < len; i++) {
                        finalError = finalError + `<li>` + errors[i] + `</li>`;
                    }
                    finalError = finalError + "</ul>"
                    return finalError;
                }
                return errors;
            },
        },
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    .login {
        font-family: 'IBM Plex Sans', 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        margin: 2rem 0 0 0;
        font-size: 1.2rem;
        background-color: #DFDBE5;
        background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='28' height='49' viewBox='0 0 28 49'%3E%3Cg fill-rule='evenodd'%3E%3Cg id='hexagons' fill='%236200ff' fill-opacity='0.4' fill-rule='nonzero'%3E%3Cpath d='M13.99 9.25l13 7.5v15l-13 7.5L1 31.75v-15l12.99-7.5zM3 17.9v12.7l10.99 6.34 11-6.35V17.9l-11-6.34L3 17.9zM0 15l12.98-7.5V0h-2v6.35L0 12.69v2.3zm0 18.5L12.98 41v8h-2v-6.85L0 35.81v-2.3zM15 0v7.5L27.99 15H28v-2.31h-.01L17 6.35V0h-2zm0 49v-8l12.99-7.5H28v2.31h-.01L17 42.15V49h-2z'/%3E%3C/g%3E%3C/g%3E%3C/svg%3E");
        height: 100%
    }

    .card {
        z-index: 1;
        background-color: rgb(243, 147, 112);
        color: black;
    }

    .form-login {
        width: 70%;
        margin: auto;
    }

    .alert-login {
        margin: 10px 0px 0px 0px;
        background-color: white;
    }
</style>
