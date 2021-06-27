<template>
  <div class="post">
    <a :href="`/board/${post.board}/${post.url}`">{{ post.title }}</a>
    
    <div class="details">
      <p><a :href="`/user/${post.createdBy}`">{{ post.createdBy }}</a></p>
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
.details {
  display: flex;
}

.details p {
  flex: 1;
}

.details a {
  flex: 1;
}

.post {
  padding: 16px;
  margin: 8px;
  border: 1px solid black;
}

a {
  text-decoration: none;
}
</style>
