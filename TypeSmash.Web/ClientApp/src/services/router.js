import Vue from 'vue';
import Router from "vue-router";
import Home from "../components/Home.vue";
import Login from "../components/Login.vue";
import Game from "../components/Game/Game.vue";
import Signup from "../components/Signup.vue";
import { checkers } from "../services/checkers";

Vue.use(Router);

export const router = new Router({
    mode: 'history',
    routes: [
        { path: '/', name:'home', component: Home },
        { path: '/login', name: 'login', component: Login },
        { path: '/game', name: 'game', component: Game},
        { path: '/signup', name: 'signup', component: Signup },
        { path: '*', redirect: '/' },
    ]
});

router.beforeEach((to, from, next) => {
// redirect to login page if not logged in and trying to access a restricted page
const publicPages = ['/login','/', '/signup'];
const authRequired = !publicPages.includes(to.path);
checkers.isStillLoggedIn().then((isLoggedIn) => {
        if (authRequired && !isLoggedIn) {
            return next({ 
            path: '/login',
            query: { returnUrl: to.path } 
            });
        }
        next();
    });
})