<template>
  <div id="app" class="d-flex flex-column h-100">
    <nav-bar />

    <div class="container flex-grow-1">
      <error />
      <div class="mt-5">
        <router-view />
      </div>
    </div>

    <footer class="bg-light text-center p-3">
    </footer>
  </div>
</template>

<script>
import NavBar from "./components/NavBar";
import Error from "./components/Error";

export default {
  components: {
    NavBar,
    Error
  }
};
</script>

<style scoped>
.container {
  margin: 2em auto;
  max-width: 800px;
  padding: 1em;
  line-height: 1.4;
  text-align: justify;
}
</style>
