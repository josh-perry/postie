<template>
  <div class="comment">
    <div class="top-details">
      <a class="post-link" v-if="postLink" :href="postLink">Post</a>
      <span class="datetime" :title="comment.createdDateTime">{{ humanDateTime }}</span>
    </div>

    <p>{{ comment.content }}</p>
  </div>
</template>

<script>
import humanDate from "human-date"

export default {
  name: "CommentPreview",
  computed: {
    postLink() {
      if (this.comment.board == null || this.comment.post == null)
        return null

      return `/board/${this.comment.board}/${this.comment.post}`
    },
    humanDateTime() {
      return humanDate.relativeTime(new Date(this.comment.createdDateTime))
    }
  },
  props: {
    comment: {
      type: Object,
      default: () => {
        return {}
      }
    }
  },
}
</script>

<style scoped>
.comment {
  padding: 16px;
  margin: 8px;
  border: 1px solid black;
  white-space: pre-wrap;
}

.comment p {
  max-height: 100px;
  text-overflow: ellipsis;
  overflow: hidden;
}

.links a {
  margin-left: 16px;
}

.top-details {
  display: block;
  padding: 8px;
}

.post-link {
  float: left;
}

.datetime {
  float: right;
}
</style>
