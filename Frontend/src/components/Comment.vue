<template>
  <div class="comment" v-bind:class="{ 'child-comment': comment.parentCommentId !== null, 'parent-comment': comment.children.length !== 0 }">
    <p><a :href="`/user/${comment.user}`">{{ comment.user }}</a></p>
    <p>{{ comment.content }}</p>

    <div class="links">
      <a href="#" @click.prevent="createCommentBox">Reply</a>
    </div>

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
  },
  methods: {
    createCommentBox() {
      // TODO: this
      console.log("wanna reply to this!")
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
