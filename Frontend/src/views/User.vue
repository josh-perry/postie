<template>
  <div>
    <h2>{{ username }}</h2>

    <h3>Recent posts</h3>
    <PostList :posts="posts" />

    <h3>Recent comments</h3>
    <ul>
      <li v-for="comment in comments" :key="comment.id">
        <CommentPreview :comment="comment" />
      </li>
    </ul>
  </div>
</template>

<script>
import apiClient from "../api_client"

import PostList from "../components/PostList.vue"
import CommentList from "../components/CommentList.vue"
import CommentPreview from "../components/CommentPreview.vue"

export default {
 components: {
    PostList,
    CommentList,
    CommentPreview
  },
  data() {
    return {
      posts: [],
      comments: [],
      username: ""
    };
  },
  async created() {
    this.username = this.$route.params.username

    let response = await apiClient.getUser(this.username)
    this.posts = response.recentPosts
    this.comments = response.recentComments
  }
}
</script>

<style scoped>
ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
}
</style>
