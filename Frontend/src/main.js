import { createApp } from 'vue';
import App from './App.vue';
import router from './router';

import store from './store';

import '@/assets/css/main.css';

import VueScreen from 'vue-screen';

import setupAxios from './axios';

setupAxios();

const app = createApp(App);

app.use(store);
app.use(router);
app.use(VueScreen);

app.mount('#app');