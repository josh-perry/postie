<template>
  <div>
    <h1>{{ postName }}</h1>
    <CommentList :comments="comments" />
  </div>
</template>

<script>
import axios from "axios";
import { store } from "../store/store"

import CommentList from "../components/CommentList.vue"

export default {
 components: {
    CommentList
  },
  data() {
    return {
      comments: [],
    };
  },
  created() {
    this.postName = this.$route.params.postName

    store.dispatch("retrieveTokenFromAuth0").then(() => {
      this.getRootComments()
    })
  },
  methods: {
    async getRootComments() {
      let headers = {}

      if (store.state.token !== null) {
        headers["Authorization"] = `Bearer ${store.state.token}`
      }

      const { data } = await axios.get(`https://localhost:5001/comment/${this.$route.params.boardName}/${this.$route.params.postName}`, {
        headers: headers
      });

      this.comments = data;
    }
  }
}
</script>
