import Vue from "vue";
import Router from "vue-router";
import Home from "../views/Home.vue";
import Profile from "../views/Profile.vue";
import Posts from "../views/Posts.vue";
import Board from "../views/Board.vue";
import { authGuard } from "../auth";

Vue.use(Router);

const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/profile",
      name: "profile",
      component: Profile,
      beforeEnter: authGuard
    },
    {
      path: "/posts",
      name: "posts",
      component: Posts,
    },
    {
      path: "/board/:boardName",
      name: "board",
      component: Board
    }
  ]
});

export default router;
