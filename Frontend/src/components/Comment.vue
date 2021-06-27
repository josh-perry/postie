<template>
  <div class="comment">
    <a href="#">{{ comment.user }}</a>
    <p>{{ comment.content }}</p>
  </div>
</template>

<script>
export default {
  name: "comment",
  props: {
    comment: {
      type: Object,
      default: () => {
        return []
      }
    }
  }
}
</script>

<style scoped>
.comment {
  padding: 16px;
  margin: 8px;
  border: 1px solid black;
}
</style>
