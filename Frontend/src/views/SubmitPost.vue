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
        <label>Content</label>
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
      if (this.$route.params.boardName != null) {
        this.$router.push(`/board/${this.$route.params.boardName}`)
        return
      }

      this.$router.push("/")
    }
  }
}
</script>

<style scoped>
fieldset {
  border: 0;
  display: flex;
}

label {
  flex: 1;
}

input, textarea, select {
  flex: 3;
  margin: 8px;
}

button {
  flex: 3;
  margin: 8px;
  padding: 8px;
}

textarea {
  min-height: 120px;
}
</style>
