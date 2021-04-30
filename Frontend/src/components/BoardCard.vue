<template>
  <a :href="`/board/${board.title}`">
    <div class="board-card">
      <p class="board-title">{{ board.title }}</p>
      <p class="created-by">{{ board.createdBy }}</p>
    </div>
  </a>
</template>

<script>
export default {
  props: {
    board: {
      type: Object,
      default: () => {

      }
    }
  }
}
</script>

<style scoped>
.board-title {

}

.board-card {
  border: 1px solid black;
  border-radius: 5px;
  padding: 6px;
  margin: 5px;
}

a {
  text-decoration: none;
}
</style>
