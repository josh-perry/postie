<template>
  <div>
    <a href="/">Postie</a>

    <li v-if="!$auth.isAuthenticated && !$auth.loading" class="nav-item">
      <button
        id="qsLoginBtn"
        class="btn btn-primary btn-margin"
        @click.prevent="login"
        >Login</button>
    </li>

    <a v-if="$auth.isAuthenticated && !$auth.loading" id="qsLogoutBtn" href="#" class @click.prevent="logout">Log out</a>

    <BoardList />
  </div>
</template>

<script>
export default {
  name: "NavBar",
  methods: {
    login() {
      this.$auth.loginWithRedirect();
    },
    logout() {
      this.$auth.logout();
      this.$router.push({ path: "/" });
    }
  }
};
</script>

<style>
#mobileAuthNavBar {
  min-height: 125px;
  justify-content: space-between;
}
</style>
