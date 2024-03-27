import { createStore } from 'vuex';
import axios from 'axios';
import VuexPersistence from 'vuex-persist';

const vuexLocal = new VuexPersistence({
  storage: window.localStorage
});

export const store = createStore({
  state: {
    token: null,
    board: {
      title: "Unknown board",
      createdBy: "Unknown",
      createdDateTime: null,
      url: "",
      description: "No description"
    },
    user: {
      username: ""
    },
    mobileMenu: false
  },
  mutations: {
    setToken(state, token) {
      state.token = token;
    },
    setBoard(state, board) {
      state.board = board;
    },
    setUser(state, user) {
      state.user = user;
    },
    setMobileMenu(state, menuOpen) {
      state.mobileMenu = menuOpen;
    }
  },
  actions: {
    async retrieveBoardDetails(context, boardUrl) {
      // Don't bother retrieving if we already have this board's information
      console.log(`Fetching board data for '${boardUrl}'`);
      if (boardUrl == context.state.board.url) {
        console.log(`Already got board data for '${boardUrl}', skipping request`);
        return;
      }
      let headers = {}

      if (context.state.token !== null) {
        headers["Authorization"] = `Bearer ${context.state.token}`
      }

      const { data } = await axios.get(`https://localhost:5001/board/${boardUrl}`, {
        headers: headers
      });

      context.commit("setBoard", data)
    },
    async login(context, credentials) {
      const { data } = await axios.post(`https://localhost:5001/account/login?useCookies=true`, {
        email: credentials.email,
        password: credentials.password
      });

      //context.commit("");

      //fetch("https://localhost:5001/account/login?useCookies=true&useSessionCookies=true", {
      //    credentials: "include",
      //    method: "POST",
      //    headers: {
      //        "Content-Type": "application/json"
      //    },
      //    body: JSON.stringify({
      //        email: this.username,
      //        password: this.password
      //    })
      //})

      console.log(data);
      context.commit("setUser", data.user);
    },
    async logout(context) {
      context.commit("setUser", {})
      context.commit("setToken", null)
    },
    async retrieveUserDetails(context) {
      if (context.state.token === null) {
        if (Object.keys(context.state.user).length === 0 && context.state.user.constructor === Object) {
          console.log("User is empty already")
          return
        }

        console.log("Emptying user state")
        context.commit("setUser", {})
        return
      }

      console.log("getting user")

      const { data } = await axios.get(`https://localhost:5001/user`, {
        headers: {
          Authorization: `Bearer ${context.state.token}`
        }
      });

      console.log("user", data)

      context.commit("setUser", data)
    },
    toggleMobileMenu(context) {
      context.commit("setMobileMenu", !context.state.mobileMenu)
    }
  },
  plugins: [vuexLocal.plugin]
});