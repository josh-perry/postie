<template>
  <div class="post">
    <a :href="`${post.board}/${post.url}`">{{ post.title }}</a>
    
    <div class="details">
      <p>{{ post.createdBy }}</p>
      <p>{{ post.createdDateTime }}</p>
      <p>{{ post.commentCount }} comments</p>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    post: {
      type: Object,
      default: () => {
        return {
          title: "",
          createdBy: "",
          board: "",
          createdDateTime: ""
        }
      }
    }
  }
}
</script>

<style scoped>
.post {
  margin: 12px;
}

.details {
  display: flex;
}

.details p {
  flex: 1;
}
</style>
