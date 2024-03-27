import axios from "axios";

export default {
    namespaced: true,
    state: {
        bearerToken: "",
        email: "",
        refreshToken: "",
        username: "",
    },
    getters: {
        bearerToken: state => state.bearerToken,
        email: state => state.email,
        refreshToken: state => state.refreshToken,
        username: state => state.username,
    },
    actions: {
        async login(context, credentials) {
            const { data } = await axios.post(`https://localhost:5001/account/login?useCookies=true`, {
                email: credentials.email,
                password: credentials.password
            });

            context.commit("SET_BEARER_TOKEN", data.bearerToken);
            context.commit("SET_EMAIL", data.email);
            context.commit("SET_REFRESH_TOKEN", data.refreshToken);
            context.commit("SET_USERNAME", data.username);
        }
    },
    mutations: {
        SET_BEARER_TOKEN(state, token) {
            state.bearerToken = token;
        },
        SET_EMAIL(state, email) {
            state.email = email;
        },
        SET_REFRESH_TOKEN(state, token) {
            state.refreshToken = token;
        },
        SET_USERNAME(state, username) {
            state.username = username;
        },
    }
}