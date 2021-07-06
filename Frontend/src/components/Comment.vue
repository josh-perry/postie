<template>
  <div class="comment" v-bind:class="{ 'child-comment': comment.parentCommentId !== null, 'parent-comment': comment.children.length !== 0 }">
    <p><a :href="`/user/${comment.user}`">{{ comment.user }}</a></p>
    <p>{{ comment.content }}</p>

    <div class="links">
      <a href="#" @click.prevent="createCommentBox">Reply</a>
    </div>

    <CommentBox v-if="showCommentBox" :parentCommentId="comment.id" v-on:addedComment="addedComment" />

    <div v-for="childComment in comment.children" v-bind:key="childComment.id">
      <Comment :comment="childComment" />
    </div>
  </div>
</template>

<script>
import CommentBox from "../components/CommentBox.vue"

export default {
  components: {
    CommentBox
  },
  name: "Comment",
  data() {
    return {
      showCommentBox: false
    }
  },
  props: {
    comment: {
      type: Object,
      default: () => {
        return []
      }
    }
  },
  methods: {
    createCommentBox() {
      this.showCommentBox = !this.showCommentBox
    },
    addedComment(value) {
      this.comment.children.push(value)
      this.showCommentBox = false
    }
  }
}
</script>

<style scoped>
.comment {
  padding: 16px;
  padding-right: 0;
  margin: 8px;
  border: 1px solid black;
  white-space: pre-wrap;
}

.comment p {
  padding-right: 16px;
}

.parent-comment { }

.child-comment {
  margin-right: 0;
  border-right: 0;
}

.links {
  padding-right: 16px;
  display: flex;
  justify-content: flex-end;
}

.links a {
  margin-left: 16px;
}
</style>
