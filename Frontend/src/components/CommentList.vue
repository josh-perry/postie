<template>
  <ul>
    <li v-for="comment in comments" :key="comment.id">
      <Comment :comment="comment" />
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
