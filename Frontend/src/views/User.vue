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
import axios from "axios";
import { store } from "../store/store"

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
  created() {
    this.username = this.$route.params.username
    this.getUser()
  },
  methods: {
    async getUser() {
      let headers = {}

      if (store.state.token !== null) {
        headers["Authorization"] = `Bearer ${store.state.token}`
      }

      const { data } = await axios.get(`https://localhost:5001/user/${this.username}`, {
        headers: headers
      })

      this.posts = data.recentPosts
      this.comments = data.recentComments
    }
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
