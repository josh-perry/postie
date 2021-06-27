<template>
  <div>
    <p v-if="parentCommentId == 0">Replying to thread</p>
    <p v-if="parentCommentId != 0">Replying to comment</p>
    <textarea v-model="content" placeholder="Say something nice!" />
    <button v-on:click="postComment">Leave a comment</button>
  </div>
</template>

<script>
import axios from "axios";
import { store } from "../store/store"

export default {
  name: "commentbox",
  props: {
    parentCommentId: {
      type: Number,
      default: () => {
        return 0
      }
    },
    post: {
      type: String,
      default: () => {
        return ""
      }
    },
    board: {
      type: String,
      default: () => {
        return ""
      }
    },
    content: {
      type: String,
      default: () => {
        return ""
      }
    }
  },
  created() {
    store.dispatch("retrieveTokenFromAuth0").then(() => { })
  },
  methods: {
    async postComment() {
      let headers = {}

      if (store.state.token !== null) {
        headers["Authorization"] = `Bearer ${store.state.token}`
      }

      let json = {
        parentCommentId: this.parentCommentId,
        content: this.content
      }

      const { data } = await axios.post(`https://localhost:5001/comment/${this.$route.params.boardName}/${this.$route.params.postName}`, json, {
        headers: headers,
        "Content-Type": "application/json"
      });

      this.content = ""
      this.$emit("addedComment", data)
    }
  }
}
</script>

<style scoped>
</style>
