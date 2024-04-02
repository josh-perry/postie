import { createStore } from 'vuex'
import VuexPersistence from 'vuex-persist'

import user from './modules/user';
import board from './modules/board';

const vuexLocal = new VuexPersistence({
    storage: window.localStorage
});

export default createStore({
    modules: {
        board,
        user
    },
    plugins: [vuexLocal.plugin]
});