<template>
  <ul>
    <li v-for="comment in comments" :key="comment.id">
      <Comment :comment="comment" :depth="1" />
    </li>
  </ul>
</template>

<script>
import Comment from "../components/Comment.vue"

export default {
  name: "commentlist",
  components: {
    Comment
  },
  props: {
    comments: {
      type: Array,
      default: () => {
        return []
      }
    }
  }
}
</script>

<style scoped>
ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
}
</style>
