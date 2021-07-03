<template>
  <div>
    <div class="section">
      <input type="text" placeholder="Search..."/>
    </div>

    <div class="section">
      <a href="/create-board">Create a new board</a>
      <a href="/submit">Submit post</a>
    </div>

    <div v-if="showBoardDetails" class="section">
      <BoardSideBar />
    </div>
  </div>
</template>

<script>
import BoardSideBar from '../components/BoardSideBar.vue';

export default {
  components: {
    BoardSideBar
  },
  data() {
    // I don't like this but it works for now
    return {
      showBoardDetails: this.$route.path.startsWith("/board")
    }
  }
};
</script>

<style scoped>
.search {
  margin: 8px;
}

.search input {
  width: 80%;
}

.section {
  margin: 8px;
}

a {
  display: block;
  padding: 8px;
}
</style>
