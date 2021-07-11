<template>
  <div>
    <h1>Home</h1>
    <PostList :posts="posts" />
  </div>
</template>

<script>
import PostList from "../components/PostList"

import apiClient from "../api_client"

export default {
  components: {
    PostList
  },
  data() {
    return {
      posts: [],
    }
  },
  async created() {
    this.posts = await apiClient.getTopPosts()
  }
}
</script>
