<template>
  <div class="comment" v-bind:class="getCommentClass()">
    <div class="top-details">
      <a class="user" :href="`/user/${comment.user}`">{{ comment.user }}</a>
      <span class="datetime" :title="comment.createdDateTime">{{ humanDateTime }}</span>
    </div>

    <div class="comment-content">
      <p>{{ comment.content }}</p>
    </div>

    <div class="links">
      <a href="#" @click.prevent="createCommentBox">Reply</a>
    </div>

    <CommentBox v-if="showCommentBox" :parentCommentId="comment.id" v-on:addedComment="addedComment" />

    <div v-for="childComment in comment.children" v-bind:key="childComment.id">
      <Comment :comment="childComment" :depth="depth+1" />
    </div>
  </div>
</template>

<script>
import CommentBox from "../components/CommentBox.vue"
import humanDate from "human-date"

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
    },
    depth: {
      type: Number,
      default: () => {
        return 1
      }
    }
  },
  computed: {
    humanDateTime() {
      return humanDate.relativeTime(new Date(this.comment.createdDateTime))
    }
  },
  methods: {
    createCommentBox() {
      this.showCommentBox = !this.showCommentBox
    },
    addedComment(value) {
      this.comment.children.push(value)
      this.showCommentBox = false
    },
    getCommentClass() {
      let classes = {
        'child-comment': this.comment.parentCommentId !== null,
        'parent-comment': this.comment.children.length !== 0,
      }

      classes[`comment-depth-${this.depth}`] = true
      return classes
    }
  }
}
</script>

<style scoped>
.comment {
  padding: 16px;
  padding-right: 0;
  margin: 8px;
  border: 1px solid #CACACA;
  white-space: pre-wrap;
}

.comment-depth-1 {
  border-left: 6px solid #F06543FF;
}

.comment-depth-2 {
  border-left: 6px solid #F06543CC;
}

.comment-depth-3 {
  border-left: 6px solid #F0654399;
}

.comment-depth-4 {
  border-left: 6px solid #F0654366;
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

.top-details {
  display: block;
  padding: 8px;
}

.user {
  float: left;
}

.datetime {
  float: right;
}

.comment-content {
  padding-top: 12px;
}
</style>
