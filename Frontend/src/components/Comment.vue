<template>
  <div class="comment" v-bind:class="{ 'child-comment': comment.parentCommentId !== null, 'parent-comment': comment.children.length !== 0 }">
    <div class="top-details">
      <a class="user" :href="`/user/${comment.user}`">{{ comment.user }}</a>
      <span class="datetime" :title="comment.createdDateTime">{{ humanDateTime }}</span>
    </div>

    <div class="comment-content">
      <p>{{ comment.content }}</p>
    </div>

    <div class="links">
      <a href="#" @click.prevent="createCommentBox">Reply</a>
    </div>

    <CommentBox v-if="showCommentBox" :parentCommentId="comment.id" v-on:addedComment="addedComment" />

    <div v-for="childComment in comment.children" v-bind:key="childComment.id">
      <Comment :comment="childComment" />
    </div>
  </div>
</template>

<script>
import CommentBox from "../components/CommentBox.vue"
import humanDate from "human-date"

export default {
  components: {
    CommentBox
  },
  name: "Comment",
  data() {
    return {
      showCommentBox: false
    }
  },
  props: {
    comment: {
      type: Object,
      default: () => {
        return []
      }
    }
  },
  computed: {
    humanDateTime() {
      return humanDate.relativeTime(new Date(this.comment.createdDateTime))
    }
  },
  methods: {
    createCommentBox() {
      this.showCommentBox = !this.showCommentBox
    },
    addedComment(value) {
      this.comment.children.push(value)
      this.showCommentBox = false
    }
  }
}
</script>

<style scoped>
.comment {
  padding: 16px;
  padding-right: 0;
  margin: 8px;
  border: 1px solid black;
  white-space: pre-wrap;
}

.comment p {
  padding-right: 16px;
}

.parent-comment { }

.child-comment {
  margin-right: 0;
  border-right: 0;
}

.links {
  padding-right: 16px;
  display: flex;
  justify-content: flex-end;
}

.links a {
  margin-left: 16px;
}

.top-details {
  display: block;
  padding: 8px;
}

.user {
  float: left;
}

.datetime {
  float: right;
}

.comment-content {
  padding-top: 12px;
}
</style>
