import Vue from "vue"
import Vuex from "vuex"

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
  }
})
