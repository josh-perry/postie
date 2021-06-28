<template>
  <div>
    <h2>Create a new board</h2>

    <form v-on:submit.prevent="createBoard">
      <fieldset>
        <label>Board name</label>
        <input v-model="form.title" v-on:input="boardNameChanged" type="text" placeholder="Name" />
      </fieldset>

      <fieldset>
        <label>URL</label>
        <input v-model="form.url" type="text" placeholder="URL" readonly="true" />
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

input, button {
  flex: 3;
  margin: 8px;
}

label {
  flex: 1;
}
</style>
