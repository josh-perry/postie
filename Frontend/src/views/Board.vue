<template>
  <div>
    <h2>{{ boardName }}</h2>

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
      posts: [],
      boardName: ""
    };
  },
  created() {
    console.log("created")
    this.boardName = this.$route.params.boardName

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

      const { data } = await axios.get(`https://localhost:5001/post/board/${this.$route.params.boardName}`, {
        headers: headers
      });

      this.posts = data;
    }
  }
}
</script>
