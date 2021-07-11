<template>
  <div class="post">
    <a class="title" :href="`/board/${post.board}/${post.url}`">{{ post.title }}</a>

    <div class="details">
      <span>
        Posted by <a :href="`/user/${post.createdBy}`">{{ post.createdBy }}</a> on <a :href="`/board/${post.board}`">{{ post.board }}</a>
      </span>
    </div>
  
    <div class="stats">
      <p class="datetime" :title="post.createdDateTime"> {{ humanDateTime }}</p>
      <p>{{ post.commentCount }} comments</p>
    </div>
  </div>
</template>

<script>
import humanDate from "human-date"

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
  },
  computed: {
    humanDateTime() {
      return humanDate.relativeTime(new Date(this.post.createdDateTime))
    }
  }
}
</script>

<style scoped>
.details {
  display: flex;
  font-size: 14px;
}

p {
  margin: 0;
  padding: 0;
}

.details a {
  flex: 1;
}

.details span a {
  color: #F06543FF;
}

.stats {
  display: flex;
  font-size: 14px;
}

.stats p {
  flex: 1;
}

.title {
  font-size: 24px;
}

.post {
  padding: 16px;
  padding-right: 0;
  margin: 8px;
  border: 1px solid #CACACA;
  border-left: 6px solid #F06543FF;
}

.title {
  text-decoration: none;
}
</style>
