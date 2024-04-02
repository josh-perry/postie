import axios from "axios";

export default {
    namespaced: true,
    state: {
        bearerToken: "",
        refreshToken: "",
        username: "",
        tokenExpirationDateTime: null
    },
    getters: {
        bearerToken: state => state.bearerToken,
        tokenExpirationDateTime: state => state.tokenExpirationDateTime,
        email: state => state.email,
        refreshToken: state => state.refreshToken,
        isAuthenticated: state => state.bearerToken !== ''
    },
    actions: {
        async login(context, credentials) {
            const result = await axios.post(`https://localhost:5001/account/login`, {
                email: credentials.email,
                password: credentials.password
            });

            context.commit("SET_BEARER_TOKEN", result.data.accessToken);
            context.commit("SET_REFRESH_TOKEN", result.data.refreshToken);
            context.commit("SET_USERNAME", credentials.email);

            const now = new Date();
            const expirationTime = now.setSeconds(now.getSeconds() + result.data.expiresIn);
            context.commit("SET_TOKEN_EXPIRATION_DATETIME", expirationTime);
        },
        async logout(context) {
            context.commit("SET_BEARER_TOKEN", null)
            context.commit("SET_USERNAME", null)
            context.commit("SET_REFRESH_TOKEN", null)
            context.commit("SET_TOKEN_EXPIRATION_DATETIME", null);
        },
        async retrieveUserDetails(context) {
            if (context.state.token === null) {
                if (Object.keys(context.state.user).length === 0 && context.state.user.constructor === Object) {
                  console.log("User is empty already")
                  return
                }
        
                console.log("Emptying user state")
                context.commit("SET_BEARER_TOKEN", null)
                context.commit("SET_USERNAME", null)
                context.commit("SET_REFRESH_TOKEN", null)
                context.commit("SET_TOKEN_EXPIRATION_DATETIME", null);
                return
              }
        
              console.log("getting user")
        
              const { data } = await axios.get(`https://localhost:5001/user`)
        
              console.log("user", data)
              context.commit("SET_USERNAME", data)
        }
    },
    mutations: {
        SET_BEARER_TOKEN(state, token) {
            state.bearerToken = token;
        },
        SET_USERNAME(state, username) {
            state.username = username;
        },
        SET_REFRESH_TOKEN(state, token) {
            state.refreshToken = token;
        },
        SET_EXPIRATION_DATETIME(state, expirationDateTime) {
            state.tokenExpirationDateTime = expirationDateTime;
        }
    }
}