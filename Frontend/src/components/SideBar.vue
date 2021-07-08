<template>
  <div class="sidebar">
    <div class="section">
      <input type="text" placeholder="Search..."/>
    </div>

    <hr />

    <div class="section">
      <a href="/create-board">Create a new board</a>
      <a :href="submitLinkDestination">Submit post</a>
    </div>

    <hr v-if="showBoardDetails" />

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
  computed: {
    submitLinkDestination() {
      if (this.$route.params.boardName == null)
        return "/submit"

      return `/board/${this.$route.params.boardName}/submit`
    }
  },
  data() {
    // I don't like this but it works for now
    return {
      showBoardDetails: this.$route.path.startsWith("/board"),
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
  margin-top: 16px;
  margin-bottom: 16px;
}

hr {
  margin-left: 24px;
  margin-right: 24px;
  margin-top: 16px;
  margin-bottom: 16px;
}

.sidebar {
  padding: 16px;
}

input {
  width: 100%;
}

a {
  display: block;
  padding: 8px;
}
</style>
