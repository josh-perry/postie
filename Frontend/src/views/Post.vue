<template>
  <div>
    <h1>{{ postName }}</h1>
    <CommentList :comments="comments" />
    <CommentBox :post="postName" :board="boardName" v-on:addedComment="addedComment" />
  </div>
</template>

<script>
import axios from "axios";
import { store } from "../store/store"

import CommentList from "../components/CommentList.vue"
import CommentBox from "../components/CommentBox.vue"

export default {
 components: {
    CommentList,
    CommentBox
  },
  data() {
    return {
      comments: [],
    };
  },
  created() {
    this.postName = this.$route.params.postName
    this.boardName = this.$route.params.boardName

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

      const { data } = await axios.get(`https://localhost:5001/comment/${this.$route.params.boardName}/${this.$route.params.postName}?childrenOf=0`, {
        headers: headers
      });

      this.comments = data;
    },
    addedComment (value) {
      this.comments.push(value)
    }
  }
}
</script>
