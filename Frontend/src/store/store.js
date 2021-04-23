import Vue from "vue"
import Vuex from "vuex"
import createPersistedState from "vuex-persistedstate"
import * as Cookies from "js-cookie"

Vue.use(Vuex)

import { getInstance } from "@/auth";

function getToken(context, instance, resolve, reject) {
  instance
    .getTokenSilently()
    .then(authToken => {
      context.commit("setToken", authToken);
      resolve(authToken);
    })
    .catch(error => {
      reject(error);
    });
}

export const store = new Vuex.Store({
  state: {
    token: null
  },
  mutations: {
    setToken(state, token) {
      state.token = token;
    }
  },
  actions: {
    retrieveTokenFromAuth0(context) {
      return new Promise((resolve, reject) => {
        console.log("getting token")
        if (context.state.token !== null) {
          console.log("returning early")
          resolve(context.state.token)
          return
        }

        const instance = getInstance();

        if (instance.loading === false) {
          if (instance.isAuthenticated) {
            getToken(context, instance, resolve, reject)
          }
          else {
            resolve(null)
          }
        }

        instance.$watch("loading", loading => {
          if (loading === false && instance.isAuthenticated) {
            getToken(context, instance, resolve, reject);
          }
          else if (loading === false) {
            resolve(null);
          }
        });
      });
    }
  },
  plugins: [
    createPersistedState({
      getState: (key) => Cookies.getJSON(key),
      setState: (key, state) => Cookies.set(key, state, { expires: 2, secure: true })
    })
  ]
})
