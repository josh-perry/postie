<template>
    <div class="login">
        <h1>Login</h1>
        <form @submit.prevent="login">
        <div class="field">
            <label for="username">Username</label>
            <input type="text" id="username" v-model="username" />
        </div>
        <div class="field">
            <label for="password">Password</label>
            <input type="password" id="password" v-model="password" />
        </div>
        <button type="submit">Login</button>
        </form>
    </div>
</template>

<script>
import store from "../store";

export default {
    name: "Login",
    data() {
        return {
            username: "",
            password: ""
        }
    },
    methods: {
        async login() {
            await store.dispatch("user/login", {
                email: this.username,
                password: this.password
            });

            this.$router.push({ path: "/" });
        }
    }
}
</script>