<template>
  <div class="navbar">
    <div class="left">
      <a class="badge" href="/">Postie</a>

      <Breadcrumb/>
    </div>

    <div class="right">
      <a v-if="!$auth.isAuthenticated && !$auth.loading" href="#" class @click.prevent="login">Log in</a>

      <div class="dropdown">
        <button v-if="$auth.isAuthenticated && !$auth.loading" class="dropdown-button">{{ username }}</button>
        <div class="dropdown-content">
          <a :href="profileLink">Profile</a>
          <a href="#" @click.prevent="logout">Log out</a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Breadcrumb from "../components/Breadcrumb";
import { store } from "../store/store"

export default {
  name: "NavBar",
  components: {
    Breadcrumb
  },
  computed: {
    profileLink() {
      if (store.state.user.username == null) {
        return null
      }

      return `/user/${store.state.user.username}`
    },
    username() {
      return store.state.user.username
    }
  },
  methods: {
    login() {
      this.$auth.loginWithRedirect();
    },
    logout() {
      store.dispatch("logout")

      this.$auth.logout();
      this.$router.push({ path: "/" });
    }
  }
};
</script>

<style>
.left {
  flex: 1;
}

.right {
  flex-grow: 0;
}

.navbar {
  display: flex;
}

.badge {
  float: left;
  font-size: 16px;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
}

.dropdown {
  float: left;
  overflow: hidden;
}

.dropdown .dropdown-button {
  font-size: 16px;
  border: none;
  outline: none;
  padding: 14px 16px;
  background-color: inherit;
  font-family: inherit;
  margin: 0;
}

.navbar .badge:hover, .dropdown:hover .dropdown-button {
  background-color: blue;
  color: white;
}

.dropdown-content {
  display: none;
  position: absolute;
  background-color: #f9f9f9;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
  z-index: 1;
}

.dropdown-content a {
  float: none;
  color: black;
  padding: 12px 16px;
  text-decoration: none;
  display: block;
  text-align: left;
}

.dropdown-content a:hover {
  background-color: #ddd;
}

.dropdown:hover .dropdown-content {
  display: block;
}
</style>
