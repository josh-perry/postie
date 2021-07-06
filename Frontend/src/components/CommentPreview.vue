<template>
  <div class="comment">
    <a v-if="postLink" :href="postLink">Post</a>
    <p>{{ comment.content }}</p>
  </div>
</template>

<script>
export default {
  name: "CommentPreview",
  computed: {
    postLink() {
      if (this.comment.board == null || this.comment.post == null)
        return null

      return `/board/${this.comment.board}/${this.comment.post}`
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
</style>
