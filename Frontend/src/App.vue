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

export default {
  components: {
    NavBar,
    SideBar,
    Error
  }
};
</script>

<style scoped>
.container {
  margin: 2em auto;
  margin-top: 0;
  padding: 1em;
  line-height: 1.4;
  display: inline-flex;
}

.main {
  flex: 4;
}

.route-wrapper {
  max-width: 880px;
  margin: auto;
}

.sidebar {
  flex: 1;
}
</style>
