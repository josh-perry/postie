<template>
  <div class="comment-box">
    <p v-if="parentCommentId == 0">Replying to thread</p>
    <p v-if="parentCommentId != 0">Replying to comment</p>

    <div>
      <textarea v-model="content" placeholder="Say something nice!" />
    </div>

    <a href="#" @click.prevent="postComment">Leave a comment</a>
  </div>
</template>

<script>
import axios from "axios";
import { store } from "../store/store"

export default {
  name: "CommentBox",
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
textarea {
  width: 100%;
  box-sizing: border-box;
  min-height: 120px;
  margin: 0;
}

.comment-box {
  text-align: right;
  padding: 16px;
  margin: 8px;
}

a {
  width: 100%;
}
</style>
