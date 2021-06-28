<template>
  <div>
    <h2>Create a new board</h2>

    <form v-on:submit.prevent="createBoard">
      <input v-model="form.title" v-on:input="boardNameChanged" type="text" placeholder="Name" />
      <input v-model="form.url" type="text" placeholder="URL" readonly="true" />

      <input value="Create" type="submit" />
    </form>
  </div>
</template>

<script>
import axios from "axios";
import { store } from "../store/store"

import PostList from "../components/PostList.vue"

export default {
 components: {
    PostList
  },
  data() {
    return {
      form: {
        title: "",
        url: ""
      }
    };
  },
  created() {
    store.dispatch("retrieveTokenFromAuth0").then(() => { })
  },
  methods: {
    async createBoard() {
      let headers = {}

      if (store.state.token !== null) {
        headers["Authorization"] = `Bearer ${store.state.token}`
      }

      const { data } = await axios.put(`https://localhost:5001/board/${this.form.url}`, this.form, {
        headers: headers,
        "Content-Type": "application/json"
      })

      console.log(data)
    },
    boardNameChanged() {
      this.form.url = this.form.title.replaceAll(" ", "-").toLowerCase();
    }
  }
}
</script>
