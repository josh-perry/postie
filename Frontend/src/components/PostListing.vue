<template>
  <div class="post">
    <a href="#">{{ post.title }}</a>
    
    <div class="details">
      <p>{{ post.username }}</p>
      <p>{{ post.postedDateTime }}</p>
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
          href: "",
          commentCount: 0,
          postedDateTime: "",
          username: ""
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
