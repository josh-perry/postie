<template>
  <div>
    <h1>Test</h1>
    <ul>
      <li v-for="post in posts" :key="post.name">
        <div>
          <h2>{{ post.name }}</h2>
          <p>{{ post.content }}</p>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import axios from "axios";
import { store } from "../store/store"

export default {
  name: "posts",
  data() {
    return {
      posts: []
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

      const { data } = await axios.get("https://localhost:5001/posts", {
        headers: headers
      });

      this.posts = data;
    }
  }
}
</script>
