<template>
  <div>
    <NavBar />

    <div class="container">
      <error />
      <div>
        <router-view />
      </div>
    </div>
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
  margin-top: 0;
  max-width: 800px;
  padding: 1em;
  line-height: 1.4;
  text-align: justify;
}
</style>
