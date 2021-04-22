<template>
  <div>
    <h1>Front Page</h1>

    <PostList :posts="posts" />
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

      const { data } = await axios.get(`https://localhost:5001/board/${this.$route.params.boardName}`, {
        headers: headers
      });

      this.posts = data;
    }
  }
}
</script>
