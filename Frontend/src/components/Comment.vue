<template>
  <div>
    <p>{{ comment.content }}</p>
  </div>
</template>

<script>
export default {
  name: "comment",
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
