<template>
  <div>
    <ul>
      <li v-for="board in boards" :key="board.id">
        <BoardCard :board="board" />
      </li>
    </ul>
  </div>
</template>

<script>
import axios from "axios";
import BoardCard from "../components/BoardCard";

export default {
  components: {
    BoardCard
  },
  data() {
    return {
      boards: []
    };
  },
  created() {
    this.getPosts()
  },
  methods: {
    async getPosts() {
      let headers = {}

      const { data } = await axios.get("https://localhost:5001/board", {
        headers: headers
      });

      this.boards = data;
    }
  }
}
</script>

<style scoped>
ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
}
</style>
