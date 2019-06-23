<template>
  <div class="container-fluid py-5 login">
    <div class="row">
        <div class="col-md-12">
                <div class="col-md-6 mx-auto">
                    <div class="card border border-warning rounded shadow-lg">
                        <div class="card-header border-top-0 border-warning">
                            <h3 class="mb-0">Signup for TypeSmash!</h3>
                        </div>
                        <div class="card-body" >
                            <form class="form form-login" role="form" @submit.prevent="handleSubmit">
                                <div class="form-group">
                                    <label for="username">Username</label>
                                    <div class="input-group-prepend">
                                      <div class="input-group-text rounded-0">@</div>
                                      <input v-model="username" type="text" class="form-control form-control rounded-0" required>
                                    </div>
                                    <div v-show="submitted && !userdata" class="invalid-feedback">Oops, you missed this one.</div>
                                </div>
                                <div class="form-group">
                                    <label for="username">Email</label>
                                    <input v-model="email" type="text" class="form-control form-control rounded-0" required>
                                    <div v-show="submitted && !userdata" class="invalid-feedback">Oops, you missed this one.</div>
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
                                <div v-if="error" class="alert alert-danger alert-login">{{error}}</div>
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
  data () {
    return {
      username: '',
      email: '',
      password: '',
      submitted: false,
      loading: false,
      error: '',
    }
  },
  methods: {
    handleSubmit (e) {
      this.submitted = true;
      const {username,email, password} = this;

      // stop here if form is invalid
      if (!(userdata && password)) return;

      if (!this.isEmail || !this.isUsername) return;

      if (this.isUsername) {
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
      if (this.isEmail) {
        userService.emailLogin(userdata, password)
                .then((success) => {
                  if (success) {
                    console.log("Routing to game...");
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
      this.username = "";
      this.email = "";
      this.password = "";
    }
  },
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .login {
    font-family: 'IBM Plex Sans', 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    margin: 2rem 0 0 0;
    font-size: 1.2rem;
    background-image: url(../assets/signupBG.svg);
    height: 100%
  }
  .card {
    z-index: 1;
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
  }
</style>
