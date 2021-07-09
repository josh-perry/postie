<template>
  <div>
    <h1>Home</h1>
    <PostList :posts="posts" />
  </div>
</template>

<script>
import PostList from "../components/PostList"

import axios from "axios"
import { store } from "../store/store"

export default {
  components: {
    PostList
  },
  data() {
    return {
      posts: [],
    }
  },
  created() {
    this.getPosts()
  },
  methods: {
    async getPosts() {
      let headers = {}

      if (store.state.token !== null) {
        headers["Authorization"] = `Bearer ${store.state.token}`
      }

      const { data } = await axios.get(`https://localhost:5001/post/top`, {
        headers: headers
      });

      this.posts = data
    }
  }
}
</script>
