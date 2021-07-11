<template>
  <div>
    <h2>{{ boardName }}</h2>

    <PostList :posts="posts" />
  </div>
</template>

<script>
import PostList from "../components/PostList.vue"

import { store } from "../store/store"

import apiClient from "../api_client"

export default {
 components: {
    PostList
  },
  data() {
    return {
      posts: [],
      boardName: ""
    };
  },
  async created() {
    this.boardName = this.$route.params.boardName

    store.dispatch("retrieveBoardDetails", this.boardName)
    this.posts = await apiClient.getPostsForBoard(this.$route.params.boardName)
  }
}
</script>
