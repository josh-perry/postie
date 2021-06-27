<template>
  <div class="comment">
    <p><a :href="`/user/${comment.user}`">{{ comment.user }}</a></p>
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
