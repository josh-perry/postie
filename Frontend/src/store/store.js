import Vue from "vue"
import Vuex from "vuex"

Vue.use(Vuex)

import { getInstance } from "@/auth";

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
        const instance = getInstance();
        instance.$watch("loading", loading => {
          if (loading === false && instance.isAuthenticated) {
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
          else if (loading === false) {
            resolve(null);
          }
        });
      });
    }
  }
})
