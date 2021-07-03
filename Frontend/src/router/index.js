import Vue from "vue";
import Router from "vue-router";

import Home from "../views/Home.vue";
import Profile from "../views/Profile.vue";
import Board from "../views/Board.vue";
import Post from "../views/Post.vue";
import User from "../views/User.vue";
import CreateBoard from "../views/CreateBoard.vue";
import SubmitPost from "../views/SubmitPost.vue";

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
      path: "/user/:username",
      name: "user",
      component: User
    },
    {
      path: "/board/:boardName",
      name: "board",
      component: Board
    },
    {
      path: "/board/:boardName/submit",
      name: "submitposttoboard",
      component: SubmitPost
    },
    {
      path: "/board/:boardName/:postName",
      name: "post",
      component: Post
    },
    {
      path: "/create-board",
      name: "createboard",
      component: CreateBoard
    },
    {
      path: "/submit",
      name: "submitpost",
      component: SubmitPost
    } 
  ]
});

export default router;
