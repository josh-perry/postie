<template>
  <div>
    <NavBar />

    <Error />

    <div class="container">
      <div class="main">
        <div class="route-wrapper">
          <RouterView />
        </div>
      </div>

      <div class="sidebar">
        <SideBar />
      </div>
    </div>
  </div>
</template>

<script>
import NavBar from "./components/NavBar";
import SideBar from "./components/SideBar";
import Error from "./components/Error";

import { store } from "./store/store"

export default {
  components: {
    NavBar,
    SideBar,
    Error
  },
  created() {
    store.dispatch("retrieveTokenFromAuth0").then(() => {
      store.dispatch("retrieveUserDetails")
    })
  }
};
</script>

<style scoped>
.container {
 line-height: 1.4;
  display: inline-flex;
  width: 100%;
}

.main {
  flex: 4;
}

.route-wrapper {
  margin: 2em auto;
  margin-top: 0;
 
  max-width: 880px;
  margin: auto;
  padding: 30px;
  background-color: #FFFCF9;
}

.sidebar {
  flex: 1.5;
  background-color: #FFFCF9;
}
</style>
