<template>
  <div>
    <h1>{{ post.title }}</h1>

    <p>
      Posted by <a class="user" :href="`/user/${post.createdBy}`">{{ post.createdBy }}</a>
      <span class="datetime" :title="post.createdDateTime"> {{ humanDateTime }}</span>
    </p>

    <hr />

    <span class="post-content">
      {{ post.content }}
    </span>

    <div class="votes">
      <Votes :post="post" />
    </div>

    <hr />
    <CommentBox :post="postName" :board="boardName" v-on:addedComment="addedComment" />
    <CommentList :comments="comments" />
  </div>
</template>

<script>
import axios from "axios";
import { store } from "../store/store"

import CommentList from "../components/CommentList.vue"
import CommentBox from "../components/CommentBox.vue"
import Votes from "../components/Votes.vue"

import humanDate from "human-date"

export default {
 components: {
    CommentList,
    CommentBox,
    Votes
  },
  data() {
    return {
      comments: [],
      post: {}
    };
  },
  computed: {
    humanDateTime() {
      return humanDate.relativeTime(new Date(this.post.createdDateTime))
    }
  },
  created() {
    this.postName = this.$route.params.postName
    this.boardName = this.$route.params.boardName

    store.dispatch("retrieveBoardDetails", this.boardName)

    this.getPost()
    this.getRootComments()
  },
  methods: {
    async getPost() {
      let headers = {}

      if (store.state.token !== null) {
        headers["Authorization"] = `Bearer ${store.state.token}`
      }

      const { data } = await axios.get(`https://localhost:5001/post/board/${this.$route.params.boardName}/${this.$route.params.postName}`, {
        headers: headers
      });

      this.post = data
    },
    async getRootComments() {
      let headers = {}

      if (store.state.token !== null) {
        headers["Authorization"] = `Bearer ${store.state.token}`
      }

      const { data } = await axios.get(`https://localhost:5001/comment/${this.$route.params.boardName}/${this.$route.params.postName}?childrenOf=0`, {
        headers: headers
      });

      // TODO: I don't know. Make this less disastrous I guess. Maybe this should be done server-side? Maybe it's fine. Who knows.
      data.filter(x => x.parentCommentId != null).forEach(childComment => {
        let parentComment = data.find(parentComment => parentComment.id == childComment.parentCommentId)
        parentComment.children.push(childComment)
      });

      this.comments = data.filter(x => x.parentCommentId == null);
    },
    addedComment (value) {
      this.comments.push(value)
    }
  }
}
</script>

<style scoped>
.post-content {
  white-space: pre-wrap;
}

.votes {
  text-align: right;
}
</style>
