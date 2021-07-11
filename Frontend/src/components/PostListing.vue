<template>
  <div class="post">
    <div class="vote-buttons">
      <div>
        <a href="#">▲</a>
      </div>

      <div>
        <a href="#">▼</a>
      </div>
    </div>

    <div class="main">
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
  display: inline-block;
  font-size: 24px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.post {
  padding: 16px;
  padding-left: 0;
  padding-right: 0;
  margin: 8px;
  border: 1px solid #CACACA;
  border-left: 6px solid #F06543FF;

  display: flex;
}

.main {
  flex: 1;
}

.vote-buttons {
  width: 32px;
  display: flex;
  flex-direction: column;
}

.vote-buttons div {
  flex: 1;
  margin: 2px;
  justify-content: center;
  align-contents: center;
  display: flex;
}

.vote-buttons div a {
  margin: auto;
  text-decoration: none;
}

.title {
  text-decoration: none;
}
</style>
