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
import { store } from "../store/store"
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
    store.dispatch("retrieveTokenFromAuth0").then(() => {
      this.getPosts()
    })
  },
  methods: {
    async getPosts() {
      let headers = {}

      if (store.state.token !== null) {
        headers["Authorization"] = `Bearer ${store.state.token}`
      }

      const { data } = await axios.get("https://localhost:5001/board", {
        headers: headers
      });

      this.boards = data;
    }
  }
}
</script>
