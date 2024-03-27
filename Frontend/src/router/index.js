import { createRouter, createWebHistory } from 'vue-router';

import Home from "../views/Home.vue";
import Profile from "../views/Profile.vue";
import Board from "../views/Board.vue";
import Boards from "../views/Boards.vue";
import Post from "../views/Post.vue";
import User from "../views/User.vue";
import CreateBoard from "../views/CreateBoard.vue";
import SubmitPost from "../views/SubmitPost.vue";
import Login from "../components/Login.vue";

const routes = [
  {
    path: "/",
    name: "home",
    component: Home
  },
  {
    path: "/profile",
    name: "profile",
    component: Profile,
    //beforeEnter: authGuard
  },
  {
    path: "/user/:username",
    name: "user",
    component: User
  },
  {
    path: "/board",
    name: "boards",
    component: Boards
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
  },
  {
    path: "/login",
    name: "login",
    component: Login
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
  async scrollBehavior(to, from, savedPosition) {
    if (to.hash) {
      await new Promise(resolve => {
        const checkElementExistence = setInterval(() => {
          const element = document.getElementById(to.hash.replace(/#/, ''));
          if (element) {
            clearInterval(checkElementExistence);
            resolve();
          }
        }, 500);
      });

      return {
        el: to.hash,
        behavior: 'smooth'
      };
    } else if (savedPosition) {
      return savedPosition;
    } else {
      return { top: 0 };
    }
  }
});

export default router;