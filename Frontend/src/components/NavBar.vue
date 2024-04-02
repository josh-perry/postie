<template>
  <div class="navbar">
    <div class="left">
      <a class="badge" href="/">Postie</a>

      <Breadcrumb v-if="!mobile" />
    </div>

    <div class="right" v-if="!mobile">
      <a v-if="!isAuthenticated" href="#" class="badge" @click.prevent="login">Log in</a> 

      <div class="dropdown">
        <button v-if="isAuthenticated" class="dropdown-button">{{ username }}</button>
        <div class="dropdown-content">
          <a :href="profileLink">Profile</a>
          <a href="#" @click.prevent="logout">Log out</a>
        </div>
      </div>
    </div>

    <div class="right" v-if="mobile">
      <div class="dropdown">
        <button @click.prevent="toggleMenu" class="dropdown-button">
          <span class="burger-part" />
          <span class="burger-part" />
          <span class="burger-part" />
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import Breadcrumb from "../components/Breadcrumb";

export default {
  name: "NavBar",
  components: {
    Breadcrumb
  },
  created() {

  },
  computed: {
    profileLink() {
      if (this.$store.state.user.username == null) {
        return null
      }

      return `/user/${this.$store.state.user.username}`
    },
    username() {
      return this.$store.state.user.username
    },
    mobile() {
      return this.$screen.width < 800
    },
    isAuthenticated() {
      return this.$store.getters["user/isAuthenticated"];
    }
  },
  methods: {
    login() {
      this.$router.push({ path: "/login" })
    },
    logout() {
      this.$store.dispatch("logout")

      this.$auth.logout();
      this.$router.push({ path: "/" });
    },
    toggleMenu() {
      this.$store.dispatch("toggleMobileMenu");
    }
  },
  mounted() {
    console.log(this.$store);
  },
};
</script>

<style scoped>
.left {
  flex: 1;
}

.right {
  flex-grow: 0;
}

.navbar {
  display: flex;
  background-color: #0A0908;
  color: #EAE0D5;
}

.badge {
  float: left;
  font-size: 16px;
  text-align: center;
  padding: 16px;
  text-decoration: none;
}

.dropdown {
  height: 100%;
  float: left;
  overflow: hidden;
}

.dropdown .dropdown-button {
  height: 100%;
  color: #EAE0D5;
  font-size: 16px;
  border: none;
  outline: none;
  background-color: inherit;
  font-family: inherit;
  margin: 0;
}

.navbar .badge:hover, .dropdown:hover .dropdown-button {
  background-color: #F06543;
  color: white;
}

.dropdown-content {
  display: none;
  position: absolute;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
  z-index: 1;
}

.dropdown-content a {
  float: none;
  padding: 16px 16px;
  text-decoration: none;
  display: block;
  text-align: left;
}

.dropdown-content a:hover {
  background-color: #F06543;
  color: white;
}

.dropdown:hover .dropdown-content {
  display: block;
}

.burger-part {
  display: block;
  width: 33px;
  height: 4px;
  margin-bottom: 5px;
  position: relative;
  background: #cdcdcd;
  border-radius: 3px;
  
  z-index: 1;
}
</style>
