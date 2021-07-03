<template>
  <div>
    <h2>Submit post</h2>

    <form v-on:submit.prevent="createPost">
      <fieldset>
        <label>Post title</label>
        <input v-model="form.title" type="text" placeholder="Title" />
      </fieldset>

      <fieldset>
        <label>Board</label>
        <select v-model="form.board">
          <option v-for="board in boards" v-bind:key="board">{{ board }}</option>
        </select>
      </fieldset>

      <fieldset>
        <textarea v-model="form.content" placeholder="Post something wonderful" />
      </fieldset>

      <fieldset>
        <input value="Create" type="submit" />
        <button @click="cancel">Cancel</button>
      </fieldset>
    </form>
  </div>
</template>

<script>
import axios from "axios";
import { store } from "../store/store"

export default {
  data() {
    return {
      form: {
        title: "",
        content: ""
      },
      boards: []
    };
  },
  created() {
    this.getBoards()
    store.dispatch("retrieveTokenFromAuth0").then(() => { })
  },
  methods: {
    async getBoards() {
      let headers = {}

      if (store.state.token !== null) {
        headers["Authorization"] = `Bearer ${store.state.token}`
      }
      
      const { data } = await axios.get(`https://localhost:5001/board/`, {
        headers: headers,
        "Content-Type": "application/json"
      })

      this.boards = data.map(x => x.url)

      let defaultBoard = this.$route.params.boardName
 
      if (defaultBoard == null) {
        return
      }

      if (this.boards.includes(defaultBoard)) {
        this.form.board = defaultBoard
      }
    },
    async createPost() {
      let headers = {}

      if (store.state.token !== null) {
        headers["Authorization"] = `Bearer ${store.state.token}`
      }
      
      const { data } = await axios.post(`https://localhost:5001/post/board/${this.form.board}`, this.form, {
        headers: headers,
        "Content-Type": "application/json"
      })

      this.$router.push(`/board/${this.form.board}/${data.url}`);
    },
    cancel() {
      this.$router.push("/")
    }
  }
}
</script>

<style scoped>
fieldset {
  display: flex;
  border: 0;
}

input, button, select {
  flex: 3;
  margin: 8px;
}

label {
  flex: 1;
}

textarea {
  width: 100%;
  min-height: 220px;
}
</style>
