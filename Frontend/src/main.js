import Vue from "vue";
import App from "./App.vue";
import router from "./router";

import { Auth0Plugin } from "./auth";
import VueScreen from "vue-screen"

import { domain, clientId, audience } from "../auth_config.json";

import { store } from "./store/store"

import '@/assets/css/main.css'

Vue.config.productionTip = false;

Vue.use(Auth0Plugin, {
  domain,
  clientId,
  audience,
  onRedirectCallback: appState => {
    router.push(
      appState && appState.targetUrl
        ? appState.targetUrl
        : window.location.pathname
    );
  }
});

Vue.use(VueScreen);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
