<template>
  <div class="comment">
    <p><a :href="`/user/${comment.user}`">{{ comment.user }}</a></p>
    <p>{{ comment.content }}</p>

    <div v-for="childComment in comment.children" v-bind:key="childComment.id">
      <Comment :comment="childComment" />
    </div>
  </div>
</template>

<script>
export default {
  name: "Comment",
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
