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
  ],
  scrollBehavior(to) {
    if (to.hash) {
      const scrollOptions = {
        behavior: "smooth"
      }

      const element = document.getElementById(to.hash.replace(/#/, ''))

      // If it's already on the page then scroll to it
      if (element) {
          element.scrollIntoView(scrollOptions)
      }
      else {
        // Otherwise check every 500ms. When we find it, scroll to it and stop the timer
        let scrollInterval = setInterval(() => {
          const element = document.getElementById(to.hash.replace(/#/, ''))
          if (element) {
            element.scrollIntoView(scrollOptions)
            clearInterval(scrollInterval)
          }
        }, 500)
      }

      return {
        el: to.hash
      }
    }
  }
});

export default router;
