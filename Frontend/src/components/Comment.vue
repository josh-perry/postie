<template>
  <div v-on:animationend="commentAnimationEnd" :id="`Comment${comment.id}`" class="comment" :class="getCommentClass()">
    <div class="top-details">
      <a class="user" :href="`/user/${comment.user}`">{{ comment.user }}</a>
      <span> — </span>
      <a class="comment-anchor" @click="commentDateClicked" :href="`#Comment${comment.id}`">
        <span class="datetime" :title="comment.createdDateTime">{{ humanDateTime }}</span>
      </a>
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
  created() {
    let hash = this.$route.hash

    if (!hash || !hash.startsWith("#Comment")) {
      return
    }

    let hashComment = parseInt(hash.replace("#Comment", ""))

    if (this.comment.id == hashComment) {
      this.highlightComment = true
    }
  },
  data() {
    return {
      showCommentBox: false,
      highlightComment: false
    }
  },
  props: {
    comment: {
      type: Object,
      default: () => {
        return { }
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

      // Scroll to the new comment
      this.$router.push({ hash: `Comment${value.id}` })
    },
    getCommentClass() {
      let classes = {
        'child-comment': this.comment.parentCommentId !== null,
        'parent-comment': this.comment.children.length !== 0,
        'highlight-comment': this.highlightComment
      }

      if (this.depth < 5) {
        classes[`comment-depth-${this.depth}`] = true
      }
      else {
        classes[`comment-depth-deep`] = true
      }

      return classes
    },
    commentAnimationEnd() {
      this.highlightComment = false
    },
    commentDateClicked() {
      this.highlightComment = true
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

@keyframes highlight-comment-animation {
  from {
    background-color: #F0645455;
  }

  to {
    background-color: unset;
  }
}

.highlight-comment {
  animation: highlight-comment-animation 4s infinite;
  animation-iteration-count: 1;
}

.comment-depth-1 {
  border-left: 6px solid #F06543FF;
}

.comment-depth-2 {
  border-left: 6px solid #F06543DD;
}

.comment-depth-3 {
  border-left: 6px solid #F06543AA;
}

.comment-depth-4 {
  border-left: 6px solid #F0654377;
}

.comment-depth-deep {
  border-left: 6px solid #F0654355;
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
</style>
