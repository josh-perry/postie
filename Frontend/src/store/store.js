import Vue from "vue"
import Vuex from "vuex"
import createPersistedState from "vuex-persistedstate"
import * as Cookies from "js-cookie"
import axios from 'axios'

Vue.use(Vuex)

import { getInstance } from "@/auth";

export const store = new Vuex.Store({
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
    }
  },
  mutations: {
    setToken(state, token) {
      state.token = token;
    },
    setBoard(state, board) {
      state.board = board
    },
    setUser(state, user) {
      state.user = user
    }
  },
  actions: {
    retrieveTokenFromAuth0(context) {
      return new Promise(async (resolve) => {
        const instance = getInstance();

        instance.$watch("loading", async loading => {
          if (loading === false) {
            let authToken = await instance.getTokenSilently();

            if (context.state.token !== authToken) {
              context.commit("setToken", authToken);
            }

            resolve(authToken);
          }
        })
      });
    },
    async retrieveBoardDetails(context, boardUrl) {
      // Don't bother retrieving if we already have this board's information
      console.log(`Fetching board data for '${boardUrl}'`)
      if (boardUrl == context.state.board.url) {
        console.log(`Already got board data for '${boardUrl}', skipping request`)
        return
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
    }
  },
  plugins: [
    createPersistedState({
      getState: (key) => Cookies.getJSON(key),
      setState: (key, state) => Cookies.set(key, state, { expires: 2, secure: true })
    })
  ]
})
