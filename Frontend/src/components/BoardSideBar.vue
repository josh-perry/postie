<template>
  <div>
    <div class="section">
      <h3>{{ board.title }}</h3>
      <p>Created by <a :href="`/user/${board.createdBy}`">{{ board.createdBy }}</a></p>

      <div class="board-description">
        <p>
          {{ board.description }}
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import { store } from "../store/store"

export default {
  computed: {
    board() {
      return store.state.board
    }
  }
};
</script>

<style scoped>
.board-description {
  white-space: pre-wrap;
}
</style>
