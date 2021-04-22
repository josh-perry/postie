<template>
  <ul>
    <li v-for="post in posts" :key="post.name">
      <PostListing :post="post" />
    </li>
  </ul>
</template>

<script>
import PostListing from "../components/PostListing.vue"

export default {
  name: "posts",
  components: {
    PostListing
  },
  props: {
    posts: {
      type: Array,
      default: () => {
        return []
      }
    }
  }
}
</script>
